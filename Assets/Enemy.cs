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

    private void Start()
    {
        granade = FindObjectOfType<Granade>();
    }

    public void TakeDamage()
    {
        if (isAlive)
        {
            float distanceToGranade = Vector3.Distance(transform.position, granadePosition.position);
            if (distanceToGranade < granade.explosionRadius)
            {
                health -= 10;

            }
        }
    }



}
