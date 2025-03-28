using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerJumpAttack : MeleeAttack
{
    private ActionController controller;

    private Vector2 movementIN;
    private bool sprintIN;

    protected override void Awake() {
        base.Awake();
        
        controller = GetComponentInParent<ActionController>();
    }

    protected override void OnEnable() {
        base.OnEnable();

        PlayerInputHandler.MoveEvent += MoveInput;
        PlayerInputHandler.SprintEvent += SprintInput;

        if(controller != null)
        {
            controller.AttackEvent += Attack;

            controller.ResetAction += ResetAnimationState;
        }
    }

    protected override void OnDisable() {
        base.OnDisable();

        PlayerInputHandler.MoveEvent -= MoveInput;
        PlayerInputHandler.SprintEvent -= SprintInput;

        if(controller != null)
        {
            controller.AttackEvent -= Attack;

            controller.ResetAction -= ResetAnimationState;
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

    protected override void Attack()
    {

        if(!sprintIN || movementIN.normalized.magnitude <= 0.25f) return;

        sprintIN = false;
        
        base.Attack();
        controller.ModifyMovement(movementModifier);
        controller.ModifyLook(lookModifier);
        controller.ModifyForwardStep(forwardStepModifier);
    }
}