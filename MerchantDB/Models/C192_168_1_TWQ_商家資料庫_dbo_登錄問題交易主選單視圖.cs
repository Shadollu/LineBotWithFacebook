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
    
    public partial class C192_168_1_TWQ_商家資料庫_dbo_登錄問題交易主選單視圖
    {
        public string CustName { get; set; }
        public int NumCorrespondingly { get; set; }
        public string MerNo { get; set; }
        public string OrderNum { get; set; }
        public Nullable<int> Amount { get; set; }
        public string AuthCode { get; set; }
        public string CreditCard { get; set; }
        public Nullable<System.DateTime> NoticeDate { get; set; }
        public Nullable<System.DateTime> AuthDate { get; set; }
        public Nullable<bool> IS_Receipt { get; set; }
        public Nullable<System.DateTime> RefundDate { get; set; }
        public Nullable<System.DateTime> DisputeCollectDate { get; set; }
        public Nullable<System.DateTime> DeFundDate { get; set; }
        public string ReasonRemarks { get; set; }
        public string NoticeUnit { get; set; }
        public string BankName { get; set; }
        public string CountryOrigin { get; set; }
    }
}
