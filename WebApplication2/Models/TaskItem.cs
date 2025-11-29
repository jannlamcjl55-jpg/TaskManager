using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models
{
    public class TaskItem
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="任務名稱不可為空")]
        public string Title {  get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public bool IsComplete { get; set; } 


        [DataType(DataType.Date)]
        public DateTime DueDate { get; set; }= DateTime.Now;

        public int MemberId {  get; set; }
        public Member Member { get; set; }


    }
}
