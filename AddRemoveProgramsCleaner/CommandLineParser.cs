using System.Text;

namespace AddRemoveProgramsCleaner;

internal static class CommandLineParser {

    /// <summary>
    ///     See https://stackoverflow.com/a/64236441/979493
    /// </summary>
    internal static IEnumerable<string> splitArgs(string commandLine) {
        StringBuilder result = new();

        bool quoted     = false;
        bool escaped    = false;
        bool started    = false;
        bool allowcaret = false;
        for (int i = 0; i < commandLine.Length; i++) {
            char chr = commandLine[i];

            if (chr == '^' && !quoted) {
                if (allowcaret) {
                    result.Append(chr);
                    started    = true;
                    escaped    = false;
                    allowcaret = false;
                } else if (i + 1 < commandLine.Length && commandLine[i + 1] == '^') {
                    allowcaret = true;
                } else if (i + 1 == commandLine.Length) {
                    result.Append(chr);
                    started = true;
                    escaped = false;
                }
            } else if (escaped) {
                result.Append(chr);
                started = true;
                escaped = false;
            } else if (chr == '"') {
                quoted  = !quoted;
                started = true;
            } else if (chr == '\\' && i + 1 < commandLine.Length && commandLine[i + 1] == '"') {
                escaped = true;
            } else if (chr == ' ' && !quoted) {
                if (started) yield return result.ToString();
                result.Clear();
                started = false;
            } else {
                result.Append(chr);
                started = true;
            }
        }

        if (started) yield return result.ToString();
    }

}