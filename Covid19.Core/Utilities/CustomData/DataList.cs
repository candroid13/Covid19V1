using System.Collections.Generic;

namespace Covid19.Core.Utilities.CustomData
{
    public static class DataList
    {
        public static List<string> Countries()
        {
            var countries = new List<string>
            {
                "Turkey",
                "America",
                "China",
                "Spain"
            };
            return countries;
        }
    }
}
