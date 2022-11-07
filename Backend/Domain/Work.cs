namespace Domain
{
    public class Work
    {
        public string Name { get; set; }
        public int Cost { get; set; }
        public int Time { get; set; }
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public Project Project { get; set; }

        public void Validate()
        {
            bool hasNoProject = this.ProjectId == 0 &&
                                this.Project == null &&
                                this.ProjectName == null;
            if (this.Name == null ||
                this.Time == 0 ||
                this.Cost == 0 ||
                hasNoProject)
            {
                throw new ValidationException();
            }
        }
    }
}