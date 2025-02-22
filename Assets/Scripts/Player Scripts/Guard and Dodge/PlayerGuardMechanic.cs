using UnityEngine;

public class PlayerGuardMechanic : GuardMechanic
{
    private ActionController controller;

    private void Awake() {
        controller = GetComponentInParent<ActionController>();
    }

    private void OnEnable() {
        if(controller != null)
        {
            controller.Guard += Guard;
            controller.ResetAction += ResetAnimationState;
        }
    }

    private void OnDisable() {
        if(controller != null)
        {
            controller.Guard -= Guard;
            controller.ResetAction -= ResetAnimationState;
        }
    }
}