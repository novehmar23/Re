namespace DTO
{ 
    public class ProjectDuration
    {

        public ProjectDuration(int expectedDuration)
        {
            Duration = expectedDuration;
        }

        public int Duration { get; set; }
    }
}