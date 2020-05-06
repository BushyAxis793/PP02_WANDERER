using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] float shootDistance = 100f;
    [SerializeField] GameObject impactEffect;
    [SerializeField] int damage = 10;
    [SerializeField] AmmoType ammoType;
    [SerializeField] AmmoManager ammoSlot;
    [SerializeField] TextMeshProUGUI ammoText;



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
        ammoText.text = ammoSlot.GetAmmo(ammoType).ToString();

        if (Input.GetButtonDown("Fire1"))
        {
            StartCoroutine(WeaponShoot());

        }
    }

    IEnumerator WeaponShoot()
    {
        if (ammoSlot.GetAmmo(ammoType) > 0)
        {

            muzzleFlash.Play();
            WeaponRaycast();
            ammoSlot.ReduceAmmo(ammoType);
            yield return new WaitForSeconds(timeBetweenShots);
            muzzleFlash.Stop();
            print("moge strzelac");
        }

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
