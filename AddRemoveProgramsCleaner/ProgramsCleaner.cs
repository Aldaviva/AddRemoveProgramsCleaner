using System;
using System.Collections.Generic;
using System.IO;
using AddRemoveProgramsCleaner.Programs;
using AddRemoveProgramsCleaner.Registry;
using Microsoft.Win32;

namespace AddRemoveProgramsCleaner {

    internal static class ProgramsCleaner {

        private static void Main() {
            foreach (ProgramToClean program in ProgramsToClean.programsToClean) {
                IEnumerable<RegistryKey> keys = program.selector.openKey();
                foreach (RegistryKey key in keys) {
                    using (key) {
                        if (program.modifications.displayName is { } desiredDisplayName) {
                            string  displayNameName     = chooseValueName(key, RegistryConstants.DISPLAY_NAME, RegistryConstants.PRODUCT_NAME);
                            string? existingDisplayName = key.GetValue(displayNameName) as string;
                            if (existingDisplayName != desiredDisplayName) {
                                Console.WriteLine($"Setting {displayNameName} value of key {key} to {desiredDisplayName}");
                                key.SetValue(displayNameName, desiredDisplayName, RegistryValueKind.String);
                            }
                        }

                        if (program.modifications.displayIconGenerator is { } displayIconGenerator) {
                            string? installLocation          = key.GetValue(RegistryConstants.INSTALL_LOCATION) as string;
                            string? uninstallString          = key.GetValue(RegistryConstants.UNINSTALL_STRING) as string;
                            string? uninstallStringDirectory = Path.GetDirectoryName(uninstallString?.Trim('"'));
                            string? desiredDisplayIcon       = displayIconGenerator(installLocation, uninstallStringDirectory);
                            if (desiredDisplayIcon != null) {
                                desiredDisplayIcon = Environment.ExpandEnvironmentVariables(desiredDisplayIcon);
                            }

                            string  displayIconName     = chooseValueName(key, RegistryConstants.DISPLAY_ICON, RegistryConstants.PRODUCT_ICON);
                            string? existingDisplayIcon = key.GetValue(displayIconName) as string;
                            if (existingDisplayIcon != desiredDisplayIcon && desiredDisplayIcon != null) {
                                Console.WriteLine($"Setting {displayIconName} value of key {key} to {desiredDisplayIcon}");
                                key.SetValue(displayIconName, desiredDisplayIcon, RegistryValueKind.String);
                            }
                        }

                        if (program.modifications.hide is { } desiredHidden) {
                            if (RegistryHelpers.isInstallerProduct(key)) {
                                // Remove "ProductName" to hide Installer products
                                string? productName       = key.GetValue(RegistryConstants.PRODUCT_NAME) as string;
                                string? productNameHidden = key.GetValue(RegistryConstants.PRODUCT_NAME_HIDDEN) as string;
                                bool    existingHidden    = productName == null && productNameHidden != null;
                                if (desiredHidden && !existingHidden) {
                                    Console.WriteLine($"Setting {RegistryConstants.PRODUCT_NAME_HIDDEN} value of {key} to {productName}");
                                    key.SetValue(RegistryConstants.PRODUCT_NAME_HIDDEN, productName!, RegistryValueKind.String);
                                    key.DeleteValue(RegistryConstants.PRODUCT_NAME);
                                } else if (!desiredHidden && existingHidden) {
                                    Console.WriteLine($"Setting {RegistryConstants.PRODUCT_NAME} value of {key} to {productNameHidden}");
                                    key.SetValue(RegistryConstants.PRODUCT_NAME, productNameHidden!, RegistryValueKind.String);
                                    key.DeleteValue(RegistryConstants.PRODUCT_NAME_HIDDEN);
                                }
                            } else {
                                // Add "SystemComponent" = 1 to hide non-Installer products
                                int? existingHidden = key.GetValue(RegistryConstants.SYSTEM_COMPONENT) as int?;
                                if (existingHidden != (desiredHidden ? 1 : 0)) {
                                    Console.WriteLine($"Setting {RegistryConstants.SYSTEM_COMPONENT} value of key {key} to {(desiredHidden ? 1 : 0)}");
                                    key.SetValue(RegistryConstants.SYSTEM_COMPONENT, desiredHidden ? 1 : 0, RegistryValueKind.DWord);
                                    key.SetValue(RegistryConstants.SYSTEM_COMPONENT + " set by Ben", 1, RegistryValueKind.DWord);
                                }
                            }
                        }
                    }
                }
            }

            foreach (UninstallBaseKey baseKey in new[] {
                UninstallBaseKey.LOCAL_MACHINE_WOW6432NODE_UNINSTALL,
                UninstallBaseKey.LOCAL_MACHINE_UNINSTALL,
                UninstallBaseKey.CURRENT_USER_UNINSTALL
            }) {
                IconFinder.findAndAssignIcons(baseKey);
            }
        }

        private static string chooseValueName(RegistryKey key, string normalName, string productName) {
            return RegistryHelpers.isInstallerProduct(key) ? productName : normalName;
        }

    }

}