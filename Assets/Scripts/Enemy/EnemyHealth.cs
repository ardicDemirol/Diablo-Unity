using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [HideInInspector] public float currentHealth;
    Animator anim;
    public float maxHealth = 100f;

    [SerializeField] private Image enemyHealthBar;

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
        currentHealth -= amount;
        enemyHealthBar.fillAmount = currentHealth / maxHealth;

        if(currentHealth > 0)
        {
            anim.SetTrigger("Hit");

        }
        if(currentHealth < 0)
        {
            Canvas canvas =  enemyHealthBar.gameObject.GetComponentInParent<Canvas>();
            canvas.gameObject.SetActive(false);
        }
    }
}
