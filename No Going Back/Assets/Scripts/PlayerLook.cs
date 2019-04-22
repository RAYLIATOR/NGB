using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private string mouseXInputName, mouseYInputName;
    [SerializeField] private float mouseSensitivity;

    [SerializeField] private Transform playerBody;

    public static bool freezeLook;

    float mouseX;
    float mouseY;

    private void Awake()
    {
        LockCursor();
        transform.localRotation = Quaternion.Euler(50f, 0, 0);
        playerBody.transform.rotation = Quaternion.Euler(0, -115.29f, 0);
    }


    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        CameraRotation();
    }

    private void CameraRotation()
    {
        if (!freezeLook)
        {
            if (Input.GetAxis(mouseXInputName) != 0)
            {
                //mouseX += Input.GetAxis(mouseXInputName) * mouseSensitivity;
                mouseX = Input.GetAxis(mouseXInputName);

                //transform.Rotate(Vector3.left * mouseY);
                playerBody.transform.localEulerAngles += new Vector3(0, mouseX, 0);
                //playerBody.transform.localRotation += Quaternion.Euler(0, mouseX, 0);
            }
            if (Input.GetAxis(mouseYInputName) != 0)
            {
                //mouseY += Input.GetAxis(mouseYInputName) * mouseSensitivity;
                mouseY = Input.GetAxis(mouseYInputName);

                mouseY = Mathf.Clamp(mouseY, -50f, 50f);

                //transform.Rotate(Vector3.left * mouseY);
                //transform.localRotation = Quaternion.Euler(-mouseY, 0, 0);
                transform.localEulerAngles += new Vector3(-mouseY, 0, 0);
            }
            print("Mouse X: " + mouseX + " Mouse Y: " + mouseY);
        }
    }

    private void ClampXAxisRotationToValue(float value)
    {
        Vector3 eulerRotation = transform.eulerAngles;
        eulerRotation.x = value;
        transform.eulerAngles = eulerRotation;
    }
}