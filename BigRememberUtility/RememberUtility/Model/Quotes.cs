using System.ComponentModel;

namespace ConnectionSampleCode.Model
{
    public class Quotes
    {
        [DisplayName("Quote id")]
        public string QuotesId { get; set; }
        [DisplayName("Quote name")]
        public string QuotesName { get; set; }
        [DisplayName("Author")]
        public string Author { get; set; }
        [DisplayName("Type")]
        public string Type { get; set; }
        [DisplayName("Created date")]
        public string CreatedDate { get; set; }
        [DisplayName("Last mod date")]
        public string LastModifiedDate { get; set; }

        public override string ToString()
        {
            return "Quotes";
        }
    }
}
