namespace Process_Spy {
    public class Processes {

        // Properties
        public string Name { get; set; } = "";
        public int ID { get; set; } = -1;
        public int Parent { get; set; } = 0;

        // Constructor
        public Processes() { }
        public Processes(string Name, int ID, int Parent) {
            this.Name = Name;
            this.ID = ID;
            this.Parent = Parent;
        }
    }
}
