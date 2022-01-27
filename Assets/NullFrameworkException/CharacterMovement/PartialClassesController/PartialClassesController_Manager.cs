using System;
using UnityEngine;

namespace NullFrameworkException.CharacterMovement.PartialClassesController
{
    public partial class PartialClassesController : MonoBehaviour
    {
        [SerializeField] private bool usingBasic2dMovement = false;

        /// <summary>
        /// Add all the partial class awake functions to this.
        /// </summary>
        private void Awake()
        {
            Awake_Basic2dMovement();
        }
    }
}