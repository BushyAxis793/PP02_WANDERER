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
    [SerializeField] float timeBetweenShots = .2f;




    bool isShootEnabled = true;

    private void OnEnable()
    {
        isShootEnabled = true;
    }

    private void Update()
    {

        ammoText.text = ammoSlot.GetAmmo(ammoType).ToString();

        ShotHandle();
    }

    IEnumerator WeaponShoot()
    {
        if (ammoSlot.GetAmmo(ammoType) > 0)
        {
            muzzleFlash.Play();
            GetComponent<AudioSource>().PlayOneShot(audioClip);
            WeaponRaycast();
            ammoSlot.ReduceAmmo(ammoType);
            yield return new WaitForSeconds(timeBetweenShots);
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

    private void ShotHandle()
    {
        if (ammoType == AmmoType.pistolAmmo || ammoType == AmmoType.shotgunAmmo)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                StartCoroutine(WeaponShoot());
            }
        }
        if (ammoType == AmmoType.carbineAmmo)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Ogień ciągły!!");
                if (ammoSlot.GetAmmo(ammoType) > 0)
                {
                    muzzleFlash.Play();
                    GetComponent<AudioSource>().Play();
                    WeaponRaycast();
                    ammoSlot.ReduceAmmo(ammoType);

                }

            }
            if (Input.GetMouseButtonUp(0))
            {
                Debug.Log("Koniec ostrzału");
                GetComponent<AudioSource>().Stop();
                muzzleFlash.Stop();//todo zrobic ciagły efekt strzelania i dzwiek
            }
        }

    }

}
