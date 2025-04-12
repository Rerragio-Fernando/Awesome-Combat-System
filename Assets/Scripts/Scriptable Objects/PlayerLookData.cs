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

    [Tooltip("Lock Out Threshold")] 
    public float lockOutThreshold;
}
/*
2 Options:
>Lock on target when player crosshair hovers over enemy for n seconds
>Lock on target when player attacks an enemy

Lock onto the enemy where the players crosshair lands on
*/