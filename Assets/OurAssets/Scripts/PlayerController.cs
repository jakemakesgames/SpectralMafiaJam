﻿//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public XboxController controllerNumber = 0;
    //[SerializeField] XboxButton shootButton = XboxButton.RightBumper;

    [SerializeField] float moveSpeed = 1;

    CharacterController cc;

    Vector3 bodyRotation;
    Vector3 turretRotation;

    private void Awake()
    {
        cc = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Movement();

    }

    void Movement()
    {
        Vector3 leftStickDirection = Vector3.zero;
        //// PC Movement
        //if (Input.GetKey(KeyCode.W))
        //    movement.z++;
        //if (Input.GetKey(KeyCode.S))
        //    movement.z--;
        //if (Input.GetKey(KeyCode.D))
        //    movement.x++;
        //if (Input.GetKey(KeyCode.A))
        //    movement.x--;

        // Left stick movement
        leftStickDirection.x = XCI.GetAxis(XboxAxis.LeftStickX, controllerNumber);
        leftStickDirection.z = XCI.GetAxis(XboxAxis.LeftStickY, controllerNumber);
        // Make sure the movement is normalized
        leftStickDirection = leftStickDirection.normalized;
        // Move the player
        cc.Move(leftStickDirection * moveSpeed * Time.deltaTime);

        // Left stick rotation
        if (leftStickDirection.x != 0 || leftStickDirection.y != 0)
        {
            Vector3 targetRotation = leftStickDirection;
            bodyRotation = Vector3.Lerp(bodyRotation, targetRotation, 10 * Time.deltaTime);
            transform.LookAt(transform.position + bodyRotation);
        }

        // Right stick
        Vector3 rightStickDirection = Vector3.zero;
        rightStickDirection.x = XCI.GetAxis(XboxAxis.RightStickX, controllerNumber);
        rightStickDirection.z = XCI.GetAxis(XboxAxis.RightStickY, controllerNumber);

        if (rightStickDirection.x != 0 || rightStickDirection.y != 0)
        {
            Vector3 targetRotation = rightStickDirection;
            turretRotation = Vector3.Lerp(turretRotation, targetRotation, 10 * Time.deltaTime);
        }
        transform.LookAt(transform.position + turretRotation);
    }
}
