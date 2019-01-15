using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RememberUtility.Model
{
    public class Books
    {
        [DisplayName("Book id")]
        [JsonProperty(PropertyName = "BookId", NullValueHandling = NullValueHandling.Ignore)]
        public string BookId { get; set; }

        [DisplayName("Book name")]
        [Required(ErrorMessage = "Book name is required")]
        [JsonProperty(PropertyName = "BookName", NullValueHandling = NullValueHandling.Ignore)]
        public string BookName { get; set; }

        [DisplayName("Author")]
        [Required(ErrorMessage = "Author is required")]
        [JsonProperty(PropertyName = "Author", NullValueHandling = NullValueHandling.Ignore)]
        public string Author { get; set; }

        [DisplayName("Category")]
        [Required(ErrorMessage = "Please choose a category")]
        [JsonProperty(PropertyName = "Category", NullValueHandling = NullValueHandling.Ignore)]
        public string Category { get; set; }

        [DisplayName("Created date")]
        [JsonProperty(PropertyName = "CreatedDate", NullValueHandling = NullValueHandling.Ignore)]
        public string CreatedDate { get; set; }

        [DisplayName("Last mod date")]
        [JsonProperty(PropertyName = "LastModifiedDate", NullValueHandling = NullValueHandling.Ignore)]
        public string LastModifiedDate { get; set; }

        public override string ToString()
        {
            return "Books";
        }
    }
}
