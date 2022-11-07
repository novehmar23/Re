namespace Domain
{
    public class Bug
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Version { get; set; }
        public bool IsActive { get; set; }
        public int Id { get; set; }
        public Project Project { get; set; }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public int? CompletedById { get; set; }
        public Developer CompletedBy { get; set; }
        public int Time { get; set; }
        public BugType? Priority { get; set; }
        public BugType? Severity { get; set; }

        public Bug()
        {
            IsActive = true;
            CompletedBy = null;
        }

        public override bool Equals(object obj)
        {
            Bug bug = (Bug)obj;
            return bug.Id == this.Id;
        }

        public void Validate()
        {
            bool hasNoProject = this.ProjectId == 0 &&
                                this.Project == null &&
                                this.ProjectName == null;
            if (this.Name == null ||
                this.Description == null ||
                this.Version == null ||
                hasNoProject)
            {
                throw new ValidationException();
            }
        }
    }
}