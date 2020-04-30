using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Granade : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        if (tag=="Enemy")
        {
            ///destroy,getdamage,vfx,sfx
        }
    }

}
