//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace prj_chuju
{
    using System;
    using System.Collections.Generic;
    
    public partial class regionList
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public regionList()
        {
            this.accountInfo = new HashSet<accountInfo>();
        }
    
        public int id { get; set; }
        public string regionName { get; set; }
        public int cityID { get; set; }
    
        public virtual cityList cityList { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<accountInfo> accountInfo { get; set; }
    }
}
