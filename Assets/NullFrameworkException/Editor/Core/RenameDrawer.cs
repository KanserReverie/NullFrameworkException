using UnityEditor;
using UnityEngine;

namespace NullFrameworkException.Editor
{
    /// <summary> This is to allow you to rename an attribute in inspector :) ~ Thanks Kieran </summary>
    [CustomPropertyDrawer(typeof(RenameAttribute))]
    public class RenameDrawer : PropertyDrawer
    {
        /// <summary> Gets in the attribute name and will display it as the new attribute in the Inspector. </summary>
        public override void OnGUI( Rect position, SerializedProperty property, GUIContent label )
        {
            EditorGUI.PropertyField(position, property, new GUIContent( (attribute as RenameAttribute).NewName ));
        }
    }
}