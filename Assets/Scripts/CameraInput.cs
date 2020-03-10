using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraInput : MonoBehaviour
{
    public float speed = 1f;
    float mouseX;

    private void Update()
    {
        mouseX = Input.GetAxisRaw("Mouse X");
    }

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate()
    {
        transform.Rotate(transform.up, mouseX);
    }
}
