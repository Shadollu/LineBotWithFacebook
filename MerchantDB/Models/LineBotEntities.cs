namespace MerchantDB.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class LineBotEntities : DbContext
    {
        public LineBotEntities()
            : base("name=LineBotEntities")
        {
        }

        public virtual DbSet<LAT_LON> LAT_LON { get; set; }
        public virtual DbSet<PARKING> PARKING { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
