using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    private float rotationX, rotationY;

    private float minX = -360f;
    private float maxX = 360f;

    private float minY = -60f;
    private float maxY = 80f;

    private Quaternion baseRotation;

    

    void Start()
    {
        baseRotation = transform.rotation;
    }

    void LateUpdate()
    {
        RotationHandle();
    }

    float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360f)
        {
            angle += 360f;
        }
        if (angle > 360f)
        {
            angle -= 360f;
        }
        return Mathf.Clamp(angle, min, max);
    }

    void RotationHandle()
    {
        Quaternion xRotation = CalcAxisX();
        Quaternion yRotation = CalcAxisY();
        transform.rotation = baseRotation * xRotation * yRotation;

    }

    private Quaternion CalcAxisY()
    {
        float sensivityY = 1.5f;
        rotationY += Input.GetAxis("Mouse Y") * sensivityY;
        rotationY = ClampAngle(rotationY, minY, maxY);
        Quaternion yRotation = Quaternion.AngleAxis(rotationY, Vector3.left);
        return yRotation;
    }

    public Quaternion CalcAxisX()
    {
        float sensivityX = 1.5f;
        rotationX += Input.GetAxis("Mouse X") * sensivityX;
        rotationX = ClampAngle(rotationX, minX, maxX);
        Quaternion xRotation = Quaternion.AngleAxis(rotationX, Vector3.up);
        return xRotation;
    }
}
