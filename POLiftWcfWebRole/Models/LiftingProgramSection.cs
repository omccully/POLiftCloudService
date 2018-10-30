using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace POLiftWcfWebRole.Models
{
    public class LiftingProgramSection : ConfigurationSection
    {
        private static readonly ConfigurationProperty _propInfo = new ConfigurationProperty(
           null,
           typeof(LiftingProgramCollection),
           null,
           ConfigurationPropertyOptions.IsDefaultCollection
        );

        private static ConfigurationPropertyCollection _properties = new ConfigurationPropertyCollection();

        public LiftingProgramSection()
        {
            _properties.Add(_propInfo);
        }

        [ConfigurationProperty("", Options = ConfigurationPropertyOptions.IsDefaultCollection)]
        public LiftingProgramCollection LiftingPrograms
        {
            get { return (LiftingProgramCollection)base[_propInfo]; }
        }
    }
}