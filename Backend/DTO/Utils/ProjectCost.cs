namespace DTO
{
    public class ProjectCost
    {

        public ProjectCost(int expectedCost)
        {
            Cost = expectedCost;
        }

        public int Cost { get; set; }
    }
}