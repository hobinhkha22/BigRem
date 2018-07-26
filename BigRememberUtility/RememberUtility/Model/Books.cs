using System.ComponentModel;

namespace ConnectionSampleCode.Model
{
    public class Books
    {
        [DisplayName("Book Id")]
        public string BookId { get; set; }

        [DisplayName("Book Name")]
        public string BookName { get; set; }

        [DisplayName("Author")]
        public string Author { get; set; }

        [DisplayName("Category")]
        public string Category { get; set; }

        [DisplayName("Created Date")]
        public string CreatedDate { get; set; }

        [DisplayName("L. Modified Date")]
        public string LastModifiedDate { get; set; }

        public override string ToString()
        {
            return "Books";
        }
    }
}
