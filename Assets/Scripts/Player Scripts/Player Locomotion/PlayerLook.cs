using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerLook : Look
{
    private ActionController controller;

    private void Awake() {
        controller = GetComponentInParent<ActionController>();
    }

    private void OnEnable() 
    {
        PlayerInputHandler.LookEvent += LookInput;

        if(controller != null)
        {
            controller.ModifyLook += ModifyLook;
        }
    }

    private void OnDisable() 
    {
        PlayerInputHandler.LookEvent -= LookInput;

        if(controller != null)
        {
            controller.ModifyLook -= ModifyLook;
        }
    }

    #region Input Functions
        void LookInput(Vector2 look, InputActionPhase phase)
        {
            if(phase == InputActionPhase.Performed)
                lookIN = look;
            else
                lookIN = Vector2.zero;
            
            lookIN = new Vector2(Mathf.Clamp(lookIN.x, -lookLimitter, lookLimitter), 
            Mathf.Clamp(lookIN.y, -lookLimitter, lookLimitter));
        }
    #endregion
}