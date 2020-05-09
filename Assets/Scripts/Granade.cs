using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Granade : MonoBehaviour
{
    [SerializeField] GameObject explosion;
    public float explosionRadius = 5f;

    Rigidbody rigGranade;
    Enemy enemy;

    private void Start()
    {
        enemy = FindObjectOfType<Enemy>();
        rigGranade = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            ThrowGranade();
        }
    }

    public void ThrowGranade()
    {
        rigGranade.AddExplosionForce(700f, transform.position, 10f);
        StartCoroutine(ExplosionTimer());
        Destroy(gameObject, 2.1f);
    }

    private void HurtEnemy()
    {
        if (enemy.CompareTag("Enemy"))
        {
            enemy.TakeDamageFromGranade();
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
