using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerTargettingSystem : TargettingSystem
{
    private void OnEnable() {
        PlayerInputHandler.MoveEvent += MoveInput;
    }

    private void OnDisable() {
        PlayerInputHandler.MoveEvent -= MoveInput;
    }

    #region Input Functions
        void MoveInput(Vector2 move, InputActionPhase phase)
        {
            if(phase == InputActionPhase.Performed)
                movementIN = move;
            else
                movementIN = Vector2.zero;
        }
    #endregion
}