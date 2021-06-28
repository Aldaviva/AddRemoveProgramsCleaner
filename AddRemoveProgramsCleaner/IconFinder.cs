using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using AddRemoveProgramsCleaner.Registry;
using Microsoft.Win32;
using MoreLinq.Extensions;
using Workshell.PE;
using Workshell.PE.Resources;

namespace AddRemoveProgramsCleaner {

    public static class IconFinder {

        private static readonly IEnumerable<string> BLACKLISTED_ICON_EXES = new[] {
            "UnityCrashHandler64.exe",
            "vc_redist.x64.exe",
            "vc_redist.x86.exe",
            "vcredist_x64.exe",
            "vcredist_x86.exe",
            "dxwebsetup.exe",
            "dxsetup.exe",
            "QuickSFV.exe"
        }.Select(s => s.ToLowerInvariant());

        public static void findAndAssignIcons(UninstallBaseKey baseKey) {
            using RegistryKey parentKey = baseKey.openKey();

            foreach (string childKeyName in parentKey.GetSubKeyNames()) {
                using RegistryKey childKey = parentKey.OpenSubKey(childKeyName, true)!;
                if (childKey.GetValue(RegistryConstants.UNINSTALL_STRING) is string uninstallString) {
                    IEnumerable<string> uninstallParts = CommandLineParser.splitArgs(uninstallString).ToArray();

                    try {
                        string  uninstallerPath      = uninstallParts.First();
                        string? uninstallerDirectory = Path.GetDirectoryName(uninstallerPath);

                        if (!string.IsNullOrEmpty(uninstallerDirectory)
                            && childKey.GetValue(RegistryConstants.DISPLAY_NAME) is string
                            && !Path.GetFileName(uninstallerPath).Equals("msiexec.exe", StringComparison.InvariantCultureIgnoreCase)
                            && childKey.GetValue(RegistryConstants.RELEASE_TYPE) as string != "Update"
                            && childKey.GetValue(RegistryConstants.DISPLAY_ICON) == null) {

                            IEnumerable<string> exeFiles = Directory.EnumerateFiles(uninstallerDirectory, "*.exe", SearchOption.AllDirectories);

                            if (exeFiles.Where(exeFilename => Path.GetFullPath(exeFilename) != Path.GetFullPath(uninstallerPath)
                                    && !BLACKLISTED_ICON_EXES.Contains(Path.GetFileName(exeFilename).ToLowerInvariant())
                                    && hasIcons(exeFilename))
                                .MaxBy(exeFilename => new FileInfo(exeFilename).Length)
                                .FirstOrDefault() is { } largestNonUninstallerExeWithIcons) {

                                Console.WriteLine($"Setting DisplayIcon of {childKeyName} to {largestNonUninstallerExeWithIcons}");
                                childKey.SetValue(RegistryConstants.DISPLAY_ICON, largestNonUninstallerExeWithIcons);
                            }
                        }
                    } catch (ArgumentException e) {
                        Console.WriteLine($"Failed to find icon for {childKeyName}: {e.Message}");
                    }
                }
            }
        }

        private static bool hasIcons(string exeFilename) {
            using PortableExecutableImage exeFile = PortableExecutableImage.FromFile(exeFilename);
            try {
                ResourceCollection? resourceCollection = ResourceCollection.Get(exeFile);
                return resourceCollection?.Any(type => type.Id == ResourceType.GroupIcon) ?? false;
            } catch (TargetInvocationException) {
                Console.WriteLine($"Failed to parse icon resources in {exeFilename}");
                return false;
            }
        }

    }

}