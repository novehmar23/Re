namespace Domain
{
    public class User
    {
        public User()
        {
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }


        public void Validate()
        {
            if (this.Username == null ||
                this.Lastname == null ||
                this.Password == null ||
                this.Name == null ||
                this.Email == null)
            {
                throw new ValidationException();
            }
        }
    }
}