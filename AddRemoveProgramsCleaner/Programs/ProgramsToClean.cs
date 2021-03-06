﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using AddRemoveProgramsCleaner.Registry;
using Microsoft.Win32;

namespace AddRemoveProgramsCleaner.Programs {

    /// <remarks>
    ///     If you're searching for the key using Registry Finder (https://registry-finder.com), it can be useful to search by Key:
    ///     <c>
    ///         HKEY_CLASSES_ROOT\Installer\Products, HKEY_LOCAL_MACHINE\Software\Microsoft\Windows\CurrentVersion\Uninstall, HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Uninstall, HKEY_LOCAL_MACHINE\Software\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall, HKEY_CURRENT_USER\Software\Microsoft\Installer\Products
    ///     </c>
    /// </remarks>
    public static class ProgramsToClean {

        public static IEnumerable<ProgramToClean> programsToClean { get; } = new List<ProgramToClean> {
            new(baseKey: UninstallBaseKey.LOCAL_MACHINE_WOW6432NODE_UNINSTALL, selector: new ProgramSelector(keyName: "DRWV_*"), setDisplayNameTo: "Dreamweaver"),
            new(baseKey: UninstallBaseKey.LOCAL_MACHINE_WOW6432NODE_UNINSTALL, selector: new ProgramSelector(keyName: "AEFT_*"), setDisplayNameTo: "After Effects"),
            new(baseKey: UninstallBaseKey.LOCAL_MACHINE_WOW6432NODE_UNINSTALL, selector: new ProgramSelector(keyName: "AME_*"), setDisplayNameTo: "Media Encoder"),
            new(baseKey: UninstallBaseKey.LOCAL_MACHINE_WOW6432NODE_UNINSTALL, selector: new ProgramSelector(keyName: "AUDT_*"), setDisplayNameTo: "Audition"),
            new(baseKey: UninstallBaseKey.LOCAL_MACHINE_WOW6432NODE_UNINSTALL, selector: new ProgramSelector(keyName: "IDSN_*"), setDisplayNameTo: "InDesign"),
            new(baseKey: UninstallBaseKey.LOCAL_MACHINE_WOW6432NODE_UNINSTALL, selector: new ProgramSelector(keyName: "ILST_*"), setDisplayNameTo: "Illustrator"),
            new(baseKey: UninstallBaseKey.LOCAL_MACHINE_WOW6432NODE_UNINSTALL, selector: new ProgramSelector(keyName: "KBRG_*"), setDisplayNameTo: "Bridge"),
            new(baseKey: UninstallBaseKey.LOCAL_MACHINE_WOW6432NODE_UNINSTALL, selector: new ProgramSelector(keyName: "LTRM_*"), setDisplayNameTo: "Lightroom"),
            new(baseKey: UninstallBaseKey.LOCAL_MACHINE_WOW6432NODE_UNINSTALL, selector: new ProgramSelector(keyName: "PHSP_*"), setDisplayNameTo: "Photoshop"),
            new(baseKey: UninstallBaseKey.LOCAL_MACHINE_WOW6432NODE_UNINSTALL, selector: new ProgramSelector(keyName: "PPRO_*"), setDisplayNameTo: "Premiere"),
            new(baseKey: UninstallBaseKey.LOCAL_MACHINE_WOW6432NODE_UNINSTALL, selector: new ProgramSelector(keyName: "Adobe Creative Cloud"), setDisplayNameTo: "Creative Cloud"),
            new(baseKey: UninstallBaseKey.LOCAL_MACHINE_WOW6432NODE_UNINSTALL, selector: new ProgramSelector(keyName: "AdobeGenuineService"), hide: true),
            new(baseKey: UninstallBaseKey.CURRENT_USER_UNINSTALL, selector: new ProgramSelector(keyName: "7 Taskbar Tweaker"), setDisplayNameTo: "7+ Taskbar Tweaker"),
            new(baseKey: UninstallBaseKey.LOCAL_MACHINE_WOW6432NODE_UNINSTALL, selector: new ProgramSelector(keyName: "A-Tuning_is1"), setDisplayNameTo: "A-Tuning"),
            new(baseKey: UninstallBaseKey.LOCAL_MACHINE_UNINSTALL, selector: new ProgramSelector(keyName: "AutoHotkey"), setDisplayNameTo: "AutoHotkey"),
            new(baseKey: UninstallBaseKey.CURRENT_USER_UNINSTALL, selector: new ProgramSelector(keyName: "{9ba76717-9b50-413d-8747-a8087fb27523}"), setDisplayNameTo: "Avidemux"),
            new(baseKey: UninstallBaseKey.LOCAL_MACHINE_WOW6432NODE_UNINSTALL, selector: new ProgramSelector(keyName: "EOS Lens Registration Tool"), hide: true),
            new(baseKey: UninstallBaseKey.LOCAL_MACHINE_WOW6432NODE_UNINSTALL, selector: new ProgramSelector(keyName: "EOS Utility 3"), setDisplayNameTo: "EOS Utility"),
            new(baseKey: UninstallBaseKey.LOCAL_MACHINE_WOW6432NODE_UNINSTALL, selector: new ProgramSelector(keyName: "EOS Utility 2"), hide: true),
            new(baseKey: UninstallBaseKey.LOCAL_MACHINE_WOW6432NODE_UNINSTALL, selector: new ProgramSelector(keyName: "EOS Network Setting Tool"), hide: true),
            new(baseKey: UninstallBaseKey.LOCAL_MACHINE_WOW6432NODE_UNINSTALL, selector: new ProgramSelector(keyName: "EOS Web Service Registration Tool"), hide: true),
            new(baseKey: UninstallBaseKey.LOCAL_MACHINE_UNINSTALL, selector: new ProgramSelector(keyName: "Cheat Engine_is1"), setDisplayNameTo: "Cheat Engine"),
            new(baseKey: UninstallBaseKey.LOCAL_MACHINE_WOW6432NODE_UNINSTALL, selector: new ProgramSelector(keyName: "Fraps"), setDisplayNameTo: "Fraps",
                setDisplayIconUsing: (_, uninstallStringDirectory) => joinPaths(uninstallStringDirectory, "fraps.exe")),
            new(baseKey: UninstallBaseKey.LOCAL_MACHINE_UNINSTALL, selector: new ProgramSelector(keyName: "Git_is1"), setDisplayNameTo: "Git"),
            new(baseKey: UninstallBaseKey.LOCAL_MACHINE_UNINSTALL, selector: new ProgramSelector(keyName: "PROSet"), setDisplayNameTo: "Intel Network Drivers"),
            new(baseKey: UninstallBaseKey.LOCAL_MACHINE_UNINSTALL, selector: new ProgramSelector(keyName: "IrfanView64"), setDisplayNameTo: "IrfanView"),
            new(baseKey: UninstallBaseKey.CURRENT_USER_UNINSTALL, selector: new ProgramSelector(keyName: "{182ae02c-6f8d-5cb2-931c-7a9b69cbddee}"), setDisplayNameTo: "ReSharper"),
            new(baseKey: UninstallBaseKey.CURRENT_USER_UNINSTALL, selector: new ProgramSelector(displayName: "JetBrains dotCover *"), setDisplayNameTo: "dotCover"),
            new(baseKey: UninstallBaseKey.CURRENT_USER_UNINSTALL, selector: new ProgramSelector(displayName: "JetBrains dotMemory *"), setDisplayNameTo: "dotMemory"),
            new(baseKey: UninstallBaseKey.CURRENT_USER_UNINSTALL, selector: new ProgramSelector(displayName: "JetBrains dotTrace *"), setDisplayNameTo: "dotTrace"),
            new(baseKey: UninstallBaseKey.LOCAL_MACHINE_WOW6432NODE_UNINSTALL, selector: new ProgramSelector(keyName: "KDiff3"), setDisplayNameTo: "KDiff3",
                setDisplayIconUsing: (_, uninstallStringDirectory) => joinPaths(uninstallStringDirectory, "kdiff3.exe")),
            new(baseKey: UninstallBaseKey.LOCAL_MACHINE_WOW6432NODE_UNINSTALL, selector: new ProgramSelector(keyName: "KeePassPasswordSafe2_is1"), setDisplayNameTo: "KeePass"),
            new(baseKey: UninstallBaseKey.LOCAL_MACHINE_UNINSTALL, selector: new ProgramSelector(keyName: "Launchy_*_is1"), setDisplayNameTo: "Launchy"),
            new(baseKey: UninstallBaseKey.LOCAL_MACHINE_UNINSTALL, selector: new ProgramSelector(keyName: "ProPlus2019Retail - en-us"), setDisplayNameTo: "Office"),
            new(baseKey: UninstallBaseKey.LOCAL_MACHINE_WOW6432NODE_UNINSTALL, selector: new ProgramSelector(keyName: "{ca67548a-5ebe-413a-b50c-4b9ceb6d66c6}"), hide: true),
            new(baseKey: UninstallBaseKey.LOCAL_MACHINE_WOW6432NODE_UNINSTALL, selector: new ProgramSelector(keyName: "{33d1fd90-4274-48a1-9bc1-97e33d9c2d6f}"), hide: true),
            new(baseKey: UninstallBaseKey.LOCAL_MACHINE_WOW6432NODE_UNINSTALL, selector: new ProgramSelector(keyName: "{ef6b00ec-13e1-4c25-9064-b2f383cb8412}"), hide: true),
            new(baseKey: UninstallBaseKey.LOCAL_MACHINE_WOW6432NODE_UNINSTALL, selector: new ProgramSelector(keyName: "{61087a79-ac85-455c-934d-1fa22cc64f36}"), hide: true),
            new(baseKey: UninstallBaseKey.LOCAL_MACHINE_WOW6432NODE_UNINSTALL, selector: new ProgramSelector(keyName: "{e31cb1a4-76b5-46a5-a084-3fa419e82201}"), hide: true),
            new(baseKey: UninstallBaseKey.LOCAL_MACHINE_WOW6432NODE_UNINSTALL, selector: new ProgramSelector(keyName: "{282975d8-55fe-4991-bbbb-06a72581ce58}"), hide: true),
            new(baseKey: UninstallBaseKey.LOCAL_MACHINE_UNINSTALL, selector: new ProgramSelector(keyName: "{6F320B93-EE3C-4826-85E0-ADF79F8D4C61}"), setDisplayNameTo: "Visual Studio Installer"),
            new(baseKey: UninstallBaseKey.LOCAL_MACHINE_WOW6432NODE_UNINSTALL, selector: new ProgramSelector(keyName: "Afterburner"), setDisplayNameTo: "Afterburner"),
            new(baseKey: UninstallBaseKey.LOCAL_MACHINE_UNINSTALL, selector: new ProgramSelector(keyName: "Notepad2"), setDisplayNameTo: "Notepad2"),
            new(baseKey: UninstallBaseKey.LOCAL_MACHINE_UNINSTALL, selector: new ProgramSelector(keyName: "{B2FE1952-0186-46C3-BAEC-A80AA35AC5B8}_Display.Driver"),
                setDisplayNameTo: "Nvidia Graphics Driver",
                setDisplayIconUsing: (_, _) => @"%ProgramFiles%\NVIDIA Corporation\Display.NvContainer\NVDisplay.Container.exe"),
            new(baseKey: UninstallBaseKey.LOCAL_MACHINE_UNINSTALL, selector: new ProgramSelector(keyName: "{B2FE1952-0186-46C3-BAEC-A80AA35AC5B8}_HDAudio.Driver"),
                setDisplayNameTo: "Nvidia Audio Driver",
                setDisplayIconUsing: (_, _) => @"%ProgramFiles%\NVIDIA Corporation\Display.NvContainer\NVDisplay.Container.exe"),
            new(baseKey: UninstallBaseKey.LOCAL_MACHINE_UNINSTALL, selector: new ProgramSelector(keyName: "{B2FE1952-0186-46C3-BAEC-A80AA35AC5B8}_Display.PhysX"), setDisplayNameTo: "PhysX",
                setDisplayIconUsing: (_, _) => @"%ProgramFiles%\NVIDIA Corporation\Display.NvContainer\NVDisplay.Container.exe"),
            new(baseKey: UninstallBaseKey.LOCAL_MACHINE_UNINSTALL, selector: new ProgramSelector(keyName: "{B2FE1952-0186-46C3-BAEC-A80AA35AC5B8}_USBC"), setDisplayNameTo: "Nvidia USB-C Driver",
                setDisplayIconUsing: (_, _) => @"%ProgramFiles%\NVIDIA Corporation\Display.NvContainer\NVDisplay.Container.exe"),
            new(baseKey: UninstallBaseKey.CURRENT_USER_UNINSTALL, selector: new ProgramSelector(keyName: "Fiddler2"), setDisplayNameTo: "Fiddler"),
            new(baseKey: UninstallBaseKey.CLASSES_ROOT_INSTALLER_PRODUCTS, selector: new ProgramSelector(displayName: "Progress® Telerik® JustMock *"), setDisplayNameTo: "JustMock"),
            new(baseKey: UninstallBaseKey.LOCAL_MACHINE_WOW6432NODE_UNINSTALL, selector: new ProgramSelector(keyName: "PS Tray Factory_is1"), setDisplayNameTo: "PS Tray Factory"),
            new(baseKey: UninstallBaseKey.LOCAL_MACHINE_UNINSTALL, selector: new ProgramSelector(keyName: "Sublime Text 3_is1"), setDisplayNameTo: "Sublime Text"),
            new(baseKey: UninstallBaseKey.LOCAL_MACHINE_UNINSTALL, selector: new ProgramSelector(keyName: "TagScanner_is1"), setDisplayNameTo: "TagScanner"),
            new(baseKey: UninstallBaseKey.LOCAL_MACHINE_WOW6432NODE_UNINSTALL, selector: new ProgramSelector(keyName: "Totalcmd"), setDisplayNameTo: "Total Commander",
                setDisplayIconUsing: (location, _) => joinPaths(location, "TOTALCMD.EXE")),
            new(baseKey: UninstallBaseKey.LOCAL_MACHINE_WOW6432NODE_UNINSTALL, selector: new ProgramSelector(keyName: "TreeSize Professional_is1"), setDisplayNameTo: "TreeSize",
                setDisplayIconUsing: (location, _) => joinPaths(location, "TreeSize.exe")),
            new(baseKey: UninstallBaseKey.LOCAL_MACHINE_WOW6432NODE_UNINSTALL, selector: new ProgramSelector(keyName: "8dedcee8"), setDisplayNameTo: "Visual Studio"),
            new(baseKey: UninstallBaseKey.LOCAL_MACHINE_UNINSTALL, selector: new ProgramSelector(keyName: "VLC media player"), setDisplayNameTo: "VLC"),
            new(baseKey: UninstallBaseKey.LOCAL_MACHINE_UNINSTALL, selector: new ProgramSelector(keyName: "WinRAR archiver"), setDisplayNameTo: "WinRAR"),
            new(baseKey: UninstallBaseKey.LOCAL_MACHINE_WOW6432NODE_UNINSTALL, selector: new ProgramSelector(keyName: "winscp3_is1"), setDisplayNameTo: "WinSCP"),
            new(baseKey: UninstallBaseKey.LOCAL_MACHINE_UNINSTALL, selector: new ProgramSelector(keyName: "RolandRDID0117"), setDisplayNameTo: "Quad-Capture"),
            new(baseKey: UninstallBaseKey.LOCAL_MACHINE_UNINSTALL, selector: new ProgramSelector(keyName: "{521c89be-637f-4274-a840-baaf7460c2b2}"), setDisplayNameTo: "G Hub"),
            new(baseKey: UninstallBaseKey.CLASSES_ROOT_INSTALLER_PRODUCTS, selector: new ProgramSelector(displayName: "Microsoft .NET SDK 5.* from Visual Studio"), setDisplayNameTo: ".NET 5 SDK",
                setDisplayIconUsing: createNetCoreSdkIcon), // Microsoft .NET SDK 5.0.100 (x64) from Visual Studio
            new(baseKey: UninstallBaseKey.LOCAL_MACHINE_WOW6432NODE_UNINSTALL, selector: new ProgramSelector(keyName: "Microsoft Edge"), setDisplayNameTo: "Edge"),
            new(baseKey: UninstallBaseKey.LOCAL_MACHINE_WOW6432NODE_UNINSTALL, selector: new ProgramSelector(keyName: "SCDNAS"), setDisplayNameTo: "Shoutcast", setDisplayIconUsing: getShoutcastIcon),
            new(baseKey: UninstallBaseKey.LOCAL_MACHINE_UNINSTALL, selector: new ProgramSelector(keyName: "Autodesk Inventor *"), setDisplayNameTo: "Inventor"),
            new(baseKey: UninstallBaseKey.CLASSES_ROOT_INSTALLER_PRODUCTS, selector: new ProgramSelector(displayName: "Autodesk Material Library 2021"), hide: true),
            new(baseKey: UninstallBaseKey.CLASSES_ROOT_INSTALLER_PRODUCTS, selector: new ProgramSelector(displayName: "Autodesk Material Library Base Resolution Image Library 2021"), hide: true),
            new(baseKey: UninstallBaseKey.CLASSES_ROOT_INSTALLER_PRODUCTS, selector: new ProgramSelector(displayName: "Autodesk Material Library Low Resolution Image Library 2021"), hide: true),
            new(baseKey: UninstallBaseKey.CLASSES_ROOT_INSTALLER_PRODUCTS, selector: new ProgramSelector(displayName: "Microsoft XNA Framework Redistributable * Refresh"), setDisplayNameTo: "XNA"),
            new(baseKey: UninstallBaseKey.LOCAL_MACHINE_UNINSTALL, selector: new ProgramSelector(keyName: "utvideo_is1"), setDisplayNameTo: "Ut Video"),
            new(baseKey: UninstallBaseKey.CLASSES_ROOT_INSTALLER_PRODUCTS, selector: new ProgramSelector(displayName: "Epic Games Launcher"), setDisplayNameTo: "Epic Games"),
            new(baseKey: UninstallBaseKey.LOCAL_MACHINE_WOW6432NODE_UNINSTALL, selector: new ProgramSelector(keyName: "AMD_Chipset_IODrivers"), setDisplayNameTo: "AMD Chipset"),
            new(baseKey: UninstallBaseKey.CLASSES_ROOT_INSTALLER_PRODUCTS, selector: new ProgramSelector(displayName: "ESET Security"), setDisplayNameTo: "NOD32"),
            new(baseKey: UninstallBaseKey.CLASSES_ROOT_INSTALLER_PRODUCTS, selector: new ProgramSelector(displayName: "Adobe Acrobat DC"), setDisplayNameTo: "Acrobat"),
            new(baseKey: UninstallBaseKey.CLASSES_ROOT_INSTALLER_PRODUCTS, selector: new ProgramSelector(displayName: "Microsoft Update Health Tools"), hide: true),
            new(baseKey: UninstallBaseKey.CLASSES_ROOT_INSTALLER_PRODUCTS, selector: new ProgramSelector(displayName: "Intel(R) C++ Redistributables on Intel(R) 64"), hide: true),
            new(baseKey: UninstallBaseKey.LOCAL_MACHINE_UNINSTALL, selector: new ProgramSelector(keyName: "Maxon Cinema 4D S22"), hide: true),
            new(baseKey: UninstallBaseKey.LOCAL_MACHINE_WOW6432NODE_UNINSTALL, selector: new ProgramSelector(displayName: "Microsoft Visual C++ 2013 Redistributable (x*"), hide: true),
        };

        private static string? getShoutcastIcon(string? installLocation, string? uninstallStringDirectory) {
            if (Microsoft.Win32.Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall\Winamp", "DisplayIcon", null) is string winampIcon) {
                return Path.Combine(Path.GetDirectoryName(winampIcon.Split(',', 2)[0])!, @"Plugins\dsp_sc.dll") + ",0";
            }

            return null;
        }

        private static string? joinPaths(params string?[] paths) {
            return paths.All(path => path != null) ? Path.Combine(paths!) : null;
        }

        private static string? createNetCoreSdkIcon(string? location, string? directory) {
            using RegistryKey? installedVersions = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\dotnet\Setup\InstalledVersions\x64");
            if (installedVersions?.GetValue("InstallLocation") is string installLocation) {
                string iconFilePath = joinPaths(installLocation, "dotnet.ico")!;

                if (!File.Exists(iconFilePath)) {
                    File.WriteAllBytes(iconFilePath, Resources.Resources.dotnetIcon);
                }

                return iconFilePath;
            }

            return null;
        }

    }

}