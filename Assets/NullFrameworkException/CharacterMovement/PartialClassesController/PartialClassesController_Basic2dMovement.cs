using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NullFrameworkException.CharacterMovement.PartialClassesController
{
    /// <summary>
    /// This class will be used for basic 2d movement.
    /// </summary>
    [RequireComponent(typeof(CharacterController))]
    public partial class PartialClassesController : MonoBehaviour
    {
        /// <summary>
        /// This is the horizontal input.
        /// </summary>
        [SerializeField] private Input horizontalInput;
        
        /// <summary>
        /// This is the character controller that will be added to the script
        /// </summary>
        [SerializeField] private CharacterController myController;

        /// <summary>
        /// This is the Awake function for this class.
        /// </summary>
        private void Awake_Basic2dMovement()
        {
            myController = GetComponentInChildren<CharacterController>();
        }
        
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}