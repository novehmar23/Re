using System.Collections.Generic;

namespace Domain
{
    public class Tester : User
    {
        public List<Project> Projects { get; set; }
        public int Cost { get; set; }

        public Tester()
        {
            Projects = new List<Project>();
        }

    }
}