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
    
    public partial class Klient
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Klient()
        {
            this.Kolichistvo = new HashSet<Kolichistvo>();
        }
    
        public int KodKlienta { get; set; }
        public int Aboniment { get; set; }
        public string ImiKlient { get; set; }
        public string FamiliaKlient { get; set; }
        public string NomerTelefona { get; set; }
        public string Email { get; set; }
        public Nullable<System.DateTime> DataRojdeniaKlient { get; set; }
        public string SeriaPasport { get; set; }
        public string NomerPasport { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Kolichistvo> Kolichistvo { get; set; }
    }
}
