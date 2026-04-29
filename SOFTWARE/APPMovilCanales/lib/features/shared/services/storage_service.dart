import 'package:caja_tacna_app/core/models/encrypt_response.dart';
import 'package:caja_tacna_app/core/services/encrypt_service.dart';
import 'package:shared_preferences/shared_preferences.dart';
import 'dart:convert';
import 'package:flutter_secure_storage/flutter_secure_storage.dart';

class StorageService {
  final storageService = StorageService1();

  Future<T?> get<T>(String key) async {
    return storageService.get<T>(key);
  }

  Future<void> remove(String key) async {
    return storageService.remove(key);
  }

  Future<void> set<T>(String key, T value) async {
    return storageService.set<T>(key, value);
  }
}

class StorageService1 {
  Future<SharedPreferences> getSharedPreferences() async {
    return await SharedPreferences.getInstance();
  }

  Future<void> set<T>(String key, T value) async {
    final prefs = await getSharedPreferences();
    final Map<String, dynamic> data = {'data': value};

    final dataEncriptada = await EncryptService.encrypt(json.encode(data));
    prefs.setString(key, jsonEncode(dataEncriptada));
  }

  Future<T?> get<T>(String key) async {
    try {
      final prefs = await getSharedPreferences();

      final dataEncriptadaString = prefs.getString(key);
      if (dataEncriptadaString == null) return null;
      final dataEncriptada = jsonDecode(dataEncriptadaString);
      final EncrypResponse encrypResponse =
          EncrypResponse.fromJson(dataEncriptada);

      dynamic dataDesencriptada;

      if (encrypResponse.isGCM) {
        dataDesencriptada = await EncryptService.decrypt(encrypResponse);
      } else {
        dataDesencriptada = await EncryptService.decrypLegacyCBC(encrypResponse);

        final value = dataDesencriptada['data'] as T;
        await set<T>(key, value);
      }

      return dataDesencriptada['data'] as T;
    } catch (e) {
      return null;
    }
  }

  Future<void> remove(String key) async {
    final prefs = await getSharedPreferences();
    await prefs.remove(key);
  }
}

class StorageService2 {
  Future<FlutterSecureStorage> initFluterSecureStorage() async {
    const storage = FlutterSecureStorage(
      aOptions: AndroidOptions(),
    );
    return storage;
  }

  Future<void> set<T>(String key, T value) async {
    final storage = await initFluterSecureStorage();
    final Map<String, dynamic> data = {'data': value};

    await storage.write(key: key, value: json.encode(data));
  }

  Future<T?> get<T>(String key) async {
    try {
      final storage = await initFluterSecureStorage();

      String? value = await storage.read(key: key);

      if (value == null) return null;

      final data = (json.decode(value))['data'];

      return data as T;
    } catch (e) {
      return null;
    }
  }

  Future<void> remove(String key) async {
    final storage = await initFluterSecureStorage();
    await storage.delete(key: key);
  }
}
