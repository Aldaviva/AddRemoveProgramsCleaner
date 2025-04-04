namespace AddRemoveProgramsCleaner.Programs;

public class ProgramModifications(string? displayName, ProgramModifications.DisplayIconGenerator? displayIconGenerator, bool? hide) {

    /// <summary>
    /// Delegate function to determine the icon for an Uninstall entry based on other fields in the registry key.
    /// </summary>
    /// <param name="installLocation">The data in the <c>InstallLocation</c> registry value of this program.</param>
    /// <param name="uninstallStringDirectory">The path portion of the executable filename in the <c>UninstallString</c> registry value.</param>
    /// <returns>The absolute path to an icon file, or to an icon group resource in an EXE or DLL (with optional trailing comma and icon index). To leave the icon unchanged, return <c>null</c>.</returns>
    public delegate string? DisplayIconGenerator(string? installLocation, string? uninstallStringDirectory);

    public string? displayName { get; } = displayName;
    public DisplayIconGenerator? displayIconGenerator { get; } = displayIconGenerator;
    public bool? hide { get; } = hide;

}