using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RememberUtility.Model
{
    public class Entertainment
    {
        [DisplayName("Enter id")]
        public string EnterId { get; set; }
        private string enterName;
        [DisplayName("Enter name")]
        public string EnterName
        {
            get { return enterName; }
            set {
                if (enterName != value)
                {
                    enterName = value;
                }
            }
        }

        private string links;
        [DisplayName("Link")]
        public string Links
        {
            get { return links; }
            set {
                if (links != value)
                {
                    links = value;
                }
            }
        }
        [DisplayName("Category")]
        public string Category { get; set; }
        [DisplayName("AuthorEnter")]
        public string AuthorEnter { get; set; }
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
