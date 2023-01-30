using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    [HideInInspector] public float currentHealth;
    public float maxHealth = 100f;
    private Image healthBar;

    private Animator anim;
    private bool isHealthUp;
    public bool IsHealthUp { get { return isHealthUp; } set {isHealthUp = value; } }

    private void Awake()
    {
        currentHealth = maxHealth;
        healthBar = GameObject.Find("HealthOrb").GetComponent<Image>();
        anim = GetComponent<Animator>();
    }
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S)) { TakeDamage(5); }
        if (Input.GetKeyDown(KeyCode.W)) { HealPlayer(5); }
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        UpdateHealth();

        if(currentHealth < 0)
        {
            anim.SetBool("Death", true);
        }
    }

    public void HealPlayer(float amount)
    {
        currentHealth+= amount;
        if(currentHealth > maxHealth) { currentHealth = maxHealth; }
        UpdateHealth();
    }

    public void UpdateHealth() { healthBar.fillAmount = currentHealth / maxHealth; }
}
