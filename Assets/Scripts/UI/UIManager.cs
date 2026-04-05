using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public event Action<float> OnHealthChanged;

    public Slider healthBar;
    //public Slider staminaBar;
    public TextMeshPro ammoCounter;

    [SerializeField] private int maxHealth;
    private void Start()
    {
        SetMaxHealth(maxHealth);
        healthBar.interactable = false; // make sure the slider is not interactable by the player

    }
    public void SetMaxHealth(int maxHealth)
    {
        healthBar.maxValue = maxHealth;
        healthBar.value = maxHealth;
    }
    public void SetHealth(int health)
    {
        healthBar.value = health;
    }
}

