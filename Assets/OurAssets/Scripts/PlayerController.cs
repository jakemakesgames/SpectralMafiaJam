//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public XboxController controllerNumber = 0;
    [SerializeField] XboxButton shootButton = XboxButton.RightBumper;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform bulletSpawnTransform;

    [SerializeField] GameObject backPack;
    [SerializeField] ParticleSystem shootParticle;

    [SerializeField] float moveSpeed = 1;
    [SerializeField] float sprintSpeed = 8;
    [SerializeField] float rotateSpeed = 5;
    [SerializeField] float fallSpeed = 1;
    [SerializeField] float bulletSpeed = 1;
    [SerializeField] float shootCooldown = 0.5f;

    [SerializeField] int maxJars = 3;

    [SerializeField] int health = 100;

    [SerializeField] int pickUpHealth = 10;

    bool isAlive = true;

    CharacterController cc;
    LineRenderer lineRenderer;
    Animator animator;


    Vector3 bodyRotation;

    Slider healthSlider;

    int maxHealth;
    float shootTimer;
    int jarCount;
    bool canShoot;

    public bool IsAlive { get { return isAlive; } set { isAlive = value; } }

    public void ResetValues()
    {
        health = maxHealth;
        isAlive = true;
        shootTimer = 0;
        jarCount = maxJars;

        cc.enabled = true;
        lineRenderer.enabled = true;
    }

    public void PlayDead()
    {
        cc.enabled = false;
        lineRenderer.enabled = false;
    }

    private void Awake()
    {
        cc = GetComponent<CharacterController>();

        animator = GetComponent<Animator>();

        jarCount = maxJars;

        lineRenderer = bulletSpawnTransform.GetComponent<LineRenderer>();

        healthSlider = transform.Find("Player Canvas").gameObject.GetComponentInChildren<Slider>();


        maxHealth = health;
        healthSlider.maxValue = maxHealth;
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

        healthSlider.value = health;
    }

    void UpdateLineRenderer()
    {
        lineRenderer.SetPosition(0, bulletSpawnTransform.position);
        // Ray cast
        Ray ray = new Ray(bulletSpawnTransform.position - bulletSpawnTransform.forward * 1f, bulletSpawnTransform.forward);
        RaycastHit hitInfo = new RaycastHit();
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

    void JarCountUpdate()
    {
        for (int i = backPack.transform.childCount; i > 0; i--)
        {
            backPack.transform.GetChild(i - 1).gameObject.SetActive(jarCount >= i);
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

            JarCountUpdate();
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

        // Right stick
        Vector3 rightStickDirection = Vector3.zero;
        rightStickDirection.x = XCI.GetAxis(XboxAxis.RightStickX, controllerNumber);
        rightStickDirection.z = XCI.GetAxis(XboxAxis.RightStickY, controllerNumber);

        Vector3 targetRotation = leftStickDirection;
        bool sprint = true;
        // Prioritize right stick for rotation
        if (rightStickDirection.x != 0 || rightStickDirection.z != 0)
        {
            targetRotation = rightStickDirection;
            sprint = false;
        }
        bodyRotation = Vector3.Lerp(bodyRotation, targetRotation, rotateSpeed * Time.deltaTime);
        // Rotate the player
        transform.LookAt(transform.position + bodyRotation);
        // Move the player
        cc.Move(leftStickDirection * (sprint ? sprintSpeed : moveSpeed) * Time.deltaTime);
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
        //if (jarCount < maxJars)
        {
            jarCount++;
            // Do some extra stuff if the jar isn't empty
            if (empty == false)
            {
                health += pickUpHealth;
                if (health > maxHealth) health = maxHealth;
                jarCount++;
                if (jarCount > maxJars) jarCount = maxJars;
                // Kill count ++
            }
            JarCountUpdate();
            return true;
        }
        //else
        //{
        //    return false;
        //}
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.tag == "Jar")
        {
            hit.rigidbody.AddForce((hit.rigidbody.transform.position - transform.position).normalized * 1, ForceMode.Impulse);
        }
    }

}