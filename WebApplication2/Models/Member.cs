using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.Models
{
    public class Member
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="姓名必填")]
        public string Name { get; set; }

        [Required(ErrorMessage ="郵箱必填")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public string PasswordHash { get; set; } //儲存到資料庫
    }
}
