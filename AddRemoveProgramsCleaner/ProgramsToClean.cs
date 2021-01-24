using System.Collections.Generic;
using System.IO;
using System.Linq;
using AddRemoveProgramsCleaner.Programs;
using Microsoft.Win32;

namespace AddRemoveProgramsCleaner {

    public static class ProgramsToClean {

        public static IEnumerable<ProgramToClean> programsToClean { get; } = new List<ProgramToClean> {
            new(UninstallBaseKey.LOCAL_MACHINE_WOW6432NODE_UNINSTALL, "DRWV_*", "Dreamweaver"),
            new(UninstallBaseKey.LOCAL_MACHINE_WOW6432NODE_UNINSTALL, "AEFT_*", "After Effects"),
            new(UninstallBaseKey.LOCAL_MACHINE_WOW6432NODE_UNINSTALL, "AME_*", "Media Encoder"),
            new(UninstallBaseKey.LOCAL_MACHINE_WOW6432NODE_UNINSTALL, "AUDT_*", "Audition"),
            new(UninstallBaseKey.LOCAL_MACHINE_WOW6432NODE_UNINSTALL, "IDSN_*", "InDesign"),
            new(UninstallBaseKey.LOCAL_MACHINE_WOW6432NODE_UNINSTALL, "ILST_*", "Illustrator"),
            new(UninstallBaseKey.LOCAL_MACHINE_WOW6432NODE_UNINSTALL, "KBRG_*", "Bridge"),
            new(UninstallBaseKey.LOCAL_MACHINE_WOW6432NODE_UNINSTALL, "LTRM_*", "Lightroom"),
            new(UninstallBaseKey.LOCAL_MACHINE_WOW6432NODE_UNINSTALL, "PHSP_*", "Photoshop"),
            new(UninstallBaseKey.LOCAL_MACHINE_WOW6432NODE_UNINSTALL, "PPRO_*", "Premiere"),
            new(UninstallBaseKey.LOCAL_MACHINE_WOW6432NODE_UNINSTALL, "Adobe Creative Cloud", "Creative Cloud"),
            new(UninstallBaseKey.CURRENT_USER_UNINSTALL, "7 Taskbar Tweaker", "7+ Taskbar Tweaker"),
            new(UninstallBaseKey.LOCAL_MACHINE_WOW6432NODE_UNINSTALL, "A-Tuning_is1", "A-Tuning"),
            new(UninstallBaseKey.LOCAL_MACHINE_UNINSTALL, "AutoHotkey", "AutoHotkey"),
            new(UninstallBaseKey.CURRENT_USER_UNINSTALL, "{9ba76717-9b50-413d-8747-a8087fb27523}", "Avidemux"),
            new(UninstallBaseKey.LOCAL_MACHINE_WOW6432NODE_UNINSTALL, "EOS Lens Registration Tool", "EOS Lens Registration"),
            new(UninstallBaseKey.LOCAL_MACHINE_WOW6432NODE_UNINSTALL, "EOS Utility 3", "EOS Utility"),
            new(UninstallBaseKey.LOCAL_MACHINE_UNINSTALL, "Cheat Engine_is1", "Cheat Engine"),
            new(UninstallBaseKey.LOCAL_MACHINE_WOW6432NODE_UNINSTALL, "Fraps", "Fraps", (_, uninstallStringDirectory) => joinPaths(uninstallStringDirectory, "fraps.exe")),
            new(UninstallBaseKey.LOCAL_MACHINE_UNINSTALL, "Git_is1", "Git"),
            new(UninstallBaseKey.LOCAL_MACHINE_UNINSTALL, "PROSet", "Intel Network Drivers"),
            new(UninstallBaseKey.LOCAL_MACHINE_UNINSTALL, "IrfanView64", "IrfanView"),
            new(UninstallBaseKey.CURRENT_USER_UNINSTALL, "{182ae02c-6f8d-5cb2-931c-7a9b69cbddee}", "ReSharper"),
            new(UninstallBaseKey.CURRENT_USER_UNINSTALL, new ProgramSelector(displayName: "JetBrains dotCover *"), "dotCover"),
            new(UninstallBaseKey.CURRENT_USER_UNINSTALL, new ProgramSelector(displayName: "JetBrains dotMemory *"), "dotMemory"),
            new(UninstallBaseKey.CURRENT_USER_UNINSTALL, new ProgramSelector(displayName: "JetBrains dotTrace *"), "dotTrace"),
            new(UninstallBaseKey.LOCAL_MACHINE_WOW6432NODE_UNINSTALL, "KDiff3", "KDiff3", (_, uninstallStringDirectory) => joinPaths(uninstallStringDirectory, "kdiff3.exe")),
            new(UninstallBaseKey.LOCAL_MACHINE_WOW6432NODE_UNINSTALL, "KeePassPasswordSafe2_is1", "KeePass"),
            new(UninstallBaseKey.LOCAL_MACHINE_UNINSTALL, "Launchy_*_is1", "Launchy"),
            new(UninstallBaseKey.LOCAL_MACHINE_UNINSTALL, "ProPlus2019Retail - en-us", "Office"),
            new(UninstallBaseKey.LOCAL_MACHINE_WOW6432NODE_UNINSTALL, "{ca67548a-5ebe-413a-b50c-4b9ceb6d66c6}", hide: true),
            new(UninstallBaseKey.LOCAL_MACHINE_WOW6432NODE_UNINSTALL, "{33d1fd90-4274-48a1-9bc1-97e33d9c2d6f}", hide: true),
            new(UninstallBaseKey.LOCAL_MACHINE_WOW6432NODE_UNINSTALL, "{ef6b00ec-13e1-4c25-9064-b2f383cb8412}", hide: true),
            new(UninstallBaseKey.LOCAL_MACHINE_WOW6432NODE_UNINSTALL, "{61087a79-ac85-455c-934d-1fa22cc64f36}", hide: true),
            new(UninstallBaseKey.LOCAL_MACHINE_WOW6432NODE_UNINSTALL, "{e31cb1a4-76b5-46a5-a084-3fa419e82201}", hide: true),
            new(UninstallBaseKey.LOCAL_MACHINE_WOW6432NODE_UNINSTALL, "{282975d8-55fe-4991-bbbb-06a72581ce58}", hide: true),
            new(UninstallBaseKey.LOCAL_MACHINE_UNINSTALL, "{6F320B93-EE3C-4826-85E0-ADF79F8D4C61}", "Visual Studio Installer"),
            new(UninstallBaseKey.LOCAL_MACHINE_WOW6432NODE_UNINSTALL, "Afterburner", "Afterburner"),
            new(UninstallBaseKey.LOCAL_MACHINE_UNINSTALL, "Notepad2", "Notepad2"),
            new(UninstallBaseKey.LOCAL_MACHINE_UNINSTALL, "{B2FE1952-0186-46C3-BAEC-A80AA35AC5B8}_Display.Driver", "Nvidia Graphics Driver",
                (_, _) => @"%ProgramFiles%\NVIDIA Corporation\Display.NvContainer\NVDisplay.Container.exe"),
            new(UninstallBaseKey.LOCAL_MACHINE_UNINSTALL, "{B2FE1952-0186-46C3-BAEC-A80AA35AC5B8}_HDAudio.Driver", "Nvidia Audio Driver",
                (_, _) => @"%ProgramFiles%\NVIDIA Corporation\Display.NvContainer\NVDisplay.Container.exe"),
            new(UninstallBaseKey.LOCAL_MACHINE_UNINSTALL, "{B2FE1952-0186-46C3-BAEC-A80AA35AC5B8}_Display.PhysX", "PhysX",
                (_, _) => @"%ProgramFiles%\NVIDIA Corporation\Display.NvContainer\NVDisplay.Container.exe"),
            new(UninstallBaseKey.LOCAL_MACHINE_UNINSTALL, "{B2FE1952-0186-46C3-BAEC-A80AA35AC5B8}_USBC", "Nvidia USB-C Driver",
                (_, _) => @"%ProgramFiles%\NVIDIA Corporation\Display.NvContainer\NVDisplay.Container.exe"),
            new(UninstallBaseKey.CURRENT_USER_UNINSTALL, "Fiddler2", "Fiddler"),
            new(UninstallBaseKey.CLASSES_ROOT_INSTALLER_PRODUCTS, "Progress® Telerik® JustMock *", "JustMock"),
            new(UninstallBaseKey.LOCAL_MACHINE_WOW6432NODE_UNINSTALL, "PS Tray Factory_is1", "PS Tray Factory"),
            new(UninstallBaseKey.LOCAL_MACHINE_UNINSTALL, "Sublime Text 3_is1", "Sublime Text"),
            new(UninstallBaseKey.LOCAL_MACHINE_UNINSTALL, "TagScanner_is1", "TagScanner"),
            new(UninstallBaseKey.LOCAL_MACHINE_WOW6432NODE_UNINSTALL, "Totalcmd", "Total Commander", (location,           _) => joinPaths(location, "TOTALCMD.EXE")),
            new(UninstallBaseKey.LOCAL_MACHINE_WOW6432NODE_UNINSTALL, "TreeSize Professional_is1", "TreeSize", (location, _) => joinPaths(location, "TreeSize.exe")),
            new(UninstallBaseKey.LOCAL_MACHINE_WOW6432NODE_UNINSTALL, "8dedcee8", "Visual Studio"),
            new(UninstallBaseKey.LOCAL_MACHINE_UNINSTALL, "VLC media player", "VLC"),
            new(UninstallBaseKey.LOCAL_MACHINE_UNINSTALL, "WinRAR archiver", "WinRAR"),
            new(UninstallBaseKey.LOCAL_MACHINE_WOW6432NODE_UNINSTALL, "winscp3_is1", "WinSCP"),
            new(UninstallBaseKey.LOCAL_MACHINE_UNINSTALL, "RolandRDID0117", "Quad-Capture"),
            new(UninstallBaseKey.LOCAL_MACHINE_UNINSTALL, "{521c89be-637f-4274-a840-baaf7460c2b2}", "Logitech G Hub"),
            new(UninstallBaseKey.CLASSES_ROOT_INSTALLER_PRODUCTS, "Microsoft .NET SDK 5.* from Visual Studio", ".NET 5 SDK",
                createNetCoreSdkIcon), // Microsoft .NET SDK 5.0.100 (x64) from Visual Studio
            new(UninstallBaseKey.LOCAL_MACHINE_WOW6432NODE_UNINSTALL, "Microsoft Edge", "Edge"),
            new(UninstallBaseKey.LOCAL_MACHINE_WOW6432NODE_UNINSTALL, "SCDNAS", "Shoutcast", getShoutcastIcon),
            new(UninstallBaseKey.LOCAL_MACHINE_UNINSTALL, "Autodesk Inventor *", "Inventor"),
            new(UninstallBaseKey.CLASSES_ROOT_INSTALLER_PRODUCTS, "Autodesk Material Library 2021", hide: true),
            new(UninstallBaseKey.CLASSES_ROOT_INSTALLER_PRODUCTS, "Autodesk Material Library Base Resolution Image Library 2021", hide: true),
            new(UninstallBaseKey.CLASSES_ROOT_INSTALLER_PRODUCTS, "Autodesk Material Library Low Resolution Image Library 2021", hide: true),
            new(UninstallBaseKey.CLASSES_ROOT_INSTALLER_PRODUCTS, new ProgramSelector(displayName: "Microsoft XNA Framework Redistributable * Refresh"), "XNA"),
            new(UninstallBaseKey.LOCAL_MACHINE_UNINSTALL, "utvideo_is1", "Ut Video"),
            new(UninstallBaseKey.CLASSES_ROOT_INSTALLER_PRODUCTS, new ProgramSelector(displayName: "Epic Games Launcher"), "Epic Games"),
            new(UninstallBaseKey.LOCAL_MACHINE_WOW6432NODE_UNINSTALL, "AMD_Chipset_IODrivers", "AMD Chipset")
        };

        private static string? getShoutcastIcon(string? installLocation, string? uninstallStringDirectory) {
            if (Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall\Winamp", "DisplayIcon", null) is string winampIcon) {
                return Path.Combine(Path.GetDirectoryName(winampIcon.Split(',', 2)[0])!, @"Plugins\dsp_sc.dll") + ",0";
            }

            return null;
        }

        private static string? joinPaths(params string?[] paths) {
            return paths.All(path => path != null) ? Path.Combine(paths!) : null;
        }

        private static string? createNetCoreSdkIcon(string? location, string? directory) {
            using RegistryKey? installedVersions = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\dotnet\Setup\InstalledVersions\x64");
            if (installedVersions?.GetValue("InstallLocation") is string installLocation) {
                string iconFilePath = joinPaths(installLocation, "dotnet.ico")!;

                if (!File.Exists(iconFilePath)) {
                    File.WriteAllBytes(iconFilePath, Resources.dotnetIcon);
                }

                return iconFilePath;
            }

            return null;
        }

    }

}