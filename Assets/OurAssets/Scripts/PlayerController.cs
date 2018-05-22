//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public XboxController controllerNumber = 0;
    [SerializeField] XboxButton shootButton = XboxButton.RightBumper;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform bulletSpawnTransform;

    [SerializeField] ParticleSystem shootParticle;

    [SerializeField] float moveSpeed = 1;
    [SerializeField] float rotateSpeed = 5;
    [SerializeField] float fallSpeed = 1;
    [SerializeField] float bulletSpeed = 1;
    [SerializeField] float shootCooldown = 0.5f;

    [SerializeField] int maxJars = 3;

    [SerializeField] int health = 100;

    bool isAlive = true;

    CharacterController cc;
    LineRenderer lineRenderer;
    Animator animator;

    Vector3 bodyRotation;

    float shootTimer;

    int jarCount;

    bool canShoot;

    public bool IsAlive { get { return isAlive; } }

    private void Awake()
    {
        cc = GetComponent<CharacterController>();

        animator = GetComponent<Animator>();

        jarCount = maxJars;

        lineRenderer = bulletSpawnTransform.GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if (isAlive == false)
            return;

        Movement();

        UpdateLineRenderer();

        canShoot = Physics.CheckSphere(bulletSpawnTransform.position - bulletSpawnTransform.forward * 0.5f, 0.5f, LayerMask.GetMask("Collider")) == false;

        if (canShoot && shootTimer <= 0 && XCI.GetButtonDown(shootButton, controllerNumber))
            Shoot();
        else
            shootTimer -= Time.deltaTime;
    }

    void UpdateLineRenderer()
    {
        lineRenderer.SetPosition(0, bulletSpawnTransform.position);
        // Ray cast
        Ray ray = new Ray(bulletSpawnTransform.position - bulletSpawnTransform.forward * 1f, bulletSpawnTransform.forward);
        RaycastHit hitInfo = new RaycastHit();
        Debug.Log(LayerMask.GetMask("Collider"));
        if (canShoot && Physics.Raycast(ray, out hitInfo, 1000, LayerMask.GetMask("Collider", "Enemy")))
        {
            lineRenderer.enabled = true;
            lineRenderer.SetPosition(1, hitInfo.point);
        }
        else
        {
            lineRenderer.enabled = false;
            // Set a default pos if the ray didn't hit
            //lineRenderer.SetPosition(1, bulletSpawnPos.position + bulletSpawnPos.forward * 1000);
        }
    }


    void Shoot()
    {
        if (jarCount > 0)
        {
            // Play the particle
            shootParticle.Play();
            // Tell the animator to play the shoot animation
            animator.SetTrigger("Shoot");

            jarCount--;
            shootTimer = shootCooldown;
            // Create a bullet
            GameObject newBullet = Instantiate(bulletPrefab, bulletSpawnTransform.position, transform.rotation);
            // Set the velocity
            newBullet.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed, ForceMode.VelocityChange);
        }
    }


    void Movement()
    {
        // Make the player fall
        cc.Move(new Vector3(0, -fallSpeed * Time.deltaTime, 0));

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

        // Right stick
        Vector3 rightStickDirection = Vector3.zero;
        rightStickDirection.x = XCI.GetAxis(XboxAxis.RightStickX, controllerNumber);
        rightStickDirection.z = XCI.GetAxis(XboxAxis.RightStickY, controllerNumber);

        Vector3 targetRotation = leftStickDirection;
        // Prioritize right stick for rotation
        if (rightStickDirection.x != 0 || rightStickDirection.z != 0)
        {
            targetRotation = rightStickDirection;
        }
        bodyRotation = Vector3.Lerp(bodyRotation, targetRotation, rotateSpeed * Time.deltaTime);
        transform.LookAt(transform.position + bodyRotation);
    }

    public void TakeDamage(int dmg)
    {
        health -= dmg;

        if (health < 0)
        {
            // Do die animation
            animator.SetTrigger("Die");
            isAlive = false;
        }
    }

    public bool PickUpJar(bool empty)
    {
        if (jarCount < maxJars)
        {
            jarCount++;
            if (empty == false)
            {
                // Kill count ++
            }
            return true;
        }
        else
        {
            return false;
        }
    }

}