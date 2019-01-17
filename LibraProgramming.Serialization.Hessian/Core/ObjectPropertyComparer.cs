using System;
using System.Collections.Generic;

namespace LibraProgramming.Serialization.Hessian.Core
{
    internal class ObjectPropertyComparer : IComparer<PropertyElement>
    {
        private readonly IComparer<int> comparer;

        public ObjectPropertyComparer()
        {
            comparer = Comparer<int>.Default;
        }

        public int Compare(PropertyElement left, PropertyElement right)
        {
            var result = comparer.Compare(left.PropertyOrder, right.PropertyOrder);
            return 0 == result
                ? String.Compare(left.PropertyName, right.PropertyName, StringComparison.Ordinal)
                : result;
        }
    }
}