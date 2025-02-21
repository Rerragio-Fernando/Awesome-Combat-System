using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerLook : Look
{
    private void OnEnable() 
    {
        PlayerInputHandler.LookEvent += LookInput;
    }

    private void OnDisable() 
    {
        PlayerInputHandler.LookEvent -= LookInput;
    }

    #region Input Functions
        void LookInput(Vector2 look, InputActionPhase phase)
        {
            if(phase == InputActionPhase.Performed)
                lookIN = look;
            else
                lookIN = Vector2.zero;
        }
    #endregion
}