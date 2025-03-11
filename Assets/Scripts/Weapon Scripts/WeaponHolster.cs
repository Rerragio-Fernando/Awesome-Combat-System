using UnityEngine;

public class WeaponHolster : CombatAnimation
{
    private GameObject[] weaponInventory;
    private int activeWeaponIndex = 0;

    private PlayerMovement movement;

    protected override void Start() {
        base.Start();

        movement = GetComponentInParent<PlayerMovement>();

        weaponInventory = new GameObject[this.transform.childCount];

        for (int i = 0; i < weaponInventory.Length; i++)
        {
            weaponInventory[i] = this.transform.GetChild(i).gameObject;
            weaponInventory[i].SetActive(false);
        }

        weaponInventory[activeWeaponIndex].SetActive(true);
    }

    protected void CycleWeapon()
    {
        int prevIndex = activeWeaponIndex;
        activeWeaponIndex = (activeWeaponIndex + 1) % weaponInventory.Length;

        string unEquipAnimationState = weaponInventory[prevIndex].GetComponent<Weapon>().PlayerWeaponData.weaponUnEquipAnimationName;

        //Un Equip
        if(unEquipAnimationState != "")
            AnimationServices.PlayAnimation(anim, unEquipAnimationState, crossFadeHolder);
        
        weaponInventory[activeWeaponIndex].SetActive(true);
        weaponInventory[prevIndex].SetActive(false);

        if(movement != null)
        {
            Debug.Log($"{weaponInventory[activeWeaponIndex].GetComponent<Weapon>().PlayerWeaponData.movementData.walkSpeed}");
            movement.ChangePlayerMovementEvent?.Invoke(weaponInventory[activeWeaponIndex].GetComponent<Weapon>().PlayerWeaponData.movementData);
        }
    }

    protected void ResetAnimationState()
    {
        string equipAnimationState = weaponInventory[activeWeaponIndex].GetComponent<Weapon>().PlayerWeaponData.weaponEquipAnimationName;
        
        //Equip
        if(equipAnimationState != "")
            AnimationServices.PlayAnimation(anim, equipAnimationState, crossFadeHolder);
    }
}