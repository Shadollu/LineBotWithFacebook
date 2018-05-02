namespace MerchantDB.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PARKING")]
    public partial class PARKING
    {
        [Key]
        [StringLength(255)]
        public string UID { get; set; }

        [StringLength(255)]
        public string LAT { get; set; }

        [StringLength(255)]
        public string LON { get; set; }
    }
}
