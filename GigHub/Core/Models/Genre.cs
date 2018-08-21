using System.ComponentModel.DataAnnotations;

namespace GigHub.Core.Models
{
    public class Genre
    {
        public byte Id { get; set; }

        //Move to GenreConfiguration
        //[Required]
        //[StringLength(255)]
        public string Name { get; set; }
    }
}