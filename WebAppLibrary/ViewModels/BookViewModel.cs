using System;
using System.ComponentModel.DataAnnotations;

namespace WebAppLibrary.ViewModels
{
    public class BookViewModel                  //For validations
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 5)]
        public string Name { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 5)]
        public string Author { get; set; }

        public DateTime Created { get; set; } = DateTime.Now;

        public string Location { get; set; }

    }
}
