using System.ComponentModel.DataAnnotations;

namespace APIHomeWork.Models
{
    public class Inn
    {
        public int Seq { get; set; }
        [Key]
        public int 民宿登記證編號 { get; set; }
        public string 中文名稱 { get; set; }
        public string 地址 { get; set; }
        public string 電話或手機 { get; set; }
        public int 合計總房間數 { get; set; }
    }
}
