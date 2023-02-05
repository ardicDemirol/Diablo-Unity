using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class EnemyHealth : MonoBehaviour
{
    [HideInInspector] public float currentHealth;
    Animator anim;
    public float maxHealth = 100f;

    [SerializeField] private Image enemyHealthBar;
    private SphereCollider targetCollider;

    public int ExpAmount = 10;
    public static event Action<int> onDeath;

    private void Awake()
    {
        currentHealth = maxHealth;
        targetCollider = GetComponentInChildren<SphereCollider>();
        anim = GetComponent<Animator>();
        if (this.gameObject.tag == "Boss") { maxHealth = UnityEngine.Random.Range(150, 200); };
        if (this.gameObject.tag == "Enemy") { maxHealth = UnityEngine.Random.Range(60, 100); }

    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        enemyHealthBar.fillAmount = currentHealth / maxHealth;

        if(currentHealth > 0)
        {
            if(this.gameObject.tag == "Boss") { AudioManager.Instance.PlaySFX(3); };
            if(this.gameObject.tag == "Enemy") { AudioManager.Instance.PlaySFX(6); }
            anim.SetTrigger("Hit");

        }
        if(currentHealth <= 0)
        {
            Canvas canvas =  enemyHealthBar.gameObject.GetComponentInParent<Canvas>();
            onDeath(ExpAmount);
            if (targetCollider.gameObject.activeInHierarchy)
            {
                targetCollider.gameObject.SetActive(false);
            }
            if (canvas.gameObject.activeInHierarchy)
            {
                canvas.gameObject.SetActive(false);
            }
            
        }
    }
}
