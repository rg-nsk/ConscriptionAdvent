namespace ConscriptionAdvent.Data.Firebird.Dto
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Firebird.VID_VS")]
    public partial class VID_VS
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [StringLength(26)]
        public string NAME { get; set; }
    }
}
