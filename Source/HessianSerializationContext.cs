using System;
using System.Collections;
using System.Collections.Generic;

namespace LibraProgramming.Serialization.Hessian
{
    /// <summary>
    /// 
    /// </summary>
    public class HessianSerializationContext
    {
        /// <summary>
        /// 
        /// </summary>
        public IList<Type> Classes
        {
            get;
            private set;
        }

        /// <summary>
        /// 
        /// </summary>
        public IList Instances
        {
            get;
            private set;
        }

        /// <summary>
        /// 
        /// </summary>
        public HessianSerializationContext()
        {
            Classes = new List<Type>();
            Instances = new List<object>();
        }
    }
}