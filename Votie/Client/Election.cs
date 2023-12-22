using System.ComponentModel.DataAnnotations;

namespace Votie.Shared
{
    public class ElectionResponse
    {
        public int Id { get; set; } 
        public  string Name { get; set; }
        public  DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string District { get; set; }
    }
    public class ElectionRequest
    {
        public string Name { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int District { get; set; }

        public void Clear() => Name = null; 
    }
}
