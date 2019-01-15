using System.Collections.Generic;

namespace RememberUtility.Model
{
    public class ConfigModel
    {
        public List<Books> Books { get; set; }
        public List<Entertainment> Entertainment { get; set; }
        public List<Quotes> Quotes { get; set; }
        public List<EventInYear> EventInYears { get; set; }
        public List<UserLogin> UserLogin { get; set; }
    }
}
