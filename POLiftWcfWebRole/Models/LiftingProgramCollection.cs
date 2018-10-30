using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace POLiftWcfWebRole.Models
{
    [ConfigurationCollection(typeof(LiftingProgram), 
        AddItemName = "liftingProgram", 
        CollectionType = ConfigurationElementCollectionType.BasicMap)]
    public class LiftingProgramCollection : ConfigurationElementCollection
    {

        protected override ConfigurationElement CreateNewElement()
        {
            return new LiftingProgram();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((LiftingProgram)element).Title;
        }

        protected override string ElementName => "liftingProgram";

        protected override bool IsElementName(string elementName)
        {
            return elementName.Equals(ElementName,
              StringComparison.InvariantCultureIgnoreCase);
        }

        public override bool IsReadOnly()
        {
            return false;
        }

        public override ConfigurationElementCollectionType CollectionType => 
            ConfigurationElementCollectionType.BasicMap;

        public LiftingProgram this[int index]
        {
            get
            {
                return (LiftingProgram)BaseGet(index);
            }
            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }
                BaseAdd(index, value);
            }
        }

        new public LiftingProgram this[string name]
        {
            get
            {
                return (LiftingProgram)BaseGet(name);
            }
        }

        public int IndexOf(LiftingProgram details)
        {
            return BaseIndexOf(details);
        }

        public void Add(LiftingProgram details)
        {
            BaseAdd(details);
        }

        protected override void BaseAdd(ConfigurationElement element)
        {
            BaseAdd(element, false);
        }

        public void Remove(LiftingProgram details)
        {
            if (BaseIndexOf(details) >= 0)
                BaseRemove(details.Title);
        }

        public void RemoveAt(int index)
        {
            BaseRemoveAt(index);
        }

        public void Remove(string name)
        {
            BaseRemove(name);
        }

        public void Clear()
        {
            BaseClear();
        }

    }
}