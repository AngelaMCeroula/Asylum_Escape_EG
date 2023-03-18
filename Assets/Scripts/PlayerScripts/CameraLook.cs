using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour
{
    public float lookSensitivity = 100f;
    public Transform playerBody;
    public float xRotation = 0f;
    public float cameraX;
    public float cameraY;
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        
    }

    // Update is called once per frame
    void Update()
    {
        //cameraX = cameraX * lookSensitivity * Time.deltaTime;
        //cameraY = cameraY * lookSensitivity * Time.deltaTime;

        xRotation -= cameraY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        
        transform.localRotation = Quaternion.Euler(xRotation, 0f,0f);
        playerBody.Rotate(Vector3.up * cameraX);
    }
}
