using UnityEngine;
using UnityEngine.UI;

public class CharacterHealth : MonoBehaviour
{
    [SerializeField] Slider _healthBar;
    [SerializeField] private int _maxHealth;
    private int _health;

    private void Start() {
        _health = _maxHealth;
        if (_healthBar)
        {
            _healthBar.maxValue = _maxHealth;
        }
    }

    public void TakeDamage(int val){
        _health -= val;
    }

    private void Update() {
        if(_healthBar)
        {
            _healthBar.value = _health;
        }   
    }
}