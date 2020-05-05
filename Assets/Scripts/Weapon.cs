using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] float shootDistance = 100f;
    [SerializeField] GameObject impactEffect;
    [SerializeField] int damage = 10;



    bool isShootEnabled = true;
    [SerializeField] float timeBetweenShots = .2f;

    private void Start()
    {
        
    }
    private void OnEnable()
    {
        isShootEnabled = true;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            StartCoroutine(WeaponShoot());

        }
    }

    IEnumerator WeaponShoot()
    {
        muzzleFlash.Play();
        WeaponRaycast();
        yield return new WaitForSeconds(timeBetweenShots);
        muzzleFlash.Stop();
        print("moge strzelac");

    }

    private void WeaponRaycast()
    {

        RaycastHit hit;
        Camera playerCamera = GetComponentInParent<Camera>();
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, shootDistance))
        {
            Debug.Log(hit.transform.name);
            Enemy target = hit.transform.GetComponent<Enemy>();
            if (target != null)
            {
                target.TakeDamageFromWeapon(damage);
            }
            GameObject impactVFX = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactVFX, 2f);


        }
    }


}
