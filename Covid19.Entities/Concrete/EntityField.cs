using System;

namespace Covid19.Entities.Concrete
{
    [Serializable]
    public class EntityField
    {
        public EntityField(string name,string value)
        {
            this.Name = name;
            this.Value = value; 
        }
        public string Name { get; set; }
        public string Value { get; set; }    }
}
