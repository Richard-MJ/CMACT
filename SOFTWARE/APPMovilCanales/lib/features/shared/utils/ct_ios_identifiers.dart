class CtIosIdentifiers {
  static String getIosModelName(String machineId) {
    switch (machineId) {
      // iPhone 17 Series
      case 'iPhone18,1':
        return 'iPhone 17 Pro';
      case 'iPhone18,2':
        return 'iPhone 17 Pro Max';
      case 'iPhone18,3':
        return 'iPhone 17';
      case 'iPhone18,4':
        return 'iPhone 17 Air';

      // iPhone 16 Series
      case 'iPhone17,1':
        return 'iPhone 16 Pro';
      case 'iPhone17,2':
        return 'iPhone 16 Pro Max';
      case 'iPhone17,3':
        return 'iPhone 16';
      case 'iPhone17,4':
        return 'iPhone 16 Plus';

      // iPhone 15 Series
      case 'iPhone16,1':
        return 'iPhone 15 Pro';
      case 'iPhone16,2':
        return 'iPhone 15 Pro Max';
      case 'iPhone15,4':
        return 'iPhone 15';
      case 'iPhone15,5':
        return 'iPhone 15 Plus';

      // iPhone 14 Series
      case 'iPhone15,2':
        return 'iPhone 14 Pro';
      case 'iPhone15,3':
        return 'iPhone 14 Pro Max';
      case 'iPhone14,7':
        return 'iPhone 14';
      case 'iPhone14,8':
        return 'iPhone 14 Plus';

      // iPhone SE (3rd generation)
      case 'iPhone14,6':
        return 'iPhone SE (3rd Gen)';

      // iPhone 13 Series
      case 'iPhone14,5':
        return 'iPhone 13';
      case 'iPhone14,4':
        return 'iPhone 13 mini';
      case 'iPhone14,2':
        return 'iPhone 13 Pro';
      case 'iPhone14,3':
        return 'iPhone 13 Pro Max';

      // iPhone 12 Series
      case 'iPhone13,2':
        return 'iPhone 12';
      case 'iPhone13,1':
        return 'iPhone 12 mini';
      case 'iPhone13,3':
        return 'iPhone 12 Pro';
      case 'iPhone13,4':
        return 'iPhone 12 Pro Max';

      // iPhone SE (2nd generation)
      case 'iPhone12,8':
        return 'iPhone SE (2nd Gen)';

      // iPhone 11 Series
      case 'iPhone12,1':
        return 'iPhone 11';
      case 'iPhone12,3':
        return 'iPhone 11 Pro';
      case 'iPhone12,5':
        return 'iPhone 11 Pro Max';

      // iPhone X Series
      case 'iPhone11,2':
        return 'iPhone XS';
      case 'iPhone11,4':
      case 'iPhone11,6':
        return 'iPhone XS Max';
      case 'iPhone11,8':
        return 'iPhone XR';
      case 'iPhone10,3':
      case 'iPhone10,6':
        return 'iPhone X';

      // iPhone 8 Series
      case 'iPhone10,1':
      case 'iPhone10,4':
        return 'iPhone 8';
      case 'iPhone10,2':
      case 'iPhone10,5':
        return 'iPhone 8 Plus';

      // iPhone 7 Series
      case 'iPhone9,1':
      case 'iPhone9,3':
        return 'iPhone 7';
      case 'iPhone9,2':
      case 'iPhone9,4':
        return 'iPhone 7 Plus';

      // iPhone 6s Series
      case 'iPhone8,1':
        return 'iPhone 6s';
      case 'iPhone8,2':
        return 'iPhone 6s Plus';
      case 'iPhone8,4':
        return 'iPhone SE';
      
      // iPad (Standard)
      case 'iPad2,1':
      case 'iPad2,2':
      case 'iPad2,3':
      case 'iPad2,4':
        return 'iPad 2';
      case 'iPad3,1':
      case 'iPad3,2':
      case 'iPad3,3':
        return 'iPad (3rd Gen)';
      case 'iPad3,4':
      case 'iPad3,5':
      case 'iPad3,6':
        return 'iPad (4th Gen)';
      case 'iPad6,11':
      case 'iPad6,12':
        return 'iPad (5th Gen)';
      case 'iPad7,5':
      case 'iPad7,6':
        return 'iPad (6th Gen)';
      case 'iPad7,11':
      case 'iPad7,12':
        return 'iPad (7th Gen)';
      case 'iPad11,6':
      case 'iPad11,7':
        return 'iPad (8th Gen)';
      case 'iPad12,1':
      case 'iPad12,2':
        return 'iPad (9th Gen)';
      case 'iPad13,18':
      case 'iPad13,19':
        return 'iPad (10th Gen)';
      case 'iPad15,1':
      case 'iPad15,7':
      case 'iPad15,8':
         return 'iPad (11th Gen)'; // 2025 release

      // iPad Air
      case 'iPad4,1':
      case 'iPad4,2':
      case 'iPad4,3':
        return 'iPad Air';
      case 'iPad5,3':
      case 'iPad5,4':
        return 'iPad Air 2';
      case 'iPad11,3':
      case 'iPad11,4':
        return 'iPad Air (3rd Gen)';
      
      // iPad Air (Re-mapping based on solid data + search results)
      case 'iPad13,1': 
        return 'iPad Air (4th Gen)'; 
      case 'iPad13,2':
        return 'iPad Air (4th Gen)';
      case 'iPad13,16':
      case 'iPad13,17':
        return 'iPad Air (5th Gen)';
      case 'iPad14,8':
      case 'iPad14,9':
        return 'iPad Air (6th Gen) 11-inch';
      case 'iPad14,10':
      case 'iPad14,11':
        return 'iPad Air (6th Gen) 13-inch';

      // iPad Mini
      case 'iPad2,5':
      case 'iPad2,6':
      case 'iPad2,7':
        return 'iPad mini';
      case 'iPad4,4':
      case 'iPad4,5':
      case 'iPad4,6':
        return 'iPad mini 2';
      case 'iPad4,7':
      case 'iPad4,8':
      case 'iPad4,9':
        return 'iPad mini 3';
      case 'iPad5,1':
      case 'iPad5,2':
        return 'iPad mini 4';
      case 'iPad11,1':
      case 'iPad11,2':
        return 'iPad mini (5th Gen)';
      case 'iPad14,1':
      case 'iPad14,2':
        return 'iPad mini (6th Gen)';
      case 'iPad16,1':
      case 'iPad16,2':
        return 'iPad mini (7th Gen)'; // 2024/2025

      // iPad Pro
      case 'iPad6,3':
      case 'iPad6,4':
        return 'iPad Pro 9.7-inch';
      case 'iPad6,7':
      case 'iPad6,8':
        return 'iPad Pro 12.9-inch (1st Gen)';
      case 'iPad7,1':
      case 'iPad7,2':
        return 'iPad Pro 12.9-inch (2nd Gen)';
      case 'iPad7,3':
      case 'iPad7,4':
        return 'iPad Pro 10.5-inch';
      case 'iPad8,1':
      case 'iPad8,2':
      case 'iPad8,3':
      case 'iPad8,4':
        return 'iPad Pro 11-inch (1st Gen)';
      case 'iPad8,5':
      case 'iPad8,6':
      case 'iPad8,7':
      case 'iPad8,8':
        return 'iPad Pro 12.9-inch (3rd Gen)';
      case 'iPad8,9':
      case 'iPad8,10':
        return 'iPad Pro 11-inch (2nd Gen)';
      case 'iPad8,11':
      case 'iPad8,12':
        return 'iPad Pro 12.9-inch (4th Gen)';
      case 'iPad13,4':
      case 'iPad13,5':
      case 'iPad13,6':
      case 'iPad13,7':
        return 'iPad Pro 11-inch (3rd Gen)';
      case 'iPad13,8':
      case 'iPad13,9':
      case 'iPad13,10':
      case 'iPad13,11':
        return 'iPad Pro 12.9-inch (5th Gen)';
      case 'iPad14,3':
      case 'iPad14,4':
        return 'iPad Pro 11-inch (4th Gen)';
      case 'iPad14,5':
      case 'iPad14,6':
        return 'iPad Pro 12.9-inch (6th Gen)';
      case 'iPad16,3':
      case 'iPad16,4':
        return 'iPad Pro 11-inch (M4)';
      case 'iPad16,5':
      case 'iPad16,6':
        return 'iPad Pro 13-inch (M4)';
      
      // Simulators
      case 'i386':
      case 'x86_64':
      case 'arm64':
        return 'Simulator';

      default:
        return machineId;
    }
  }
}
