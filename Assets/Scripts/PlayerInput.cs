using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerInput : MonoBehaviour
{

    CharacterController controller;

    float hInput, vInput;

    public float speed = .5f;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        hInput = Input.GetAxis("Horizontal");
        vInput = Input.GetAxis("Vertical");

        controller.Move((transform.forward * vInput + transform.right * hInput) * speed);
    }
}
