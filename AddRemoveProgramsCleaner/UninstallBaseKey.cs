using System;
using Microsoft.Win32;

namespace AddRemoveProgramsCleaner {

    public enum UninstallBaseKey {

        /// <summary>
        ///     <c>HKLM\Software\Microsoft\Windows\CurrentVersion\Uninstall</c>
        /// </summary>
        LOCAL_MACHINE_UNINSTALL,

        /// <summary>
        ///     <c>HKCU\Software\Microsoft\Windows\CurrentVersion\Uninstall</c>
        /// </summary>
        CURRENT_USER_UNINSTALL,

        /// <summary>
        ///     <c>HKCR\Installer\Products</c>
        /// </summary>
        CLASSES_ROOT_INSTALLER_PRODUCTS,

        /// <summary>
        ///     <c>HKCU\Software\Microsoft\Installer\Products</c>
        /// </summary>
        CURRENT_USER_INSTALLER_PRODUCTS,

        /// <summary>
        ///     <c>HKLM\Software\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall</c>
        /// </summary>
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