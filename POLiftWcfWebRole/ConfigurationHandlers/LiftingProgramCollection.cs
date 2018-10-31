using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace POLiftWcfWebRole.ConfigurationHandlers
{
    [ConfigurationCollection(typeof(LiftingProgramElement), 
        AddItemName = "liftingProgram", 
        CollectionType = ConfigurationElementCollectionType.BasicMap)]
    public class LiftingProgramCollection : ConfigurationElementCollection
    {

        protected override ConfigurationElement CreateNewElement()
        {
            return new LiftingProgramElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((LiftingProgramElement)element).Title;
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

        public LiftingProgramElement this[int index]
        {
            get
            {
                return (LiftingProgramElement)BaseGet(index);
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

        new public LiftingProgramElement this[string name]
        {
            get
            {
                return (LiftingProgramElement)BaseGet(name);
            }
        }

        public int IndexOf(LiftingProgramElement details)
        {
            return BaseIndexOf(details);
        }

        public void Add(LiftingProgramElement details)
        {
            BaseAdd(details);
        }

        protected override void BaseAdd(ConfigurationElement element)
        {
            BaseAdd(element, false);
        }

        public void Remove(LiftingProgramElement details)
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