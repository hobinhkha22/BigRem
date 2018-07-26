using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectionSampleCode.Model
{
    public class Entertainment
    {
        public string EnterId { get; set; }
        public string EnterName { get; set; }
        public string Links { get; set; }
        public string Category { get; set; }
        public string CreatedDate { get; set; }
        public string LastModifiedDate { get; set; }

        public override string ToString()
        {
            return "Entertainment";
        }
    }
}
