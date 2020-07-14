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

    Coroutine carbineCoroutine;

    Animator shotAnim;

    bool isShootEnabled = true;
    private int burstSize;

    private void Start()
    {
        shotAnim = GetComponentInChildren<Animator>();
    }

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
        if (ammoType == AmmoType.pistolAmmo || ammoType == AmmoType.shotgunAmmo)
        {

            if (Input.GetButtonDown("Fire1"))
            {
                shotAnim.SetTrigger("isShooting");
                StartCoroutine(WeaponShoot());
            }
        }

        if (ammoType == AmmoType.carbineAmmo)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                muzzleFlash.Play();
                GetComponent<Animation>().Play("Carbine Recoil");
                carbineCoroutine = StartCoroutine(CarbineShoot());
            }
            if (Input.GetButtonUp("Fire1"))
            {
                muzzleFlash.Stop();
                GetComponent<Animation>().Stop("Carbine Recoil");
                StopCoroutine(carbineCoroutine);
            }
        }

    }



    IEnumerator WeaponShoot()
    {
        if (ammoSlot.GetAmmo(ammoType) > 0)
        {
            muzzleFlash.Play();
            AudioSource.PlayClipAtPoint(audioClip, Camera.main.transform.position, 1f);
            WeaponRaycast();
            ammoSlot.ReduceAmmo(ammoType);
            yield return new WaitForSeconds(shotsDelay);
            muzzleFlash.Stop();
        }

    }

    IEnumerator CarbineShoot()
    {
        while (ammoSlot.GetAmmo(ammoType) > 0)
        {
            GetComponent<AudioSource>().PlayOneShot(audioClip);
            WeaponRaycast();
            ammoSlot.ReduceAmmo(ammoType);
            yield return new WaitForSeconds(shotsDelay);
        }
        if (ammoSlot.GetAmmo(ammoType) <= 0)
        {
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
