//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace MerchantDB.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class MPOS_TID
    {
        public string CustName { get; set; }
        public string MerNo_F { get; set; }
        public string Machine_No { get; set; }
        public string MID { get; set; }
        public string TID { get; set; }
        public Nullable<System.DateTime> OnCmpTime { get; set; }
        public string Description { get; set; }
        public Nullable<int> MPOSCorreCode { get; set; }
        public int No { get; set; }
    
        public virtual 正式商店設定資料 正式商店設定資料 { get; set; }
    }
}
