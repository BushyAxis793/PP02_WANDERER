using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AmmoPickup : MonoBehaviour
{
    [SerializeField] int ammoAmount = 5;
    [SerializeField] AmmoType ammoType;
    [SerializeField] GameObject infoText;

    bool canPickup;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) & canPickup)
        {
            PickUp();
            infoText.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            infoText.SetActive(true);
            canPickup = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {

            infoText.SetActive(false);
            canPickup = false;
        }
    }

    private void PickUp()
    {
        FindObjectOfType<AmmoManager>().IncreaseAmmo(ammoType, ammoAmount);
        Destroy(gameObject);
    }
}
