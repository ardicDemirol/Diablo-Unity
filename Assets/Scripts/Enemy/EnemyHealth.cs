using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [HideInInspector] private float currentHealth;

    public float maxHealth = 100f;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        Debug.Log(currentHealth);
    }

    public void TakeDamage(float amount)
    {
        currentHealth-= amount;
    }
}
