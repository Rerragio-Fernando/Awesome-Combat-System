using UnityEngine;

[CreateAssetMenu(fileName = "PlayerLookData", menuName = "Scriptable Objects/Data/PlayerLookData")]
public class PlayerLookData : ScriptableObject
{
    [Tooltip("Lower-bound of vertical look")]
    public float _minRotation;

    [Tooltip("Upper-bound of vertical look")]
    public float _maxRotation;

    [Tooltip("Player Look Sensitivity")]
    public float _aimSensitivity;
}