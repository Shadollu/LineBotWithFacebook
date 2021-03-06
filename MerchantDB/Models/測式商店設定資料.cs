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
    
    public partial class 測式商店設定資料
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public 測式商店設定資料()
        {
            this.測試商店備註欄 = new HashSet<測試商店備註欄>();
        }
    
        public int No { get; set; }
        public string MerNo_T { get; set; }
        public string CustName { get; set; }
        public string CooperMode { get; set; }
        public string MerState { get; set; }
        public string MerType2 { get; set; }
        public string MerType { get; set; }
        public string HostID { get; set; }
        public Nullable<System.DateTime> Test_Ntc_Date { get; set; }
        public Nullable<System.DateTime> Test_Cmp_Date { get; set; }
        public Nullable<bool> Auth { get; set; }
        public string NewebSales { get; set; }
        public string Remarks { get; set; }
        public Nullable<bool> InspectStatus { get; set; }
        public Nullable<System.DateTime> Created { get; set; }
        public Nullable<bool> IS_IM { get; set; }
        public Nullable<bool> IS_CTID { get; set; }
        public Nullable<bool> OnlyTest { get; set; }
        public Nullable<System.DateTime> TestDelDate { get; set; }
        public string TestPassword { get; set; }
        public Nullable<bool> CS_VAPCHK { get; set; }
        public Nullable<bool> CS_WEBATMCHK { get; set; }
        public Nullable<bool> CS_IBONCHK { get; set; }
        public Nullable<bool> CS_FAMIPORTCHK { get; set; }
        public Nullable<bool> CS_LIFEETCHK { get; set; }
        public Nullable<bool> CS_OKGOCHK { get; set; }
        public Nullable<bool> CS_AlipayCHK { get; set; }
        public Nullable<bool> CS_AlipayWAPCHK { get; set; }
        public Nullable<bool> CS_CSCHK { get; set; }
        public string VAP_MerState { get; set; }
        public string MMK_MerState { get; set; }
        public string CS_MerState { get; set; }
        public string ALIPAY_MerState { get; set; }
    
        public virtual HOSTS HOSTS { get; set; }
        public virtual MER_STATE MER_STATE { get; set; }
        public virtual MER_TYPE MER_TYPE { get; set; }
        public virtual 客戶基本資料 客戶基本資料 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<測試商店備註欄> 測試商店備註欄 { get; set; }
    }
}
