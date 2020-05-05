using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int health = 100;
    [SerializeField] int damageAmount = 5;
    [SerializeField] Transform granadePosition;

    bool isAlive = true;

    Granade granade;

    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        granade = FindObjectOfType<Granade>();
    }

    public void TakeDamageFromGranade()
    {
        if (isAlive)
        {
            float distanceToGranade = Vector3.Distance(transform.position, granadePosition.position);
            if (distanceToGranade < granade.explosionRadius)
            {
                anim.SetTrigger("GetHit");
                health -= 10;
                if (health<=0)
                {
                    anim.SetTrigger("Dead");
                    Destroy(gameObject, 10f);
                }

            }
        }
    }
    public void TakeDamageFromWeapon(int damage)
    {
        anim.SetTrigger("GetHit");
        health -= damage;
        if (health <= 0)
        {
            anim.SetTrigger("Dead");
            Destroy(gameObject,10f);
        }
    }

    public void GiveDamage(int damage)
    {
        anim.SetBool("Attack",true);

    }


}
