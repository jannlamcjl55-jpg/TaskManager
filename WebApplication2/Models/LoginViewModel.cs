using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models
{
    public class LoginViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress,ErrorMessage ="請輸入郵箱格式")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)] 
        public string Password { get; set; }
    }
}
