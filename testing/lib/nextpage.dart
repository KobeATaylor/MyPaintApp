import 'package:flutter/material.dart';
import 'dart:convert';
import 'dart:io';
import 'package:http/http.dart' as http;
import 'package:http/io_client.dart';
import 'models/painting.dart';

class NextPage extends StatefulWidget {
  const NextPage({super.key});

  @override
  State<NextPage> createState() => _NextPageState();
}

Future<List<Painting>> getPaintings() async {
  final client = HttpClient()
    ..badCertificateCallback = (cert, host, port) => true;
  final ioClient = IOClient(client);

  final url = Uri.parse('http://10.0.2.2:5041/paint/GetPainting');
  try {
    final response = await ioClient.get(url);
    if (response.statusCode == 200) {
      List<dynamic> jsonList = jsonDecode(response.body);
      return jsonList.map((json) => Painting.fromJson(json)).toList();
    } else {
      throw Exception('Failed to load paintings');
    }
  } catch (e) {
    throw Exception("Fetch failed: $e");
  }
}

class _NextPageState extends State<NextPage> {
  List<Painting> paintings = [];
  bool isLoading = true;

  @override
  void initState() {
    super.initState();
    loadPaintings();
  }

  Future<void> loadPaintings() async {
    try {
      paintings = await getPaintings();
    } catch (e) {
      print("Error loading paintings: $e");
      ScaffoldMessenger.of(context).showSnackBar(
        SnackBar(content: Text('Error: $e')),
      );
    } finally {
      setState(() {
        isLoading = false;
      });
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Painting Catalog'),
      ),
      body: isLoading
          ? const Center(child: CircularProgressIndicator())
          : paintings.isEmpty
          ? const Center(child: Text("No paintings found."))
          : ListView.builder(
        itemCount: paintings.length,
        itemBuilder: (context, index) {
          final painting = paintings[index];
          return ListTile(
            leading: const Icon(Icons.brush),
            title: Text(painting.Title),
            subtitle: Text(painting.PaintingUrl),
          );
        },
      ),
    );
  }
}
