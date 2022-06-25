using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Makale.Entities
{
    public class RegisterViewModel
    {
        [DisplayName("Kullanıcı Adı"), Required(ErrorMessage = "{0} alanı boş geçilemez."),StringLength(25,ErrorMessage ="{0} max.{1} karakter olmalı.")]
        public string KullaniciAdi { get; set; }

        [ DisplayName("E-posta"), 
          Required(ErrorMessage = "{0} alanı boş geçilemez."), 
          StringLength(50, ErrorMessage = "{0} max.{1} karakter olmalı."),
          EmailAddress(ErrorMessage ="{0} alanı için geçerli bir e-posta adresi giriniz.")]
        public string Email { get; set; }

        [DisplayName("Şifre"), 
         Required(ErrorMessage = "{0} alanı boş geçilemez."), 
         DataType(DataType.Password),
         StringLength(25, ErrorMessage = "{0} max.{1} karakter olmalı.")]
        public string Sifre { get; set; }

        [DisplayName("Şifre Tekrar"), 
         Required(ErrorMessage = "{0} alanı boş geçilemez."), 
         DataType(DataType.Password),
         StringLength(25, ErrorMessage = "{0} max.{1} karakter olmalı."),
         Compare("Sifre",ErrorMessage ="{0} ile {1} uyuşmuyor. ") ]
        public string Sifre2 { get; set; }
    }
}