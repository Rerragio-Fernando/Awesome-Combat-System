using UnityEngine;

public class PlayerStrongAttack : MeleeAttack
{
    private ActionController controller;

    protected override void Awake() {
        base.Awake();

        controller = GetComponentInParent<ActionController>();
    }

    protected override void OnEnable() {
        base.OnEnable();

        if(controller != null)
        {
            controller.StrongAttack += Attack;
            controller.ResetAction += ResetAnimationState;
        }
    }

    protected override void OnDisable() {
        base.OnDisable();
        
        if(controller != null)
        {
            controller.StrongAttack -= Attack;
            controller.ResetAction -= ResetAnimationState;
        }
    }

    protected override void Attack()
    {
        base.Attack();
        controller.ModifyMovement(movementModifier);
        controller.ModifyLook(lookModifier);
        controller.ModifyForwardStep(forwardStepModifier);
    }
}