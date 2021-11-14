using UnityEditor;
using UnityEngine;

// Just ".Editor" because we ignore ".Core"
// If it were say mobile it would be:
// namespace NullFrameworkException.Editor.Mobile
namespace NullFrameworkException.Editor
{
    // Now Unity knows to use .this drawer to render anything
    // with the Attribute: [TagAttribute]
    [CustomPropertyDrawer(typeof(TagAttribute))]
    public class TagDrawer : PropertyDrawer // "Drawer" is telling that it should draw this.
    {
        // This will override the current drawer.
        public override void OnGUI(Rect _position, SerializedProperty _property, GUIContent _label)
        {
            // Start drawing this specific instance of the tag property
            EditorGUI.BeginProperty(_position, _label, _property); // BEGINNING, FOR EVERY BEGINNING...
            // Indicates the block of code is part of the property using { brackets }.
            {
                //Determine if the property was set to nothing by default
                bool isNotSet = string.IsNullOrEmpty(_property.stringValue);
				
                // Draw the string as a tag instead of a normal string box
                _property.stringValue = EditorGUI.TagField(_position, _label, 
                    isNotSet ? (_property.serializedObject.targetObject as Component)?.gameObject.tag : _property.stringValue);
                // If the property is not set it will set it to the gameObject tag is.  
            }
            // Stop drawing this specific instance of the tag property
            EditorGUI.EndProperty(); // ...YOU NEED AN END
        }

        // Tell the inspector to just use a single line for this property.
        public override float GetPropertyHeight(SerializedProperty _property, GUIContent _label) 
            => EditorGUIUtility.singleLineHeight;
    }
} 