using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField, Tooltip("Weapon Graphics")]
    private WeaponData weaponData;
}

[System.Serializable]
public struct WeaponData
{
    [Tooltip("Weapon Graphics")]
    public string weaponName;

    [Tooltip("Weapon Graphics")]
    public GameObject weaponGraphics;
}