using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frilp
{
    public class SearchResults<T1,T2>
    {
        public Dictionary<T1, T2> invalidItems = new Dictionary<T1, T2>();
        public Dictionary<T1, T2> validItems = new Dictionary<T1, T2>();
        public List<T1> duplicateItems = new List<T1>();
        public List<T1> duplicateandInvalidItems = new List<T1>();
    }
}
