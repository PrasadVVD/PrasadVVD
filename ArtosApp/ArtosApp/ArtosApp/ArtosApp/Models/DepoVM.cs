using Newtonsoft.Json;
using Xamarin.Forms;

namespace ArtosApp.Models
{
    public class DepoVM
    {
        public int DepoId { get; set; }
        public string DepoName { get; set; }
        public int UserId { get; set; }

        [JsonIgnore]
        public Color BackGround { get; set; }
    }
}
