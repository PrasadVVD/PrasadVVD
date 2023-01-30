using ArtosApp.Models;
using System.Collections.Generic;

namespace ArtosApp.Helpers
{
    public static class StaticVariableHelper
    {
        public static LoginResponse LoggedUser { get; set; }
        public static List<DepoVM> Depos { get; set; }
        public static List<ItemVM> Items { get; set; }
    }
}
