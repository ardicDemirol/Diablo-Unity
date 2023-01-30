using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : PlayerSkillsDamage
{
    public GameObject Explosion;
    public float speed = 10f;
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        transform.rotation = Quaternion.LookRotation(player.transform.forward);
    }

    internal override void Update()
    {
        base.Update();
        transform.Translate(Vector3.forward * (speed * Time.deltaTime));
        if(colided)
        {
            Instantiate(Explosion,transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
