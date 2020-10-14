using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AddRemoveProgramsCleaner.Programs;
using Microsoft.Win32;

namespace AddRemoveProgramsCleaner {

    public static class ProgramsToClean {

        public static IEnumerable<ProgramToClean> programsToClean { get; } = new List<ProgramToClean> {
            new ProgramToClean(UninstallBaseKey.LOCAL_MACHINE_WOW6432NODE_UNINSTALL, "DRWV_*", "Dreamweaver"),
            new ProgramToClean(UninstallBaseKey.LOCAL_MACHINE_WOW6432NODE_UNINSTALL, "AEFT_*", "After Effects"),
            new ProgramToClean(UninstallBaseKey.LOCAL_MACHINE_WOW6432NODE_UNINSTALL, "AME_*", "Media Encoder"),
            new ProgramToClean(UninstallBaseKey.LOCAL_MACHINE_WOW6432NODE_UNINSTALL, "AUDT_*", "Audition"),
            new ProgramToClean(UninstallBaseKey.LOCAL_MACHINE_WOW6432NODE_UNINSTALL, "IDSN_*", "InDesign"),
            new ProgramToClean(UninstallBaseKey.LOCAL_MACHINE_WOW6432NODE_UNINSTALL, "ILST_*", "Illustrator"),
            new ProgramToClean(UninstallBaseKey.LOCAL_MACHINE_WOW6432NODE_UNINSTALL, "KBRG_*", "Bridge"),
            new ProgramToClean(UninstallBaseKey.LOCAL_MACHINE_WOW6432NODE_UNINSTALL, "LTRM_*", "Lightroom"),
            new ProgramToClean(UninstallBaseKey.LOCAL_MACHINE_WOW6432NODE_UNINSTALL, "PHSP_*", "Photoshop"),
            new ProgramToClean(UninstallBaseKey.LOCAL_MACHINE_WOW6432NODE_UNINSTALL, "PPRO_*", "Premiere"),
            new ProgramToClean(UninstallBaseKey.LOCAL_MACHINE_WOW6432NODE_UNINSTALL, "Adobe Creative Cloud", "Creative Cloud"),
            new ProgramToClean(UninstallBaseKey.CURRENT_USER_UNINSTALL, "7 Taskbar Tweaker", "7+ Taskbar Tweaker"),
            new ProgramToClean(UninstallBaseKey.CLASSES_ROOT_INSTALLER_PRODUCTS, "52869F3B3CDBC4546967706BAD209ECF", "Acrobat"),
            new ProgramToClean(UninstallBaseKey.CLASSES_ROOT_INSTALLER_PRODUCTS, "B78766D5FDF66214D88EB8CFA618E424", "Java"),
            new ProgramToClean(UninstallBaseKey.CLASSES_ROOT_INSTALLER_PRODUCTS, "030A37AA92C7DAB499A771749B347954", "XMLSpy"),
            new ProgramToClean(UninstallBaseKey.LOCAL_MACHINE_WOW6432NODE_UNINSTALL, "art of rally_is1", "Art of Rally",
                (installLocation, uninstallStringDirectory) => joinPaths(installLocation, "artofrally.exe")),
            new ProgramToClean(UninstallBaseKey.LOCAL_MACHINE_WOW6432NODE_UNINSTALL, "A-Tuning_is1", "A-Tuning"),
            new ProgramToClean(UninstallBaseKey.LOCAL_MACHINE_UNINSTALL, "AutoHotkey", "AutoHotkey"),
            new ProgramToClean(UninstallBaseKey.CURRENT_USER_UNINSTALL, "{9ba76717-9b50-413d-8747-a8087fb27523}", "Avidemux"),
            new ProgramToClean(UninstallBaseKey.LOCAL_MACHINE_WOW6432NODE_UNINSTALL, "EOS Lens Registration Tool", "EOS Lens Registration"),
            new ProgramToClean(UninstallBaseKey.LOCAL_MACHINE_WOW6432NODE_UNINSTALL, "EOS Utility 3", "EOS Utility"),
            new ProgramToClean(UninstallBaseKey.LOCAL_MACHINE_UNINSTALL, "Cheat Engine_is1", "Cheat Engine"),
            new ProgramToClean(UninstallBaseKey.LOCAL_MACHINE_WOW6432NODE_UNINSTALL, "Dakar 18 Desafio Ruta 40 Rally_is1", "Dakar 18",
                (location, uninstallString) => joinPaths(location, "Dakar18Game.exe")),
            new ProgramToClean(UninstallBaseKey.CLASSES_ROOT_INSTALLER_PRODUCTS, "69720111BDA03144F9A2958287FA0021", "NOD32"),
            new ProgramToClean(UninstallBaseKey.LOCAL_MACHINE_WOW6432NODE_UNINSTALL, "Fraps", "Fraps", (location, uninstallStringDirectory) => joinPaths(uninstallStringDirectory, "fraps.exe")),
            new ProgramToClean(UninstallBaseKey.CLASSES_ROOT_INSTALLER_PRODUCTS, "BE82DA964FFDEC540BD053A5ADF4601E", "Git Extensions"),
            new ProgramToClean(UninstallBaseKey.LOCAL_MACHINE_UNINSTALL, "Git_is1", "Git"),
            new ProgramToClean(UninstallBaseKey.CLASSES_ROOT_INSTALLER_PRODUCTS, "5945BCC491DF14C499846B99372877EC", hide: true),
            new ProgramToClean(UninstallBaseKey.LOCAL_MACHINE_UNINSTALL, "PROSet", "Intel Network Drivers"),
            new ProgramToClean(UninstallBaseKey.LOCAL_MACHINE_UNINSTALL, "IrfanView64", "IrfanView"),
            new ProgramToClean(UninstallBaseKey.CURRENT_USER_UNINSTALL, "{c88b025a-3cf3-5b2f-b245-4f1382e652bc}", "dotCover"),
            new ProgramToClean(UninstallBaseKey.CURRENT_USER_UNINSTALL, "{182ae02c-6f8d-5cb2-931c-7a9b69cbddee}", "ReSharper"),
            new ProgramToClean(UninstallBaseKey.CURRENT_USER_UNINSTALL, "{2674a135-2851-5eca-a325-db1253d93dee}", "dotMemory"),
            new ProgramToClean(UninstallBaseKey.CURRENT_USER_UNINSTALL, "{78afe7b0-2508-5a6c-986d-6cf35b582123}", "dotTrace"),
            new ProgramToClean(UninstallBaseKey.LOCAL_MACHINE_WOW6432NODE_UNINSTALL, "KDiff3", "KDiff3", (location, uninstallStringDirectory) => joinPaths(uninstallStringDirectory, "kdiff3.exe")),
            new ProgramToClean(UninstallBaseKey.LOCAL_MACHINE_WOW6432NODE_UNINSTALL, "KeePassPasswordSafe2_is1", "KeePass"),
            new ProgramToClean(UninstallBaseKey.LOCAL_MACHINE_UNINSTALL, "Launchy_*_is1", "Launchy"),
            new ProgramToClean(UninstallBaseKey.CLASSES_ROOT_INSTALLER_PRODUCTS, "CE78175DAE378854AA90B7BE71313417", "Logitech Gaming Software"),
            new ProgramToClean(UninstallBaseKey.CLASSES_ROOT_INSTALLER_PRODUCTS, "ED1DC945D077C6B4C86733192AC58FEF", ".NET Core SDK", (location, directory) => {
                using RegistryKey? installedVersions = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\dotnet\Setup\InstalledVersions\x64");
                if (installedVersions?.GetValue("InstallLocation") is string installLocation) {
                    string iconFilePath = joinPaths(installLocation, "dotnet.ico")!;

                    if (!File.Exists(iconFilePath)) {
                        File.WriteAllBytes(iconFilePath, Resources.dotnetIcon);
                    }

                    return iconFilePath;
                }

                return null;
            }),
            new ProgramToClean(UninstallBaseKey.LOCAL_MACHINE_UNINSTALL, "ProPlus2019Retail - en-us", "Office"),
            new ProgramToClean(UninstallBaseKey.CLASSES_ROOT_INSTALLER_PRODUCTS, "E55ABB22B302F1E4E82E2516707E2AEC", hide: true),
            new ProgramToClean(UninstallBaseKey.CLASSES_ROOT_INSTALLER_PRODUCTS, "18378055CABA8CA46BF7EDA5E0234D7D", hide: true),
            new ProgramToClean(UninstallBaseKey.CLASSES_ROOT_INSTALLER_PRODUCTS, "C558A51006735C645AEE5A0FC6A310C9", hide: true),
            new ProgramToClean(UninstallBaseKey.CLASSES_ROOT_INSTALLER_PRODUCTS, "C219D7C9EDE64A7469E2A738664304AB", hide: true),
            new ProgramToClean(UninstallBaseKey.CLASSES_ROOT_INSTALLER_PRODUCTS, "F37207D363F3FBE43901D6914195B624", hide: true),
            new ProgramToClean(UninstallBaseKey.CLASSES_ROOT_INSTALLER_PRODUCTS, "6C7E9C94F9A4F6E4EA39E910D4A1AC39", hide: true),
            new ProgramToClean(UninstallBaseKey.CLASSES_ROOT_INSTALLER_PRODUCTS, "B4E370007AE0BD84C914DF7A9EBB8493", hide: true),
            new ProgramToClean(UninstallBaseKey.CLASSES_ROOT_INSTALLER_PRODUCTS, "D2F20908FE1EAC343B66479416790E40", hide: true),
            new ProgramToClean(UninstallBaseKey.CLASSES_ROOT_INSTALLER_PRODUCTS, "E554C16404AD3B9478B14103C87CECFF", hide: true),
            new ProgramToClean(UninstallBaseKey.LOCAL_MACHINE_WOW6432NODE_UNINSTALL, "{ca67548a-5ebe-413a-b50c-4b9ceb6d66c6}", hide: true),
            new ProgramToClean(UninstallBaseKey.LOCAL_MACHINE_WOW6432NODE_UNINSTALL, "{33d1fd90-4274-48a1-9bc1-97e33d9c2d6f}", hide: true),
            new ProgramToClean(UninstallBaseKey.LOCAL_MACHINE_WOW6432NODE_UNINSTALL, "{ef6b00ec-13e1-4c25-9064-b2f383cb8412}", hide: true),
            new ProgramToClean(UninstallBaseKey.LOCAL_MACHINE_WOW6432NODE_UNINSTALL, "{61087a79-ac85-455c-934d-1fa22cc64f36}", hide: true),
            new ProgramToClean(UninstallBaseKey.LOCAL_MACHINE_WOW6432NODE_UNINSTALL, "{e31cb1a4-76b5-46a5-a084-3fa419e82201}", hide: true),
            new ProgramToClean(UninstallBaseKey.LOCAL_MACHINE_WOW6432NODE_UNINSTALL, "{282975d8-55fe-4991-bbbb-06a72581ce58}", hide: true),
            new ProgramToClean(UninstallBaseKey.LOCAL_MACHINE_UNINSTALL, "{6F320B93-EE3C-4826-85E0-ADF79F8D4C61}", "Visual Studio Installer"),
            new ProgramToClean(UninstallBaseKey.LOCAL_MACHINE_WOW6432NODE_UNINSTALL, "Afterburner", "Afterburner"),
            new ProgramToClean(UninstallBaseKey.LOCAL_MACHINE_UNINSTALL, "Notepad2", "Notepad2"),
            new ProgramToClean(UninstallBaseKey.LOCAL_MACHINE_UNINSTALL, "{B2FE1952-0186-46C3-BAEC-A80AA35AC5B8}_Display.Driver", "Nvidia Graphics Driver",
                (location, directory) => Environment.ExpandEnvironmentVariables(@"%ProgramFiles%\NVIDIA Corporation\Display.NvContainer\NVDisplay.Container.exe")),
            new ProgramToClean(UninstallBaseKey.LOCAL_MACHINE_UNINSTALL, "{B2FE1952-0186-46C3-BAEC-A80AA35AC5B8}_HDAudio.Driver", "Nvidia Audio Driver",
                (location, directory) => Environment.ExpandEnvironmentVariables(@"%ProgramFiles%\NVIDIA Corporation\Display.NvContainer\NVDisplay.Container.exe")),
            new ProgramToClean(UninstallBaseKey.LOCAL_MACHINE_UNINSTALL, "{B2FE1952-0186-46C3-BAEC-A80AA35AC5B8}_Display.PhysX", "PhysX",
                (location, directory) => Environment.ExpandEnvironmentVariables(@"%ProgramFiles%\NVIDIA Corporation\Display.NvContainer\NVDisplay.Container.exe")),
            new ProgramToClean(UninstallBaseKey.LOCAL_MACHINE_UNINSTALL, "{B2FE1952-0186-46C3-BAEC-A80AA35AC5B8}_USBC", "Nvidia USB-C Driver",
                (location, directory) => Environment.ExpandEnvironmentVariables(@"%ProgramFiles%\NVIDIA Corporation\Display.NvContainer\NVDisplay.Container.exe")),
            new ProgramToClean(UninstallBaseKey.CURRENT_USER_UNINSTALL, "Fiddler2", "Fiddler"),
            new ProgramToClean(UninstallBaseKey.CLASSES_ROOT_INSTALLER_PRODUCTS, "8F325563E5BC7EB498E801DE8976F5DE", "JustMock"),
            new ProgramToClean(UninstallBaseKey.LOCAL_MACHINE_WOW6432NODE_UNINSTALL, "PS Tray Factory_is1", "PS Tray Factory"),
            new ProgramToClean(UninstallBaseKey.CLASSES_ROOT_INSTALLER_PRODUCTS, "B89D7B03492828D48835ADF81F1C98B0", "Retrospect Client"),
            new ProgramToClean(UninstallBaseKey.LOCAL_MACHINE_UNINSTALL, "Sublime Text 3_is1", "Sublime Text"),
            new ProgramToClean(UninstallBaseKey.LOCAL_MACHINE_UNINSTALL, "TagScanner_is1", "TagScanner"),
            new ProgramToClean(UninstallBaseKey.LOCAL_MACHINE_WOW6432NODE_UNINSTALL, "Totalcmd", "Total Commander", (location,           directory) => joinPaths(location, "TOTALCMD.EXE")),
            new ProgramToClean(UninstallBaseKey.LOCAL_MACHINE_WOW6432NODE_UNINSTALL, "TreeSize Professional_is1", "TreeSize", (location, directory) => joinPaths(location, "TreeSize.exe")),
            new ProgramToClean(UninstallBaseKey.LOCAL_MACHINE_WOW6432NODE_UNINSTALL, "8dedcee8", "Visual Studio"),
            new ProgramToClean(UninstallBaseKey.LOCAL_MACHINE_UNINSTALL, "VLC media player", "VLC"),
            new ProgramToClean(UninstallBaseKey.LOCAL_MACHINE_UNINSTALL, "WinRAR archiver", "WinRAR"),
            new ProgramToClean(UninstallBaseKey.LOCAL_MACHINE_WOW6432NODE_UNINSTALL, "winscp3_is1", "WinSCP"),
            new ProgramToClean(UninstallBaseKey.LOCAL_MACHINE_WOW6432NODE_UNINSTALL, "WRC 8_is1", setDisplayIconUsing: (location, directory) => joinPaths(location, "WRC8.exe")),
            new ProgramToClean(UninstallBaseKey.LOCAL_MACHINE_UNINSTALL, "RolandRDID0117", "Quad-Capture")
        };

        private static string? joinPaths(params string?[] paths) {
            return paths.All(path => path != null) ? Path.Combine(paths!) : null;
        }

    }

}