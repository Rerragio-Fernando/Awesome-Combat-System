using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerMovement : MovementAnimator
{
    private void OnEnable() {
        PlayerInputHandler.MoveEvent += MoveInput;
        PlayerInputHandler.SprintEvent += SprintInput;
    }

    private void OnDisable() {
        PlayerInputHandler.MoveEvent -= MoveInput;
        PlayerInputHandler.SprintEvent -= SprintInput;
    }

    #region Input Functions
        void MoveInput(Vector2 move, InputActionPhase phase)
        {
            if(phase == InputActionPhase.Performed)
                movementIN = move;
            else
                movementIN = Vector2.zero;
        }
        void SprintInput(InputActionPhase phase)
        {
            if(phase == InputActionPhase.Started)
                sprintIN = !sprintIN;
        }
    #endregion
}