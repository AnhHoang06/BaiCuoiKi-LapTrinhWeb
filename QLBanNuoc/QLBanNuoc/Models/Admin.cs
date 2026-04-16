using System.ComponentModel.DataAnnotations;

namespace QLBanNuoc.Models
{
    public class Admin
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên đăng nhập không được để trống")]
        [StringLength(50)]
        public string Username { get; set; }

        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        [StringLength(255)]
        public string PasswordHash { get; set; }

        [StringLength(100)]
        public string? FullName { get; set; }
    }
}