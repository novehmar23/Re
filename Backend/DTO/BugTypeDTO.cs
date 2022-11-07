using Domain;

namespace DTO
{
    public class BugTypeDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Comments { get; set; }
        public string Value { get; set; }
        public int Id { get; set; }

        public BugTypeDTO(BugType bugType)
        {
            if (bugType != null)
            {
                Name = bugType.Name;
                Description = bugType.Description;
                Comments = bugType.Comments;
                Value = bugType.Value;
            }
        }

        public BugTypeDTO() { }

        public BugType ConvertToDomain()
        {
            BugType bugType = new BugType()
            {
                Name = this.Name,
                Description = this.Description,
                Comments = this.Comments,
                Value = this.Value,
                Id = this.Id,
            };
            return bugType;
        }

        public override bool Equals(object obj)
        {
            BugTypeDTO bugTypeDTO = (BugTypeDTO)obj;
            return bugTypeDTO.Id == this.Id;
        }
    }


}