using UnityEngine;

public class WeaponHolster : CombatAnimation
{
    private GameObject[] weaponInventory;
    private int activeWeaponIndex = 0;

    private void Start() {
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
        
        weaponInventory[activeWeaponIndex].SetActive(true);
        weaponInventory[prevIndex].SetActive(false);
    }

    protected void ResetAnimationState()
    {
        
    }
}