using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    public float currenHealth;
    public float maxHealth = 100;

    public void Heal(float amount)
    {
        currenHealth += amount;

        if (currenHealth > maxHealth)
        {
            maxHealth = currenHealth;
        }

    }
}
