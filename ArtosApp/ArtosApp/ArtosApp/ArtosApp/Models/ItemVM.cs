using Newtonsoft.Json;
using Xamarin.Forms;

namespace ArtosApp.Models
{
    public class ItemVM
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public int BottlesPerCase { get; set; }
        public decimal CasePrice { get; set; }
        [JsonIgnore]
        public Color BackGround { get; set; }
    }
}
