using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Xml;

namespace POLiftWcfWebRole.Models
{
    [DataContract]
    public class LiftingProgram : ConfigurationElement
    {
        [DataMember]
        [ConfigurationProperty("title", IsRequired = true, IsKey = true)]
        public string Title
        {
            get { return base["title"] as string; }
            set { base["title"] = value; }
        }

        [DataMember]
        [ConfigurationProperty("description", IsRequired = false)]
        public string Description {
            get { return base["description"] as string; }
            set { base["description"] = value; }
        }

        [DataMember]
        [ConfigurationProperty("downloadUrl", IsRequired = true)]
        public string DownloadUrl {
            get { return base["downloadUrl"] as string; }
            set { base["downloadUrl"] = value; }
        }

        public LiftingProgram()
        {

        }

        

        // override object.Equals
        public override bool Equals(object obj)
        {
            //       
            // See the full list of guidelines at
            //   http://go.microsoft.com/fwlink/?LinkID=85237  
            // and also the guidance for operator== at
            //   http://go.microsoft.com/fwlink/?LinkId=85238
            //

            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            LiftingProgram lp = (LiftingProgram)obj;

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

        public static bool operator ==(LiftingProgram obj1, LiftingProgram obj2)
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


        public static bool operator !=(LiftingProgram obj1, LiftingProgram obj2)
        {
            return !(obj1 == obj2);
        }
    }
}