using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [HideInInspector] private float currentHealth;
    Animator anim;
    public float maxHealth = 100f;

    private void Awake()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        Debug.Log(currentHealth);
    }

    public void TakeDamage(float amount)
    {
        anim.SetTrigger("Hit");
        currentHealth-= amount;
    }
}
