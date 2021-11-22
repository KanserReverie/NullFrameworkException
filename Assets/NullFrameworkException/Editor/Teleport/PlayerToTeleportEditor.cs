using NullFrameworkException.Teleport;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.AnimatedValues;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace NullFrameworkException.Editor.Teleport
{
    // It will effect everything that is a "Spawner".
    [CustomEditor(typeof(PlayerToTeleport))]
    public class PlayerToTeleportEditor : UnityEditor.Editor
    {
        // The reference to the component we are drawing the editor for.
        private PlayerToTeleport playerToTeleport;

        // The references to the values of the variables in held in the script.
        private SerializedProperty teleportPointsProperty;
        private SerializedProperty teleportOnKeypressProperty;
        private SerializedProperty teleportClosestKeyProperty;
        
        // The custom animation and scene elements
        private AnimBool shouldTeleportOnKeypress = new AnimBool(); // This allows the animation of showing the boss variables when the toggle is on
        private BoxBoundsHandle handle; // This is the thing that will allow us to edit the bounds in the SceneView
        
        // OnEnable is the Start of custom inspectors
        private void OnEnable()
        {
            // Convert the object that is being targeted to a spawner type as we know it is.
            playerToTeleport = target as PlayerToTeleport;

            // Retrieve the serializedProperties from the object
            teleportPointsProperty = serializedObject.FindProperty("referenceTeleportPoints");
            teleportOnKeypressProperty = serializedObject.FindProperty("teleportOnKeypress");
            teleportClosestKeyProperty = serializedObject.FindProperty("teleportClosestKey");
            
            // Set the animation bool for the bossSpawning and create the handle
            shouldTeleportOnKeypress.value = teleportOnKeypressProperty.boolValue;
            shouldTeleportOnKeypress.valueChanged.AddListener(Repaint);
            handle = new BoxBoundsHandle();
        }
        // This allows us to modify and draw things in the SceneView
        private void OnSceneGUI()
        {
            // Set the handles colour to green and store the original matrix value
            Handles.color = Color.green;
            Matrix4x4 original = Handles.matrix;
            
            // Change the Handles matrix to be using the transform of this object
            Handles.matrix = playerToTeleport.transform.localToWorldMatrix;

            // Begin listening for changes to the handle and draw it
            EditorGUI.BeginChangeCheck();
            handle.DrawHandle();
            
            // Check if any changes were made
            if(EditorGUI.EndChangeCheck())
            {
                // Register this change for Undo-redo system
                Undo.RecordObject(playerToTeleport, "UPDATE_SPAWNER_BOUNDS");
            }
            
            // Reset the handles matrix back to the original one
            Handles.matrix = original;
        }
        // This is where we draw the custom inspector window and render the scripts properties
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.BeginVertical(GUI.skin.box);
            {
                EditorGUILayout.PropertyField(teleportPointsProperty);
                string label = "All active points";
                
                
                
                // Apply some spacing between lines
                EditorGUILayout.Space();
                
                
                GUIStyle header = GUI.skin.label;
                header.fontStyle = FontStyle.Bold;
                EditorGUILayout.LabelField("Activation Options",header);
                
                EditorGUILayout.PropertyField(teleportOnKeypressProperty);
                
                // Attempt to fade the next variables in and out
                shouldTeleportOnKeypress.target = teleportOnKeypressProperty.boolValue;
                if(EditorGUILayout.BeginFadeGroup(shouldTeleportOnKeypress.faded))
                {
                    // Only visible when 'shouldSpawnBoss' in Spawner script is true
                    // Indent the editor
                    EditorGUI.indentLevel++;
                    {
                        // Draw bossSpawnChance and bossPrefab as normal
                        EditorGUILayout.PropertyField(teleportClosestKeyProperty);
                    }
                    EditorGUI.indentLevel--;
                }
                EditorGUILayout.EndFadeGroup();
            }
            EditorGUILayout.EndVertical();

            // Apply the changes we made in the inspector
            serializedObject.ApplyModifiedProperties(); 
        }
    }
}