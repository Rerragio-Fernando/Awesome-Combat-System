using UnityEngine;

public class CharacterHitbox : MonoBehaviour
{
    [SerializeField] HitBox[] hitboxList;
}

public struct HitBox
{
    public Collider collider;
    public float hitMultiplier;
}