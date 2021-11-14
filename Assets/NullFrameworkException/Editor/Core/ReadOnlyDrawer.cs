using UnityEditor;
using UnityEngine;

namespace NullFrameworkException.Editor
{
    // Now Unity knows to use .this drawer to render anything
    // with the Attribute: [ReadOnlyAttribute]
    [CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
    public class ReadOnlyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect _position, SerializedProperty _property, GUIContent _label)
        {
            EditorGUI.BeginProperty(_position, _label, _property);
            {
                // Disable the GUI, making this readonly, as it still renders, just becomes not-interactable
                GUI.enabled = false;
                {
                    // Render the property exactly as it already is
                    EditorGUI.PropertyField(_position, _property, _label);
                }
                // Re-enable the GUI to make everything work after this property
                GUI.enabled = true;
            }
            EditorGUI.EndProperty();
        }
        
        public override float GetPropertyHeight(SerializedProperty _property, GUIContent _label) => EditorGUI.GetPropertyHeight(_property);
    }
}