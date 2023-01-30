using Newtonsoft.Json;
using Xamarin.Forms;

namespace SmartFinance.Models
{
    public class LineVM
    {
        public int Id { get; set; }
        public string LineName { get; set; }
        public int UserId { get; set; }
        public decimal InterestPer980 { get; set; }
        public int CollectionMode { get; set; }
        [JsonIgnore]
        public Color BackGround { get; set; }
    }
}
