namespace test.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Nhanvien")]
    public partial class Nhanvien
    {
        [Key]
        public int manv { get; set; }

        [Required]
        [StringLength(30)]
        public string hotennv { get; set; }

        public int tuoi { get; set; }

        [StringLength(30)]
        public string diachi { get; set; }

        public int luongnv { get; set; }

        public int? maphong { get; set; }

        [Required]
        [StringLength(50)]
        public string matkhau { get; set; }

        public virtual Phongban Phongban { get; set; }
    }
}
