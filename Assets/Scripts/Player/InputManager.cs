using UnityEngine;

namespace Player
{
    internal static class InputManager
    {
        public static float GetMovement()
        {
            return Input.GetAxis("Horizontal");
        }

        public static bool GetJump()
        {
            return Input.GetKeyDown(KeyCode.Space);
        }
        
        public static bool GetFly()
        {
            return Input.GetButton("Jump");
        }


        public static bool GetCrouch()
        {
            return Input.GetKey(KeyCode.LeftControl);
        }

        public static bool GetDash()
        {
            return Input.GetKeyDown(KeyCode.LeftShift);
        }
    }
}