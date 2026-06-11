using FinalExam.Models.Base;

namespace FinalExam.Models
{
    public class City : BaseEntity
    {
        public string Name { get; set; }
        public List<Place> Places { get; set; }
    }
}
