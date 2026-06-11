using FinalExam.Models.Base;

namespace FinalExam.Models
{
    public class Place : BaseEntity
    {
        public string Name { get; set; }
        public string FullAddress { get; set; }
        public string Description { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }
        public string ImageUrl { get; set; }
    }
}
