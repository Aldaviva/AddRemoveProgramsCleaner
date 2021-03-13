using System;
using AddRemoveProgramsCleaner.Registry;

namespace AddRemoveProgramsCleaner.Programs {

    public class ProgramToClean {

        public ProgramSelector selector { get; }
        public ProgramModifications modifications { get; }

        public ProgramToClean(UninstallBaseKey baseKey, ProgramSelector selector, string? setDisplayNameTo = null, ProgramModifications.DisplayIconGenerator? setDisplayIconUsing = null,
                              bool?            hide = null) {
            if (selector.displayName == null && selector.keyName == null) {
                throw new ArgumentException("The selector must not have a null keyName pattern and a displayName pattern. At least one of these properties must be non-null.");
            }

            this.selector         = selector;
            this.selector.baseKey = baseKey;
            modifications         = new ProgramModifications(setDisplayNameTo, setDisplayIconUsing, hide);
        }

    }

}