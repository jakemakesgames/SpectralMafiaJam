//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public XboxController controllerNumber = 0;
    [SerializeField] XboxButton shootButton = XboxButton.RightBumper;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform bulletSpawnPos;

    [SerializeField] float moveSpeed = 1;
    [SerializeField] float bulletSpeed = 1;

    [SerializeField] int health = 100;

    bool isAlive = true;

    CharacterController cc;

    Vector3 bodyRotation;

    public bool IsAlive { get { return isAlive; } }

    private void Awake()
    {
        cc = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Movement();

        if (/*throwTimer <= 0 &&*/ XCI.GetButtonDown(shootButton, controllerNumber))
        {
            Shoot();
        }

    }

    void Shoot()
    {
        // Create a bullet
        GameObject newBullet = Instantiate(bulletPrefab, bulletSpawnPos.position, transform.rotation);
        // Set the velocity
        newBullet.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed, ForceMode.VelocityChange);
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
            bodyRotation = Vector3.Lerp(bodyRotation, targetRotation, 10 * Time.deltaTime);
        }
        transform.LookAt(transform.position + bodyRotation);
    }

    public void TakeDamage(int dmg)
    {
        health -= dmg;

        if (health < 0)
            isAlive = false;
    }

}