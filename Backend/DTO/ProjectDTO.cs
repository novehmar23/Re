using Domain;

namespace DTO
{
    public class ProjectDTO
    {
        public ProjectDTO()
        {
        }

        public ProjectDTO(Project project)
        {
            Name = project.Name;
            Id = project.Id;
        }

        public string Name { get; set; }
        public int Id { get; set; }
        public int TotalCost { get; set; }
        public int TotalDuration { get; set; }
        public int BugsQuantity { get; set; }

        public Project ConvertToDomain()
        {
            Project project = new Project()
            {
                Name = this.Name,
                Id = this.Id
            };
            return project;
        }

        public override bool Equals(object obj)
        {
            ProjectDTO projectDTO = (ProjectDTO)obj;
            return projectDTO.Id == this.Id;
        }
    }
}