using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TokenManager.Models
{
    public class Token
    {
        public int Id { get; set; }
        [Display(Name="私钥")]
        [Required(ErrorMessage = "必填字段")]
        public string SecretToken { get; set; }
        [Display(Name = "公钥")]
        [Required(ErrorMessage = "必填字段")]
        public string OpenToken { get; set; }
        [Display(Name="第三方名称")]
        [Required(ErrorMessage="必填字段")]
        public string ThirdPartName { get; set; }
        [Display(Name="联系方式")]
        [RegularExpression(@"^[1][3578]\d{9}$",ErrorMessage="无效手机号")]
        public string Telphone { get; set; }
    }
}