using System;
using UnityEngine;

public class HitRegisterController : MonoBehaviour
{
    public Action<int> RegisterHitEvent;

    public void RegisterHit(int hitPointIndex)
    {
        RegisterHitEvent?.Invoke(hitPointIndex);
    }
}

public enum HitLevel
{
    MISS,
    LIGHT,
    STRONG,
    CRITICAL
}

[System.Serializable]
public class Hit
{
    public HitLevel hitLevel;
    public float hitAmount;

    public Hit(HitLevel hL, float hA)
    {
        hitLevel = hL;
        hitAmount = hA;

        Debug.Log($"{hL}");
    }
}