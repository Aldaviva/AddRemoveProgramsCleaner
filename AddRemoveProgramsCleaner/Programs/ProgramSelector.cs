using System;
using System.Collections.Generic;
using System.Linq;
using DotNet.Globbing;
using DotNet.Globbing.Token;
using Microsoft.Win32;

namespace AddRemoveProgramsCleaner.Programs {

    public class ProgramSelector {

        public UninstallBaseKey baseKey { get; }
        public Glob keyName { get; }

        public ProgramSelector(UninstallBaseKey baseKey, Glob keyName) {
            this.baseKey = baseKey;
            this.keyName = keyName;
        }

        public IEnumerable<RegistryKey> openKey() {
            using RegistryKey baseKeyHandle = baseKey.openKey();
            if (baseKey == UninstallBaseKey.CLASSES_ROOT_INSTALLER_PRODUCTS || baseKey == UninstallBaseKey.CURRENT_USER_INSTALLER_PRODUCTS) {
                return baseKeyHandle.GetSubKeyNames()
                    .Select(subkey => baseKeyHandle.OpenSubKey(subkey, true))
                    .Where(subkey => keyName.ToString() == subkey?.GetValue("PackageCode") as string)
                    .Compact()
                    .ToList();
            } else if (isKeyNameLiteral()) {
                RegistryKey? subKey = baseKeyHandle.OpenSubKey(keyName.ToString(), true);
                return subKey != null ? new[] { subKey } : Enumerable.Empty<RegistryKey>();
            } else {
                return baseKeyHandle.GetSubKeyNames()
                    .Where(subkey => keyName.IsMatch(subkey))
                    .Select(subkey => baseKeyHandle.OpenSubKey(subkey, true))
                    .Compact()
                    .ToList();
            }
        }

        /// <returns><c>true</c> if none of the path components of <c>keyName</c> have wildcards (* or ?) in them, or <c>false</c> if one or more components do contain a wildcard.</returns>
        private bool isKeyNameLiteral() => keyName.Tokens.All(token => token is LiteralToken);

        public override string ToString() {
            return $"{nameof(baseKey)}: {baseKey}, {nameof(keyName)}: {keyName}";
        }

    }

    public enum UninstallBaseKey {

        LOCAL_MACHINE_UNINSTALL,
        CURRENT_USER_UNINSTALL,
        CLASSES_ROOT_INSTALLER_PRODUCTS,
        CURRENT_USER_INSTALLER_PRODUCTS,

        // ReSharper disable once InconsistentNaming - it's literally WOW6432NODE in the registry, not WOW6432_NODE
        LOCAL_MACHINE_WOW6432NODE_UNINSTALL

    }

    public static class UninstallBaseKeyExtensions {

        public static RegistryKey openKey(this UninstallBaseKey baseKey) {
            return baseKey switch {
                UninstallBaseKey.LOCAL_MACHINE_UNINSTALL             => Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Uninstall")!,
                UninstallBaseKey.CURRENT_USER_UNINSTALL              => Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Uninstall")!,
                UninstallBaseKey.CLASSES_ROOT_INSTALLER_PRODUCTS     => Registry.ClassesRoot.OpenSubKey(@"Installer\Products")!,
                UninstallBaseKey.LOCAL_MACHINE_WOW6432NODE_UNINSTALL => Registry.LocalMachine.OpenSubKey(@"Software\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall")!,
                UninstallBaseKey.CURRENT_USER_INSTALLER_PRODUCTS     => Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Installer\Products")!,
                _                                                    => throw new ArgumentOutOfRangeException(nameof(baseKey), baseKey, null)
            };
        }

        public static string name(this UninstallBaseKey baseKey) {
            using RegistryKey key = baseKey.openKey();
            return key.Name;
        }

    }

}