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
    
    public partial class OnCmpData
    {
        public Nullable<int> No { get; set; }
        public int OnCmpCorreCode { get; set; }
        public string Merno_F { get; set; }
        public string Custname { get; set; }
        public string Mertype { get; set; }
        public string SystemAccount { get; set; }
        public string SystemPassword { get; set; }
        public string TransAccount { get; set; }
        public string TransPassword { get; set; }
        public string ReportAccount { get; set; }
        public string ReportPassword { get; set; }
        public string R_code { get; set; }
        public string Code { get; set; }
        public string MappingCode { get; set; }
        public string SFTPAccount { get; set; }
        public string SFTPPassword { get; set; }
        public string VPOS_Account { get; set; }
        public string VPOS_Password { get; set; }
        public Nullable<bool> quick_Ticket { get; set; }
        public string Trans_Code { get; set; }
        public Nullable<System.DateTime> sendMail_Time { get; set; }
    
        public virtual 正式商店設定資料 正式商店設定資料 { get; set; }
    }
}
