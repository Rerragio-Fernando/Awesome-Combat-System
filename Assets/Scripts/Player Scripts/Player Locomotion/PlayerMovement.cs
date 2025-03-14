using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerMovement : MovementAnimator
{
    private ActionController controller;

    private void Awake() {
        controller = GetComponentInParent<ActionController>();
    }

    protected override void OnEnable() {
        base.OnEnable();
        
        PlayerInputHandler.MoveEvent += MoveInput;
        PlayerInputHandler.SprintEvent += SprintInput;

        if(controller != null)
        {
            controller.ModifyMovement += ModifyMovement;
        }
    }

    protected override void OnDisable() {
        base.OnDisable();

        PlayerInputHandler.MoveEvent -= MoveInput;
        PlayerInputHandler.SprintEvent -= SprintInput;

        if(controller != null)
        {
            controller.ModifyMovement -= ModifyMovement;
        }
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