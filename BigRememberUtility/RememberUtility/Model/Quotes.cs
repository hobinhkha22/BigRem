using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectionSampleCode.Model
{
    public class Quotes
    {
        public string QuotesId { get; set; }
        public string QuotesName { get; set; }
        public string Author { get; set; }
        public string Type { get; set; }
        public string CreatedDate { get; set; }
        public string LastModifiedDate { get; set; }

        public override string ToString()
        {
            return "Quotes";
        }
    }
}
