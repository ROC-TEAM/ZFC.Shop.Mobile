using System;

namespace ZFC.Shop.Entity
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ColumnAttribute : Attribute
    {
        public ColumnAttribute(bool ignore, string name)
        {
            this.IsKey = false;
            this.Increment = false;
            this.Ignore = ignore;
            this.Name = name;
        }

        public ColumnAttribute(bool isKey, bool increment = true, string name="")
        {
            this.Ignore = false;
            this.IsKey = IsKey;
            this.Increment = increment;
            this.Name = name;
        }

        public ColumnAttribute(string name)
        {
            this.IsKey = false;
            this.Increment = false;
            this.Ignore = false;
            this.Name = name;
        }

        public string Name { get; set; }

        public bool IsKey { get; set; }

        public bool Ignore { get; set; }

        public bool Increment { get; set; }
    }
}
