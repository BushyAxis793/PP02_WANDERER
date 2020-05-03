using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] float shootDistance = 100f;
    [SerializeField] GameObject impactEffect;
    [SerializeField] float damage = 20f;



    bool isShootEnabled = true;

    private void OnEnable()
    {
        isShootEnabled = true;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && isShootEnabled)
        {
            //StartCoroutine(WeaponShoot());
            WeaponRaycast();

        }
    }

    IEnumerator WeaponShoot()
    {
        isShootEnabled = false;
        yield return new WaitForSeconds(.5f);
        isShootEnabled = true;
        print("moge strzelac");

    }

    private void WeaponRaycast()
    {
        muzzleFlash.Play();

        RaycastHit hit;
        Camera playerCamera = GetComponentInParent<Camera>();
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, shootDistance))
        {
            Debug.Log(hit.transform.name);
            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }
            GameObject impactVFX = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactVFX, 2f);
            //todo poprawic particle effects


        }
    }


}
