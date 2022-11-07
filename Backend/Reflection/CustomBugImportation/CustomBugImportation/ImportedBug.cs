namespace CustomBugImportation
{
    public class ImportedBug
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Version { get; set; }
        public bool IsActive { get; set; }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public int? CompletedById { get; set; }
        public int Time { get; set; }

        // Only use for testing
        public override bool Equals(object obj)
        {
            if (!(obj is ImportedBug))
                return false;
            ImportedBug b = (ImportedBug)obj;
            bool equal = this.Name == b.Name
                         && this.Description == b.Description
                         && this.Version == b.Version
                         && this.IsActive == b.IsActive
                         && this.ProjectId == b.ProjectId
                         && this.ProjectName == b.ProjectName
                         && this.CompletedById == b.CompletedById
                         && this.Time == b.Time;
            return equal;
        }
    }
}
