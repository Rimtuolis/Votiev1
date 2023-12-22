using System.ComponentModel.DataAnnotations;

namespace Votie.Shared

{
    public class District
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Region { get;set; }
    }
    public class DistrictRequest
    {
        public string Name { get; set; }
        public string Region { get; set; }
        public void Clear() => Name = Region = null;
    }
}
