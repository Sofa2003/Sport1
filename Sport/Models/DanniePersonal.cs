//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Sport.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class DanniePersonal
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DanniePersonal()
        {
            this.InfoZanitia = new HashSet<InfoZanitia>();
        }
    
        public int KodPersonal { get; set; }
        public string ImiPersonal { get; set; }
        public string FamiliaPersonala { get; set; }
        public string OthestvoPersonala { get; set; }
        public Nullable<System.DateTime> DataRojdenia { get; set; }
        public string NomerTelefona { get; set; }
        public int DoljnostiPersonal { get; set; }
        public Nullable<int> StajRaboti { get; set; }
        public string E_mail { get; set; }
    
        public virtual Doljnosti Doljnosti { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InfoZanitia> InfoZanitia { get; set; }
        public virtual Polizovateli Polizovateli { get; set; }
    }
}
