using System.ComponentModel;

namespace ConnectionSampleCode.Model
{
    public class Books
    {
        [DisplayName("Book id")]
        public string BookId { get; set; }

        [DisplayName("Book name")]
        public string BookName { get; set; }

        [DisplayName("Author")]
        public string Author { get; set; }

        [DisplayName("Category")]
        public string Category { get; set; }

        [DisplayName("Created date")]
        public string CreatedDate { get; set; }

        [DisplayName("Last mod date")]
        public string LastModifiedDate { get; set; }

        public override string ToString()
        {
            return "Books";
        }
    }
}
