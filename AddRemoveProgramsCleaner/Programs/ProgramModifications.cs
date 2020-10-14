namespace AddRemoveProgramsCleaner.Programs {

    public class ProgramModifications {

        public delegate string? DisplayIconGenerator(string? installLocation, string? uninstallStringDirectory);

        public string? displayName { get; }
        public DisplayIconGenerator? displayIconGenerator { get; }
        public bool? hide { get; }

        public ProgramModifications(string? displayName, DisplayIconGenerator? displayIconGenerator, bool? hide) {
            this.displayName          = displayName;
            this.displayIconGenerator = displayIconGenerator;
            this.hide                 = hide;
        }

    }

}