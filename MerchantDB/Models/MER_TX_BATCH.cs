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
    
    public partial class MER_TX_BATCH
    {
        public int NUM { get; set; }
        public string MERCHANTNUMBER { get; set; }
        public string BATCHNUMBER { get; set; }
        public System.DateTime CAPTUREDATE { get; set; }
        public Nullable<int> TOTALCOUNT { get; set; }
        public Nullable<int> TOTALAMOUNT { get; set; }
        public Nullable<int> TOTALCREDCOUNT { get; set; }
        public Nullable<int> TOTALCREDAMOUNT { get; set; }
        public string SERVICE_TYPE { get; set; }
        public string ACQUIRE_BANK { get; set; }
    }
}
