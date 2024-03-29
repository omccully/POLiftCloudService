﻿using System;
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
    public class LiftingProgram
    {
        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string FileName { get; set; }

        public LiftingProgram()
        {

        }

        // override object.Equals
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            LiftingProgram lp = (LiftingProgram)obj;

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
                obj1.FileName == obj2.FileName;
        }


        public static bool operator !=(LiftingProgram obj1, LiftingProgram obj2)
        {
            return !(obj1 == obj2);
        }
    }
}