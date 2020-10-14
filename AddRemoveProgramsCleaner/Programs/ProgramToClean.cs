using DotNet.Globbing;

namespace AddRemoveProgramsCleaner.Programs {

    public class ProgramToClean {

        public ProgramSelector selector { get; }
        public ProgramModifications modifications { get; }

        public ProgramToClean(UninstallBaseKey baseKey, string subKeyPattern, string? setDisplayNameTo = null, ProgramModifications.DisplayIconGenerator? setDisplayIconUsing = null, bool? hide = null) {
            selector      = new ProgramSelector(baseKey, Glob.Parse(subKeyPattern));
            modifications = new ProgramModifications(setDisplayNameTo, setDisplayIconUsing, hide);
        }

    }

}