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

        [ConfigurationProperty("downloadUrl", IsRequired = true)]
        public string DownloadUrl
        {
            get { return base["downloadUrl"] as string; }
            set { base["downloadUrl"] = value; }
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
                DownloadUrl = this.DownloadUrl
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
                DownloadUrl == lp.DownloadUrl;
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            // TODO: write your implementation of GetHashCode() here
            return Title.GetHashCode() ^ Description.GetHashCode() ^
                DownloadUrl.GetHashCode();
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
                obj1.DownloadUrl == obj2.DownloadUrl;
        }

        public static bool operator !=(LiftingProgramElement obj1,
            LiftingProgramElement obj2)
        {
            return !(obj1 == obj2);
        }
    }
}