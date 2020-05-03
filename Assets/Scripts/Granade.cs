using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Granade : MonoBehaviour
{
    [SerializeField] GameObject explosion;
    public float explosionRadius = 5f;


    Enemy enemy;

    private void Start()
    {
        enemy = FindObjectOfType<Enemy>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ThrowGranade();
        }
    }

    public void ThrowGranade()
    {
        StartCoroutine(ExplosionTimer());
        Destroy(gameObject, 2.1f);
    }

    private void HurtEnemy()
    {
        if (enemy.CompareTag("Enemy"))
        {
            enemy.TakeDamage();
        }
    }

    IEnumerator ExplosionTimer()
    {
        yield return new WaitForSeconds(2f);
        Instantiate(explosion, transform.position, Quaternion.identity);
        HurtEnemy();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
