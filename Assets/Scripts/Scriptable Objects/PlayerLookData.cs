using UnityEngine;

[CreateAssetMenu(fileName = "PlayerLookData", menuName = "Scriptable Objects/Data/PlayerLookData")]
public class PlayerLookData : ScriptableObject
{
    [Tooltip("Lower-bound of vertical look")]
    public float minRotation;

    [Tooltip("Upper-bound of vertical look")]
    public float maxRotation;

    [Tooltip("Player Look Sensitivity")]
    public float aimSensitivity;
}