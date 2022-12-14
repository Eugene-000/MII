//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MilitaryAirfield.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Вооружение
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Вооружение()
        {
            this.Воздушное_судно = new HashSet<Воздушное_судно>();
        }

        [Display(Name = "ID вооружения судна")]
        public int ID_вооружения_судна { get; set; }
        [Display(Name = "Используемые ракеты")]
        [Required(ErrorMessage = "Введите используемые ракеты")]
        public string Используемые_ракеты { get; set; }
        [Display(Name = "Боевая нагрузка (кг)")]
        public int Боевая_нагрузка_в_кг_ { get; set; }
        [Display(Name = "Количество точек подвески")]
        public int Количество_точек_подвески { get; set; }
        [Display(Name = "Наличие бомб")]
        public bool Наличие_бомб { get; set; }
        [Display(Name = "Стрелково-пушечное вооружение")]
        [Required(ErrorMessage = "Введите стрелково-пушечное вооружение")]
        public string Стрелково_пушечное_вооружение { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Воздушное_судно> Воздушное_судно { get; set; }
    }
}
