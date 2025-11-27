using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "姓名必填")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "名字不能少於2個字")]
        public string Name { get; set; }

        [Remote(action:"CheckRepeatEmail",controller:"Home")]
        [Required(ErrorMessage = "Email 必填")]
        [EmailAddress(ErrorMessage = "Email 格式錯誤")]
        public string Email { get; set; }

        [Required(ErrorMessage = "密碼必填")]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "密碼不能少於6個數字")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
