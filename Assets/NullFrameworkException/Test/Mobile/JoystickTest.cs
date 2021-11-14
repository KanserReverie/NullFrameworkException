using NullFrameworkException.Mobile.InputHandling;

using UnityEngine;

namespace NullFrameworkException.Test.Mobile
{
    public class JoystickTest : MonoBehaviour
    {
        // Update is called once per frame
        private void Update()
        {
            Vector2 joystickAxis = MobileInputManager.GetJoystickAxis();
            
            transform.position += transform.right * (joystickAxis.x * Time.deltaTime);
            transform.position += transform.forward * (joystickAxis.y * Time.deltaTime);
        }
    }
}