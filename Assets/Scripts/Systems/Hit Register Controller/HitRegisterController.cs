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