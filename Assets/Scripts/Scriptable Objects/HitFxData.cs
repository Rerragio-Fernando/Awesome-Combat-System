using UnityEngine;

[CreateAssetMenu(fileName = "HitFxData", menuName = "Scriptable Objects/Player Skills Data/Hit FX Data")]
public class HitFxData : ScriptableObject
{
    [Tooltip("Miss FX")]
    public GameObject missFx;

    [Tooltip("Light Hit FX")]
    public GameObject lightHitFx;

    [Tooltip("Strong Hit FX")]
    public GameObject strongHitFx;

    [Tooltip("Critical Hit FX")]
    public GameObject criticalHitFx;
}