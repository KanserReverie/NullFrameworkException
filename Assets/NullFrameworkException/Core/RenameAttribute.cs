using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NullFrameworkException
{
    /// <summary> You cant normally change the attribute name in inspector.
    /// This is to do just that. </summary>
    public class RenameAttribute : PropertyAttribute
    {
        public string NewName { get ; private set; }    
        public RenameAttribute( string name )
        {
            NewName = name ;
        }
    }
}