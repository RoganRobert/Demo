using UnityEngine;

namespace Core
{
    public class InputHandler : IInputHandler
    {
        public Vector3 GetInput()
        {
            Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
            return input;
        }

        public bool GetJumpInput()
        {
            return Input.GetButtonDown("Jump");
        }

        public bool IsSprinting()
        {
            if (GetInput().magnitude > 0 && Input.GetKey(KeyCode.LeftShift))
            {
                return true;
            }
            return false;
        }
        
        
    }

    public interface IInputHandler
    {
        public Vector3 GetInput();
        public bool GetJumpInput();

        public bool IsSprinting();

    }
}