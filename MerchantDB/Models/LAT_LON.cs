namespace MerchantDB.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class LAT_LON
    {
        [Key]
        [StringLength(255)]
        public string UID { get; set; }

        [StringLength(255)]
        public string LAT { get; set; }

        [StringLength(255)]
        public string LON { get; set; }

        [StringLength(10)]
        public string COUNTRY { get; set; }
    }
}
