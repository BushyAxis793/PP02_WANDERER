using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class Weapon : MonoBehaviour
{
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] float shootDistance = 100f;
    [SerializeField] GameObject impactEffect;
    [SerializeField] int damage = 10;
    [SerializeField] AmmoType ammoType;
    [SerializeField] AmmoManager ammoSlot;
    [SerializeField] TextMeshProUGUI ammoText;
    [SerializeField] AudioClip audioClip;
    [SerializeField] float shotsDelay = .2f;
    [SerializeField] Canvas pauseMenu, optionsMenu;



    bool isShootEnabled = true;
    private int burstSize;

    private void OnEnable()
    {
        isShootEnabled = true;
    }

    private void Update()
    {

        if (!pauseMenu.isActiveAndEnabled && !optionsMenu.isActiveAndEnabled)
        {
            HandleShot();
        }
        ammoText.text = ammoSlot.GetAmmo(ammoType).ToString();
    }

    private void HandleShot()
    {
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
            GetComponent<AudioSource>().PlayOneShot(audioClip);
            WeaponRaycast();
            ammoSlot.ReduceAmmo(ammoType);
            yield return new WaitForSeconds(shotsDelay);
            muzzleFlash.Stop();
        }

    }


    private void WeaponRaycast()
    {
        RaycastHit hit;
        Camera playerCamera = GetComponentInParent<Camera>();
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, shootDistance))
        {
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
