using System;
using System.Collections.Generic;
using System.IO;
using AddRemoveProgramsCleaner.Programs;
using Microsoft.Win32;

namespace AddRemoveProgramsCleaner {

    internal static class ProgramsCleaner {

        private const string DISPLAY_NAME        = "DisplayName";
        private const string DISPLAY_ICON        = "DisplayIcon";
        private const string SYSTEM_COMPONENT    = "SystemComponent";
        private const string INSTALL_LOCATION    = "InstallLocation";
        private const string UNINSTALL_STRING    = "UninstallString";
        private const string PRODUCT_NAME        = "ProductName";
        private const string PRODUCT_NAME_HIDDEN = "ProductName hidden by Ben";
        private const string PRODUCT_ICON        = "ProductIcon";

        private static void Main() {
            foreach (ProgramToClean program in ProgramsToClean.programsToClean) {
                IEnumerable<RegistryKey> keys = program.selector.openKey();
                foreach (RegistryKey key in keys) {
                    using (key) {
                        if (program.modifications.displayName is { } desiredDisplayName) {
                            string  displayNameName     = getName(key, DISPLAY_NAME, PRODUCT_NAME);
                            string? existingDisplayName = key.GetValue(displayNameName) as string;
                            if (existingDisplayName != desiredDisplayName) {
                                Console.WriteLine($"Setting {displayNameName} value of key {key} to {desiredDisplayName}");
                                key.SetValue(displayNameName, desiredDisplayName, RegistryValueKind.String);
                            }
                        }

                        if (program.modifications.displayIconGenerator is { } displayIconGenerator) {
                            string? installLocation          = key.GetValue(INSTALL_LOCATION) as string;
                            string? uninstallString          = key.GetValue(UNINSTALL_STRING) as string;
                            string? uninstallStringDirectory = Path.GetDirectoryName(uninstallString?.Trim('"'));
                            string? desiredDisplayIcon       = displayIconGenerator(installLocation, uninstallStringDirectory);
                            string  displayIconName          = getName(key, DISPLAY_ICON, PRODUCT_ICON);
                            string? existingDisplayIcon      = key.GetValue(displayIconName) as string;
                            if (existingDisplayIcon != desiredDisplayIcon && desiredDisplayIcon != null) {
                                Console.WriteLine($"Setting {displayIconName} value of key {key} to {desiredDisplayIcon}");
                                key.SetValue(displayIconName, desiredDisplayIcon, RegistryValueKind.String);
                            }
                        }

                        if (program.modifications.hide is { } desiredHidden) {
                            if (isInstallerProduct(key)) {
                                string? productName       = key.GetValue(PRODUCT_NAME) as string;
                                string? productNameHidden = key.GetValue(PRODUCT_NAME_HIDDEN) as string;
                                bool    existingHidden    = productName == null && productNameHidden != null;
                                if (desiredHidden && !existingHidden) {
                                    Console.WriteLine($"Setting {PRODUCT_NAME_HIDDEN} value of {key} to {productName}");
                                    key.SetValue(PRODUCT_NAME_HIDDEN, productName!, RegistryValueKind.String);
                                    key.DeleteValue(PRODUCT_NAME);
                                } else if (!desiredHidden && existingHidden) {
                                    Console.WriteLine($"Setting {PRODUCT_NAME} value of {key} to {productNameHidden}");
                                    key.SetValue(PRODUCT_NAME, productNameHidden!, RegistryValueKind.String);
                                    key.DeleteValue(PRODUCT_NAME_HIDDEN);
                                }
                            } else {
                                var existingHidden = key.GetValue(SYSTEM_COMPONENT) as int?;
                                if (existingHidden != (desiredHidden ? 1 : 0)) {
                                    Console.WriteLine($"Setting {SYSTEM_COMPONENT} value of key {key} to {(desiredHidden ? 1 : 0)}");
                                    key.SetValue(SYSTEM_COMPONENT, desiredHidden ? 1 : 0, RegistryValueKind.DWord);
                                    key.SetValue(SYSTEM_COMPONENT + " set by Ben", 1, RegistryValueKind.DWord);
                                }
                            }
                        }
                    }
                }
            }
        }

        private static string getName(RegistryKey key, string normalName, string productName) {
            return isInstallerProduct(key) ? productName : normalName;
        }

        private static bool isKeyParentOfChild(UninstallBaseKey parent, RegistryKey child) {
            return child.Name.StartsWith(parent.name() + @"\", StringComparison.InvariantCultureIgnoreCase);
        }

        private static bool isInstallerProduct(RegistryKey key) {
            return isKeyParentOfChild(UninstallBaseKey.CLASSES_ROOT_INSTALLER_PRODUCTS, key) ||
                isKeyParentOfChild(UninstallBaseKey.CURRENT_USER_INSTALLER_PRODUCTS, key);
        }

    }

}