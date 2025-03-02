using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField, Tooltip("Weapon Graphics")]
    private WeaponData weaponData;

    public WeaponData PlayerWeaponData => weaponData;

    private void OnEnable() {
        if(weaponData.weaponGraphics != null)
            weaponData.weaponGraphics.SetActive(true);
    }

    private void OnDisable() {
        if(weaponData.weaponGraphics != null)
            weaponData.weaponGraphics.SetActive(false);
    }
}

[System.Serializable]
public struct WeaponData
{
    [Tooltip("Weapon Name")]
    public string weaponName;

    [Tooltip("Weapon Graphics")]
    public GameObject weaponGraphics;

    [Tooltip("Weapon Movement Blend Tree Name in Animator")]
    public string weaponMovementBlendTreeName;

    [Tooltip("Weapon Equip Animation State Name in Animator")]
    public string weaponEquipAnimationName;

    [Tooltip("Weapon Un-Equip Animation State Name in Animator")]
    public string weaponUnEquipAnimationName;
}