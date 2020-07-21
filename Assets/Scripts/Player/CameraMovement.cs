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

    private float sensivityX = 1.5f;
    private float sensivityY = 1.5f;

    private float sensivityXSet = 1.5f;
    private float sensivityYSet = 1.5f;

    private float mouseSensivity = 1.5f;

    public enum axesRotation { mouseX, mouseY }
    public axesRotation axes = axesRotation.mouseY;

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
        if (sensivityX != mouseSensivity || sensivityY != mouseSensivity)
        {
            sensivityX = sensivityY = mouseSensivity;
        }

        sensivityXSet = sensivityX;
        sensivityYSet = sensivityY;

        if (axes == axesRotation.mouseX)
        {
            rotationX += Input.GetAxis("Mouse X") * sensivityX;

            rotationX = ClampAngle(rotationX, minX, maxX);
            Quaternion xRotation = Quaternion.AngleAxis(rotationX, Vector3.up);
            transform.localRotation = baseRotation * xRotation;
        }
        if (axes == axesRotation.mouseY)
        {
            rotationY += Input.GetAxis("Mouse Y") * sensivityY;

            rotationY = ClampAngle(rotationY, minY, maxY);
            Quaternion yRotation = Quaternion.AngleAxis(rotationY, Vector3.left);
            transform.localRotation = baseRotation * yRotation;
        }
    }
}
