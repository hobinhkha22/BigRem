using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectionSampleCode.Model
{
    public class Entertainment
    {
        [DisplayName("Enter id")]
        public string EnterId { get; set; }
        [DisplayName("Enter name")]
        public string EnterName { get; set; }
        [DisplayName("Link")]
        public string Links { get; set; }
        [DisplayName("Category")]
        public string Category { get; set; }
        [DisplayName("Created date")]
        public string CreatedDate { get; set; }
        [DisplayName("Last Mod date")]
        public string LastModifiedDate { get; set; }

        public override string ToString()
        {
            return "Entertainment";
        }
    }
}
