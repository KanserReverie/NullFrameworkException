using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NullFrameworkException.Test.CharacterController.Rigidbody3d
{
    /// <summary> Just exit the game on Esc. </summary>
    public class ExitScript : MonoBehaviour
    {
        // Update is called once per frame, but you probably already knew that.
        void Update()
        {
            Quit();
        }

        /// <summary> Basic Quit function to exit the game on "Esc". </summary>
        private void Quit()
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #endif
            }
        }
    }
}