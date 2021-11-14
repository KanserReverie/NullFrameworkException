using System;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

namespace NullFrameworkException.Test.Core.SceneDragNDropTest
{
    public class SceneDragNDropTest : MonoBehaviour
    {
        [SerializeField, Rename("Title for the Scene")] private Text titleSceneTextbox;
        [Header("Scene Fields")]
        [SerializeField, SceneField] private string mainScene;
        [SerializeField, SceneField] private string scene1;
        [SerializeField, SceneField] private string scene2;
        [SerializeField, SceneField] private string scene3;

        /// <summary> This will make the title = the current scene name. </summary>
        private void Start() => titleSceneTextbox.text = SceneManager.GetActiveScene().name;
        
        /// <summary> This will load in the dragged in mainScene. </summary>
        public void GoTo_mainScene() { SceneManager.LoadScene(mainScene); }
        
        /// <summary> This will load in the dragged in scene1. </summary>
        public void GoTo_scene1() => SceneManager.LoadScene(scene1);
        
        /// <summary> This will load in the dragged in scene2. </summary>
        public void GoTo_scene2() => SceneManager.LoadScene(scene2);
        /// <summary> This will load in the dragged in scene3. </summary>
        public void GoTo_scene3() => SceneManager.LoadScene(scene3);
    }
}