using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [HideInInspector] public float currentHealth;
    public float maxHealth = 100f;

    private Animator anim;
    private Image healthImage;

    private void Awake()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
        healthImage = GameObject.Find("HealthOrb").GetComponent<Image>();
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        UpdateHealth();
        if (currentHealth <= 0f)
        {
            anim.SetBool("Death", true);
            
        }
    }

    public void HealPlayer(float amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth) { currentHealth=maxHealth; }
        UpdateHealth();
    }

    public void UpdateHealth()
    {
        healthImage.fillAmount = currentHealth / maxHealth;
    }
}
