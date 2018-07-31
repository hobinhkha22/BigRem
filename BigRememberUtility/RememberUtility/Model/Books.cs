using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ConnectionSampleCode.Model
{
    public class Books
    {
        [DisplayName("Book id")]
        public string BookId { get; set; }

        [DisplayName("Book name")]
        [Required(ErrorMessage = "Book name is required")]
        public string BookName { get; set; }

        [DisplayName("Author")]
        [Required(ErrorMessage = "Author is required")]
        public string Author { get; set; }

        [DisplayName("Category")]
        [Required(ErrorMessage = "Please choose a category")]
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
