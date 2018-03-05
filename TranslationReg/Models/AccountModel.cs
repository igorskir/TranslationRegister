using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TranslationReg.Models
{
    public class LoginModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

    public class RegisterModel
    {
        [Required]
        [Display(Name = "\"Логин\"")]
        [StringLength(100, ErrorMessage = "Логин должен включать менее 100 символов")]
        [RegularExpression(@"^[a-zA-Zа-яА-Я0-9 _]+$", ErrorMessage = "Допустимы буквы, цифры, пробел и символ подчеркивания")]
        public string Name { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Пароль должен включать менее 100 символов")]
        [DataType(DataType.Password)]
        [Display (Name = "\"Пароль\"")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "\"Подтверждение пароля\"")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }
    }
}