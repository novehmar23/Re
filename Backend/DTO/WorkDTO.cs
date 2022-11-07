using Domain;

namespace DTO
{
    public class WorkDTO
    {
        public string Name { get; set; }
        public int Cost { get; set; }
        public int Time { get; set; }
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }

        public WorkDTO()
        {
        }

        public WorkDTO(Work work)
        {
            Name = work.Name;
            Cost = work.Cost;
            Time = work.Time;
            Id = work.Id;
            ProjectId = work.ProjectId;
            ProjectName = work.ProjectName;
        }

        public Work ConvertToDomain()
        {
            Work work = new Work()
            {
                Name = this.Name,
                Cost = this.Cost,
                Time = this.Time,
                Id = this.Id,
                ProjectId = this.ProjectId,
                ProjectName = this.ProjectName
            };
            return work;
        }

        public override bool Equals(object obj)
        {
            WorkDTO workDTO = (WorkDTO)obj;
            return workDTO.Id == this.Id;
        }
    }
}