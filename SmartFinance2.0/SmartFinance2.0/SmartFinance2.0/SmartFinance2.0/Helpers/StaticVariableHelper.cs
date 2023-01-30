using SmartFinance.Models;
using System.Collections.Generic;

namespace SmartFinance.Helpers
{
    public static class StaticVariableHelper
    {
        public static LoginResponse LoggedUser { get; set; }
        public static List<LineVM> Lines { get; set; }
        public static List<string> LineDays { get; set; }
    }
}
