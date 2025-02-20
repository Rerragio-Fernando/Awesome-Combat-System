using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerMovement : Movement
{
    private void OnEnable() {
        PlayerInputHandler.MoveEvent += MoveInput;
        PlayerInputHandler.SprintEvent += SprintInput;
        PlayerEventSystem.OnForwardStepEvent += ForwardStep;
    }

    private void OnDisable() {
        PlayerInputHandler.MoveEvent -= MoveInput;
        PlayerInputHandler.SprintEvent -= SprintInput;
        PlayerEventSystem.OnForwardStepEvent -= ForwardStep;
    }

    #region Input Functions
        void MoveInput(Vector2 move, InputActionPhase phase)
        {
            if(phase == InputActionPhase.Performed)
                _movementIN = move;
            else
                _movementIN = Vector2.zero;
        }
        void SprintInput(InputActionPhase phase)
        {
            if(phase == InputActionPhase.Started)
                _sprintIN = !_sprintIN;
        }
    #endregion
}