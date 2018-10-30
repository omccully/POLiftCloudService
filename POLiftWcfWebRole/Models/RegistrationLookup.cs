using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace POLiftWcfWebRole.Models
{
    public class RegistrationLookup
    {
        [Key]
        public int Id { get; set; }

        public string DeviceId { get; set; }

        public DateTime Time { get; set; }

        public string ClientAddress { get; set; }

        public RegistrationLookup()
        {

        }

        public RegistrationLookup(string deviceId, DateTime time, string clientAddress)
        {
            this.DeviceId = deviceId;
            this.Time = time;
            this.ClientAddress = clientAddress;
        }

    }
}