using System.ComponentModel;

namespace RememberUtility.Model
{
    public class EventInYear
    {
        // EventInYear by myslef
        [DisplayName("Event id")]
        public string EventYearId { get; set; }
        [DisplayName("Event name")]
        public string EventName { get; set; }
        [DisplayName("Country occured")]
        public string CountryOccured { get; set; }
        [DisplayName("Event link")]
        public string EventLink { get; set; }
        [DisplayName("Event date")]
        public string EventDate { get; set; }
        [DisplayName("Short Describe")]
        public string ShortDescribe { get; set; }
        [DisplayName("Created date")]
        public string CreatedDate { get; set; }
        [DisplayName("last mod date")]
        public string LastModifiedDate { get; set; }

        public override string ToString()
        {
            return "EventInYear";
        }
    }
}
