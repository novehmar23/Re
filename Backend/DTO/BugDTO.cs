using Domain;

namespace DTO
{
    public class BugDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Version { get; set; }
        public int Time { get; set; }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public int Id { get; set; }
        public int? CompletedById { get; set; }
        public bool IsActive { get; set; }
        public string CompletedByUsername { get; set; }
        public BugTypeDTO Type { get; set; }

        public BugDTO(Bug bug)
        {
            Name = bug.Name;
            Description = bug.Description;
            Version = bug.Version;
            Time = bug.Time;
            ProjectId = bug.ProjectId;
            ProjectName = bug.Project.Name;
            Id = bug.Id;
            IsActive = bug.IsActive;
            CompletedById = bug.CompletedById;
            if (bug.CompletedBy != null) { CompletedByUsername = bug.CompletedBy.Username; };
            Type = new BugTypeDTO(bug.Priority);
        }

        public BugDTO()
        {
            IsActive = true;
        }

        public Bug ConvertToDomain()
        {
            Bug bug = new Bug()
            {
                Name = this.Name,
                Description = this.Description,
                Version = this.Version,
                CompletedById = this.CompletedById,
                Id = this.Id,
                IsActive = this.IsActive,
                ProjectId = this.ProjectId,
                ProjectName = this.ProjectName,
                Time = this.Time
            };
            if (this.Type != null)
            {

                bug.Priority = this.Type.ConvertToDomain();
            }
            return bug;
        }

        public override bool Equals(object obj)
        {
            BugDTO bugDTO = (BugDTO)obj;
            return bugDTO.Id == this.Id;
        }
    }


}