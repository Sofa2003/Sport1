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
    
    public partial class Kolichistvo
    {
        public int KodKolich { get; set; }
        public int KodZanitia { get; set; }
        public int KodClienta { get; set; }
    
        public virtual Klient Klient { get; set; }
        public virtual Zanitie Zanitie { get; set; }
    }
}