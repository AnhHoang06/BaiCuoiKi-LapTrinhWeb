using System.ComponentModel.DataAnnotations;

namespace QLBanNuoc.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên loại không được để trống")]
        [StringLength(100)]
        public string Name { get; set; }

        public ICollection<Drink> Drinks { get; set; } = new List<Drink>();
    }
}