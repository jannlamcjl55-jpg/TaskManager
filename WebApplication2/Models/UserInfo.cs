using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models
{
    public class UserInfo
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="請輸入賬號")]
        [StringLength(15,ErrorMessage ="賬號名稱最多15個字元")]
        public string UserName {  get; set; }

        [Required(ErrorMessage ="請輸入密碼")]
        [DataType(DataType.Password)]
        [StringLength(20,MinimumLength =6,ErrorMessage ="密碼不能少於6個子元")]
        public string Password { get; set; }
    }
}
