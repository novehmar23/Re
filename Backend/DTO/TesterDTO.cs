using Domain;
using System.Collections.Generic;

namespace DTO
{
    public class TesterDTO
    {
        private Tester tester;

        public TesterDTO()
        {
            ProjectsDTO = new List<ProjectDTO>();
        }

        public TesterDTO(Tester tester)
        {
            Id = tester.Id;
            Username = tester.Username;
            Name = tester.Name;
            Lastname = tester.Lastname;
            Password = tester.Password;
            Email = tester.Email;
            Cost = tester.Cost;
            ProjectsDTO = tester.Projects.ConvertAll(p => new ProjectDTO(p));
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int Cost { get; set; }
        public List<ProjectDTO> ProjectsDTO { get; set; }

        public Tester ConvertToDomain()
        {
            Tester tester = new Tester()
            {
                Id = this.Id,
                Username = this.Username,
                Name = this.Name,
                Cost = this.Cost,
                Email = this.Email,
                Lastname = this.Lastname,
                Password = this.Password,
                Projects = this.ProjectsDTO.ConvertAll(p => p.ConvertToDomain())
            };
            return tester;
        }

        public override bool Equals(object obj)
        {
            TesterDTO testerDTO = (TesterDTO)obj;
            return testerDTO.Id == this.Id;
        }
    }
}
