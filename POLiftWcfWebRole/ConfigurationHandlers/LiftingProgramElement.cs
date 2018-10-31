using POLiftWcfWebRole.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace POLiftWcfWebRole.ConfigurationHandlers
{
    public class LiftingProgramElement : ConfigurationElement
    {
        [ConfigurationProperty("title", IsRequired = true, IsKey = true)]
        public string Title
        {
            get { return base["title"] as string; }
            set { base["title"] = value; }
        }

        [ConfigurationProperty("description", IsRequired = false)]
        public string Description
        {
            get { return base["description"] as string; }
            set { base["description"] = value; }
        }

        [ConfigurationProperty("fileName", IsRequired = true)]
        public string FileName
        {
            get { return base["fileName"] as string; }
            set { base["fileName"] = value; }
        }

        public LiftingProgramElement()
        {

        }

        public LiftingProgram ToLiftingProgram()
        {
            return new LiftingProgram()
            {
                Title = this.Title,
                Description = this.Description,
                FileName = this.FileName
            };
        }

        // override object.Equals
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            LiftingProgramElement lp = (LiftingProgramElement)obj;

            return Title == lp.Title &&
                Description == lp.Description &&
                FileName == lp.FileName;
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            // TODO: write your implementation of GetHashCode() here
            return Title.GetHashCode() ^ Description.GetHashCode() ^
                FileName.GetHashCode();
        }

        public static bool operator ==(LiftingProgramElement obj1,
            LiftingProgramElement obj2)
        {
            if (ReferenceEquals(obj1, obj2))
            {
                return true;
            }

            if (obj1 == null || obj2 == null)
            {
                return false;
            }

            return obj1.Title == obj2.Title &&
                obj1.Description == obj2.Description &&
                obj1.FileName == obj2.FileName;
        }

        public static bool operator !=(LiftingProgramElement obj1,
            LiftingProgramElement obj2)
        {
            return !(obj1 == obj2);
        }
    }
}