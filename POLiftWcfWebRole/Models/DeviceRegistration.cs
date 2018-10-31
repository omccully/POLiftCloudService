using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace POLiftWcfWebRole.Models
{
    public class DeviceRegistration
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(64)]
        [Index(IsUnique = true)]
        public string DeviceId { get; set; }

        public DateTime Time { get; set; }

        public DeviceRegistration()
        {

        }

        public DeviceRegistration(string deviceId, DateTime time)
        {
            this.DeviceId = deviceId;
            this.Time = time;
        }

        public override string ToString()
        {
            return $"#{Id}. {DeviceId} {Time}";
        }
    }
}