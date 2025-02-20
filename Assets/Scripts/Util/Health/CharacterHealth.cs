using UnityEngine;
using UnityEngine.UI;

public class CharacterHealth : MonoBehaviour
{
    [SerializeField] Slider healthBar;
    [SerializeField] private int maxHealth;
    private int health;

    private void Start() {
        health = maxHealth;
        if (healthBar)
        {
            healthBar.maxValue = maxHealth;
        }
    }

    public void TakeDamage(int val){
        health -= val;
    }

    private void Update() {
        if(healthBar)
        {
            healthBar.value = health;
        }   
    }
}