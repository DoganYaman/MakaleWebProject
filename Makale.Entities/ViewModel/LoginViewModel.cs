using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Makale.Entities
{
    public class LoginViewModel
    {
        [DisplayName("Kullanıcı Adı"),Required(ErrorMessage ="{0} alanı boş geçilemez.")]
        public string KullaniciAdi { get; set; }

        [DisplayName("Şifre"),Required(ErrorMessage = "{0} alanı boş geçilemez."),DataType(DataType.Password)]
        public string Sifre { get; set; }
    }
}