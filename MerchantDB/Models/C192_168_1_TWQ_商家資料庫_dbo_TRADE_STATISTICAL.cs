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
    
    public partial class C192_168_1_TWQ_商家資料庫_dbo_TRADE_STATISTICAL
    {
        public string CustName { get; set; }
        public string ServiceType { get; set; }
        public Nullable<int> LastWeekAmount { get; set; }
        public Nullable<int> LastWeekCount { get; set; }
        public Nullable<int> LastWeekCredAmount { get; set; }
        public Nullable<int> LastWeekCredCount { get; set; }
        public Nullable<int> LastMonthAmount { get; set; }
        public Nullable<int> LastMonthCount { get; set; }
        public Nullable<int> LastMonthCredAmount { get; set; }
        public Nullable<int> LastMonthCredCount { get; set; }
        public Nullable<int> ThreeMonthsAmount { get; set; }
        public Nullable<int> ThreeMonthsCount { get; set; }
        public Nullable<int> ThreeMonthsCredAmount { get; set; }
        public Nullable<int> ThreeMonthsCredCount { get; set; }
        public Nullable<System.DateTime> LastWeekUpdateDate { get; set; }
        public Nullable<System.DateTime> LastMonthUpdateDate { get; set; }
        public Nullable<System.DateTime> ThreeMonthsUpdateDate { get; set; }
        public Nullable<System.DateTime> TradeDateFinally { get; set; }
    }
}
