using System;
using System.Linq;
using Microsoft.Win32;

namespace AddRemoveProgramsCleaner.Registry; 

public static class RegistryHelpers {

    public static string? getDisplayName(RegistryKey key) {
        if (isInstallerProduct(key)) {
            return key.GetValue(RegistryConstants.PRODUCT_NAME) as string ?? key.GetValue(RegistryConstants.PRODUCT_NAME_HIDDEN) as string;
        } else {
            return key.GetValue(RegistryConstants.DISPLAY_NAME) as string;
        }
    }

    public static bool isInstallerProduct(RegistryKey key) {
        return new[] {
            UninstallBaseKey.CLASSES_ROOT_INSTALLER_PRODUCTS,
            // UninstallBaseKey.CURRENT_USER_INSTALLER_PRODUCTS
        }.Any(baseKey => key.Name.StartsWith(baseKey.name() + '\\', StringComparison.InvariantCultureIgnoreCase));
    }

}