using System.Collections.Generic;

namespace Domain
{
    public class Project
    {
        public string Name { get; set; }
        public List<Tester> Testers { get; set; }
        public List<Developer> Developers { get; set; }
        public List<Bug> Bugs { get; set; }
        public int Id { get; set; }
        public List<Work> Works { get; set; }

        public Project()
        {
            Testers = new List<Tester>();
            Developers = new List<Developer>();
            Bugs = new List<Bug>();
            Works = new List<Work>();
        }

        public override bool Equals(object obj)
        {
            Project project = (Project)obj;
            return (project.Id == this.Id);
        }

        public void Validate()
        {
            if (this.Name == null ||
                this.Testers == null ||
                this.Developers == null ||
                this.Bugs == null ||
                this.Works == null)
            {
                throw new ValidationException();
            }
        }
    }
}