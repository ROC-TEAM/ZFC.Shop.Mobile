using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZFC.Shop.Entity
{
    public class Suggestion<T>
    {
        public Suggestion(IEnumerable<T> data)
        {
            if (data == null)
            {
                data = new List<T>();
            }
            this.suggestions = data;
        }

        public IEnumerable<T> suggestions { get; private set; }
    }

    public class Suggestion : Suggestion<dynamic>
    {
        public Suggestion(IEnumerable<dynamic> data) : base(data)
        {

        }
    }
}
