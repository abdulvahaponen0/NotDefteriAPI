using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class NotModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Lütfen başlık değerini yazınız.")]
        [StringLength(20,MinimumLength =3,ErrorMessage ="Başlık 3 ile 20 karakter uzunluğunda olamlıdır.")]
        public string? Baslik { get; set; }
        [Required(ErrorMessage = "Lütfen tarihi yazınız.")]
        [Display(Name = "Tarih")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? Tarih { get; set; }
        [Required(ErrorMessage = "Lütfen not değerini yazınız.")]
        [StringLength(500, MinimumLength = 3, ErrorMessage = "Not değeri uzunluğu 3 ile 500 karakter uzunluğunda olamlıdır.")]
        public string? NotDegeri { get; set; }
    }
}
