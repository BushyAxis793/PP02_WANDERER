using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public void Movement()
    {
        float inputX = Input.GetAxis("Mouse Y");
        float inputY = Input.GetAxis("Mouse X");
        Vector3 rotateDirection = new Vector3(inputX, inputY, 0);
        //todo zrobic rotacje kamery
        
    }

    private void Update()
    {
        Movement();
    }


}
