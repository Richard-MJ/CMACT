#!/bin/bash

# --- Build Script para Aplicación Flutter ---
# Este script automatiza el proceso de compilación de un APK de Flutter
# y lo copia a un directorio específico según el entorno y el SO.
#
# Uso:
# ./build_app.sh [contraseña_keychain] [entorno] [so]
#
# Parámetros:
#   [contraseña_keychain]: La contraseña para desbloquear el keychain de login.
#   [entorno]: El entorno de compilación. Valores posibles: DES, CERT, PROD.
#   [so]: El sistema operativo de destino. Valores posibles: android, huawei.
#
# Ejemplo:
# ./build_app.sh "miContraseña123" CERT android

# --- 1. Validación de Parámetros ---

# Verifica si se proporcionaron los 3 argumentos necesarios
if [ "$#" -ne 3 ]; then
    echo "Error: Faltan parámetros." >&2
    echo "Uso: $0 [contraseña_keychain] [entorno] [so]" >&2
    echo "Entornos válidos: DES, CERT, PROD" >&2
    echo "SO válidos: android, huawei" >&2
    exit 1
fi

KEYCHAIN_PASSWORD="$1"
ENVIRONMENT=$(echo "$2" | tr '[:lower:]' '[:upper:]') # Convierte a mayúsculas
OS=$(echo "$3" | tr '[:lower:]' '[:upper:]')         # Convierte a mayúsculas

# Valida el parámetro de entorno
case "$ENVIRONMENT" in
    DES|CERT|PROD)
        echo "Entorno seleccionado: $ENVIRONMENT"
        ;;
    *)
        echo "Error: Entorno '$2' no válido. Use DES, CERT, o PROD." >&2
        exit 1
        ;;
esac

# Valida el parámetro de sistema operativo
case "$OS" in
    ANDROID|HUAWEI|IOS)
        echo "Sistema Operativo seleccionado: $OS"
        ;;
    *)
        echo "Error: Sistema Operativo '$3' no válido. Use android o huawei." >&2
        exit 1
        ;;
esac

# --- 2. Preparación y Compilación ---

timestamp=$(date +'%d%m%Y%H%M%S')
source_apk="build/app/outputs/flutter-apk/app-release.apk"
source_ipa="build/ios/ipa"
destination_file="app-release-$timestamp.apk"
destination_dir="ipa-$timestamp"
destination_path=""
source_path=""

# Define la ruta de destino basado en los parámetros
if [ "$ENVIRONMENT" = "DES" ]; then
    if [ "$OS" = 'ANDROID' ]; then
        source_path="$HOME/Compartida/MobileApps/AppMovilCanales-apk"
        destination_path="$HOME/Desktop/des/apk"
    elif [ "$OS" = 'IOS' ]; then
        source_path="$HOME/Compartida/MobileApps/AppMovilCanales-ipa"
        destination_path="$HOME/Desktop/des/ipa"
    fi

elif [ "$ENVIRONMENT" = "CERT" ]; then
    if [ "$OS" = "ANDROID" ]; then
        source_path="$HOME/Compartida/MobileApps/AppMovilCanales-certi-android-wifi"
        destination_path="$HOME/Desktop/certi/android/wifi"
    elif [ "$OS" = "HUAWEI" ]; then
        source_path="$HOME/Compartida/MobileApps/AppMovilCanales-certi-huawei-wifi"
        destination_path="$HOME/Desktop/certi/huawei/wifi"
    elif [ "$OS" = 'IOS' ]; then
        source_path="$HOME/Compartida/MobileApps/AppMovilCanales-certi-ios-wifi"z
        destination_path="$HOME/Desktop/certi/ios/wifi"
    fi

elif [ "$ENVIRONMENT" = "PROD" ]; then
    if [ "$OS" = "ANDROID" ]; then
        source_path="$HOME/Compartida/MobileApps/AppMovilCanales-certi-android-prod"
        destination_path="$HOME/Desktop/certi/android/prod"
    elif [ "$OS" = "HUAWEI" ]; then
        source_path="$HOME/Compartida/MobileApps/AppMovilCanales-certi-huawei-prod"
        destination_path="$HOME/Desktop/certi/huawei/prod"
    elif [ "$OS" = 'IOS' ]; then
        source_path="$HOME/Compartida/MobileApps/AppMovilCanales-certi-ios-prod"
        destination_path="$HOME/Desktop/certi/ios/prod"
    fi
fi

# Navega al directorio del proyecto
cd "$source_path" || { echo "Error: No se pudo encontrar el directorio del proyecto." >&2; exit 1; }
echo "Ubicación actual: $(pwd)"
echo "Ubicación destino: $destination_path"

# Desbloquea el Keychain
echo "Desbloqueando el Keychain..."
if security unlock-keychain -p "$KEYCHAIN_PASSWORD" "$HOME/Library/Keychains/login.keychain-db"; then
    echo "Keychain desbloqueado correctamente."
else
    echo "Error al desbloquear el Keychain. Verifica tu contraseña." >&2
    exit 1
fi

# Obtiene las dependencias de Flutter
echo "Ejecutando 'flutter pub get'..."
$HOME/develop/flutter/bin/flutter pub get

# Compila la aplicación
if [ "$OS" = 'IOS' ]; then
    echo "Iniciando compilación del IPA en modo release"
    $HOME/develop/flutter/bin/flutter build ipa --release --obfuscate --split-debug-info=build/app/outputs/symbols --export-options-plist="ios/export_options.plist"
else 
    echo "Iniciando compilación del APK en modo release..."
    $HOME/develop/flutter/bin/flutter build apk --release --obfuscate --split-debug-info=build/app/outputs/symbols --no-shrink > $HOME/logs/build.log 2>&1
fi

# Verifica si la compilación fue exitosa
if [ $? -ne 0 ]; then
    echo "Error durante la compilación de Flutter. Revisa el log en $HOME/logs/build.log" >&2
    exit 1
fi

echo "Compilación completada exitosamente."

# --- 3. Copia del Artefacto (APK) O (IPA)---

# Crea el directorio de destino si no existe
echo "Creando directorio de destino si es necesario: $destination_path"
mkdir -p "$destination_path"

# Copia el archivo/carpeta

if [ "$OS" = 'IOS' ]; then 
    echo "Copiando a: $destination_path/$destination_dir"
    cp -R "$source_ipa" "$destination_path/$destination_dir"
else 
    echo "Copiando a: $destination_path/$destination_file"
    cp -R "$source_apk" "$destination_path/$destination_file"
fi

if [ $? -eq 0 ]; then
    echo "¡Proceso completado!  Se ha copiado correctamente."
else
    echo "Error al copiar." >&2
    exit 1
fi
