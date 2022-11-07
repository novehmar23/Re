namespace Domain
{
    public class BugType
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Comments { get; set; }
        public string Value { get; set; }
        public int Id { get; set; }


        public override bool Equals(object obj)
        {
            BugType bugType = (BugType)obj;
            return bugType.Id == this.Id;
        }

        public void Validate()
        {
            if (Name == null || Description == null || Comments == null || Value == null ||
                Name == "" || Description == "" || Comments == "" || Value == "")
                throw new ValidationException();
        }
    }
}