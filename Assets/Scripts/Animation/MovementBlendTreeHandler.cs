using UnityEngine;

public class MovementBlendTreeHandler : MonoBehaviour
{
    private Weapon weapon;
    private string blendTreeStateName;
    private Animator anim;

    private void Awake() {
        weapon = GetComponent<Weapon>();
        anim = GetComponentInParent<Animator>();
        blendTreeStateName = weapon.PlayerWeaponData.weaponMovementBlendTreeName;
    }

    private void OnEnable() {
        AnimationServices.PlayAnimation(anim, blendTreeStateName, 0);
    }
}