//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CLicense.Models
{
    using System;
    
    public partial class spGetGrantee_Result
    {
        public int RowID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string NameInt { get; set; }
        public string TaxCode { get; set; }
        public string DocSeries { get; set; }
        public string DocNum { get; set; }
        public Nullable<int> SexID { get; set; }
        public Nullable<System.DateTime> Birthday { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<System.DateTime> CloseDate { get; set; }
        public string Note { get; set; }
        public string SexCode { get; set; }
        public string SexName { get; set; }
        public string StatusCode { get; set; }
        public string StatusName { get; set; }
    }
}
