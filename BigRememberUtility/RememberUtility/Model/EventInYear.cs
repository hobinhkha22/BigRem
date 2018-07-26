using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectionSampleCode.Model
{
    public class EventInYear
    {
        // EventInYear by myslef
        public string EventYearId { get; set; }
        public string EventName { get; set; }
        public string CountryOccured { get; set; }
        public string EventLink { get; set; }
        public string EventDate { get; set; }
        public string ShortDescribe { get; set; }
        public string CreatedDate { get; set; }
        public string LastModifiedDate { get; set; }

        public override string ToString()
        {
            return "EventInYear";
        }
    }
}
