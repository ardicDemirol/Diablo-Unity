using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillsDamage : MonoBehaviour
{
    public LayerMask enemyLayer;
    public float radius = 0.5f;
    public float damageCount = 10f;

    private EnemyHealth enemyHealth;
    protected bool collided;

    void Start()
    {
        
    }

    internal virtual void Update()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, radius,enemyLayer);

        foreach (Collider hit in hits) 
        {
            enemyHealth = hit.gameObject.GetComponent<EnemyHealth>();
            collided = true;

        }
        if (collided)
        {
            enemyHealth.TakeDamage(damageCount);
            enabled = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
