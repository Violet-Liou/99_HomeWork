using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Homework_1.Models
{
    public class Response
    {
        [Display(Name = "編號")]
        [StringLength(36, MinimumLength = 36)]
        [Key]
        [HiddenInput]
        public string ResponseID { get; set; } = null!; //主文編號, 採用GUID

        [Display(Name = "回覆內容")]
        [Required(ErrorMessage = "必填")]
        [DataType(DataType.MultilineText)]
        public string RContent { get; set; } = null!;

        [Display(Name = "回覆人")]
        [StringLength(20, ErrorMessage = "最多20字")]
        [Required(ErrorMessage = "必填")]
        public string RAuthor { get; set; } = null!;

        [Display(Name = "回覆時間")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm:ss}")]
        [HiddenInput]
        public DateTime CreatedDate { get; set; } = DateTime.Now;



        [ForeignKey("MainContent")]
        [HiddenInput]
        public string MainID { get; set; } = null!;

        //一個Response只能屬於一個MainContent
        public virtual MainContent? MainContent { get; set; } 
    }
}
