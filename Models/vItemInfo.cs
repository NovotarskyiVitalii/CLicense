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
    using System.Collections.Generic;
    
    public partial class vItemInfo
    {
        public int CID { get; set; }
        public Nullable<int> ClassParent { get; set; }
        public string ClassCode { get; set; }
        public string ClassName { get; set; }
        public string ClassNote { get; set; }
        public int IID { get; set; }
        public Nullable<int> Parent { get; set; }
        public string ItemCode { get; set; }
        public string ItemCodeLit { get; set; }
        public Nullable<int> ItemCodeNum { get; set; }
        public string ItemName { get; set; }
        public string FullName { get; set; }
        public string Note { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> ExpireDate { get; set; }
        public short Status { get; set; }
    }
}