using System;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace NullFrameworkException.Test.Core.AttributeTests
{
    [SuppressMessage("ReSharper", "NotAccessedField.Local")] public class AttributeTests : MonoBehaviour
    {
        [TextArea(2,4)] [SerializeField] private string toolTip = 
            "See now it shows all Tags in the project so you wont miss typing in a string";
        [Tag, SerializeField] private string playerTag;
        
        [Space(10)]
        
        [TextArea(2,4)] [SerializeField] private string toolTip2 = 
            "In this case we make it default to 'Finish'";
        [Tag, SerializeField] private string finishTag = "Finish";

        [Space(10)]
        
        [TextArea(2, 4)] [SerializeField] private string toolTip3
            = "This is a readonly attribute. It can be seen but not edited in inspector despite [SerializeField]";
        [SerializeField, ReadOnly] private string thing = "12346";
    }
}