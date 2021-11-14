using System;

using UnityEngine;

namespace NullFrameworkException.Mobile
{
    public class GyroInputHandler : RunnableBehaviour
    {
        [Serializable]
        public class GyroscopeState
        {
            public Vector3 rotationDelta;
            public Quaternion deviceRotation;
        }

        public GyroscopeState gyroscopeState = new GyroscopeState();
        
        protected override void OnSetup(params object[] _params)
        {
            // Detect if the gyroscrope is actually supported on this device (most should)
            Input.gyro.enabled = SystemInfo.supportsGyroscope;
        }

        protected override void OnRun(params object[] _params)
        {
            gyroscopeState.deviceRotation = Input.gyro.attitude;
            gyroscopeState.rotationDelta = Input.gyro.rotationRateUnbiased;
        }
    }
}