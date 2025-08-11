using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Homework_1.Models
{
    public class MainContent
    {
        [Display(Name = "編號")]
        [StringLength(36, MinimumLength = 36)]
        [Key]
        [HiddenInput]
        public string MainID { get; set; } = null!; //主文編號, 採用GUID

        [Display(Name = "主題")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "主題2~30個字")]
        [Required(ErrorMessage = "必填")]
        public string MTitle { get; set; } = null!;

        [Display(Name = "發表內容")]
        [Required(ErrorMessage = "必填")]
        [DataType(DataType.MultilineText)]
        public string MContent { get; set; } = null!;

        [Display(Name = "照片")]
        [StringLength(40)]

        public string? MPhoto { get; set; } = null!; //照片的檔名規則:GUID+.jpg

        [Display(Name = "照片類型")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "照片簡述2~30個字")]
        public string? MPhotoType { get; set; } = null!; 

        [Display(Name = "發表人")]
        [StringLength(20, ErrorMessage = "最多20字")]
        [Required(ErrorMessage = "必填")]
        public string NAuthor { get; set; } = null!;

        [Display(Name = "張貼時間")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm:ss}")]
        [HiddenInput]
        public DateTime CreatedDate { get; set; } = DateTime.Now;



        public virtual List<Response>? Response { get; set; }
    }
}
