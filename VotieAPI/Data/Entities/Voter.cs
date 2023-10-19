namespace VotieAPI.Data.Entities
{
    public class Voter
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }    
        public DateTime? Date { get; set; } 

    }
}
