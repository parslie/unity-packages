using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Controller3D))]
public class Player : MonoBehaviour
{
    [SerializeField] float jogSpeed;
    [SerializeField] float gravity;
    Vector3 velocity;

    Controller3D controller;

    void Awake()
    {
        controller = GetComponent<Controller3D>();
    }

    void Update()
    {
        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        velocity.x = jogSpeed * moveInput.x;
        velocity.z = jogSpeed * moveInput.z;

        if (controller.IsGrounded || controller.IsCeiled)
            velocity.y = 0;
        velocity.y -= gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}
