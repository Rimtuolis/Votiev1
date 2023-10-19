namespace VotieAPI.Data.Entities
{
    public class Election
    {
        public int Id { get; set; } 
        public  string Name { get; set; }
        public  DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public  District District { get; set; }
    }
}
