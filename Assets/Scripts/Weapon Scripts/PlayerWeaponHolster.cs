using UnityEngine;

public class PlayerWeaponHolster : WeaponHolster
{
    private ActionController controller;

    private void Awake() {
        controller = GetComponentInParent<ActionController>();
    }

    private void OnEnable() {
        if(controller != null)
        {
            controller.CycleWeapon += CycleWeapon;
            controller.ResetAction += ResetAnimationState;
        }
    }

    private void OnDisable() {
        if(controller != null)
        {
            controller.CycleWeapon -= CycleWeapon;
            controller.ResetAction -= ResetAnimationState;
        }
    }
}