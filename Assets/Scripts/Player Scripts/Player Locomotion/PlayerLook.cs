using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerLook : Look
{
    private void OnEnable() 
    {
        PlayerInputHandler.LookEvent += LookInput;

        PlayerEventSystem.CombatStateEvent += CheckCombatState;
    }

    private void OnDisable() 
    {
        PlayerInputHandler.LookEvent -= LookInput;

        PlayerEventSystem.CombatStateEvent -= CheckCombatState;
    }

    void CheckCombatState(CombatState cs)
    {
        if(cs == CombatState.NotAttacking)
            notAttacking = true;
        else
            notAttacking = false;
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