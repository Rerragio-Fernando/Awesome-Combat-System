using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField, Tooltip("Weapon Graphics")]
    private WeaponData weaponData;

    public WeaponData PlayerWeaponData => weaponData;

    protected virtual void OnEnable() {
        if(weaponData.weaponGraphics != null)
            weaponData.weaponGraphics.SetActive(true);            
    }

    protected virtual void OnDisable() {
        if(weaponData.weaponGraphics != null)
            weaponData.weaponGraphics.SetActive(false);
    }
}

[System.Serializable]
public struct WeaponData
{
    [Tooltip("Weapon Name")]
    public string weaponName;

    [Tooltip("Weapon Movement Speed")]
    public PlayerMovementData movementData;

    [Tooltip("Weapon Graphics")]
    public GameObject weaponGraphics;

    [Tooltip("Weapon Movement Blend Tree Name in Animator")]
    public string weaponMovementBlendTreeName;

    [Tooltip("Weapon Equip Animation State Name in Animator")]
    public string weaponEquipAnimationName;

    [Tooltip("Weapon Un-Equip Animation State Name in Animator")]
    public string weaponUnEquipAnimationName;
}