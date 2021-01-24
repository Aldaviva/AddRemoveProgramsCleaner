using System;
using System.Collections.Generic;
using System.Linq;
using DotNet.Globbing;
using DotNet.Globbing.Token;
using Microsoft.Win32;

namespace AddRemoveProgramsCleaner.Programs {

    public class ProgramSelector {

        public UninstallBaseKey baseKey { get; set; }
        public Glob? keyName { get; }
        public Glob? displayName { get; }

        public ProgramSelector(string? keyName = null, string? displayName = null) {
            if ((displayName == null) && (keyName == null)) {
                throw new ArgumentException("The selector must not have a null keyName pattern and a null displayName pattern. At least one of these properties must be non-null.");
            }

            this.keyName     = keyName != null ? Glob.Parse(keyName) : null;
            this.displayName = displayName != null ? Glob.Parse(displayName) : null;
        }

        public IEnumerable<RegistryKey> openKey() {
            using RegistryKey        baseKeyHandle = baseKey.openKey();
            IEnumerable<RegistryKey> subKeys;

            if (isPatternLiteral(keyName)) {
                RegistryKey? subKey = baseKeyHandle.OpenSubKey(keyName!.ToString(), true);
                subKeys = subKey != null ? new[] { subKey } : Enumerable.Empty<RegistryKey>();
            } else {
                // ReSharper disable once AccessToDisposedClosure - closure is evaluated by .ToList() before method returns and baseKeyHandle is disposed
                subKeys = baseKeyHandle.GetSubKeyNames()
                    .Where(subkeyName => keyName?.IsMatch(subkeyName) ?? true)
                    .Select(subkeyName => baseKeyHandle.OpenSubKey(subkeyName, true))
                    .Compact()
                    .Where(subKey => (displayName == null) || (RegistryHelpers.getDisplayName(subKey) is { } displayNameValue && displayName.IsMatch(displayNameValue)))
                    .Compact()
                    .ToList();
            }

            return subKeys;
        }

        /// <returns><c>true</c> if none of the path components of <c>pattern</c> have wildcards (* or ?) in them, or <c>false</c> if one or more components do contain a wildcard.</returns>
        private static bool isPatternLiteral(Glob? pattern) {
            return pattern?.Tokens.All(token => token is LiteralToken) ?? false;
        }

    }

}