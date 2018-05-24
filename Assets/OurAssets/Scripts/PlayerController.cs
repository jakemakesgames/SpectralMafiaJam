//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public XboxController controllerNumber = 0;
    [SerializeField] XboxButton shootButton = XboxButton.RightBumper;
    [SerializeField] XboxButton pauseButton = XboxButton.Start;
    [SerializeField] bool useKeyboardControls = true;
    [SerializeField] GameObject bulletPrefab = null;
    [SerializeField] Transform bulletSpawnTransform = null;

    [SerializeField] GameObject backPack = null;
    [SerializeField] ParticleSystem shootParticle = null;

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

        canShoot = Physics.CheckSphere(bulletSpawnTransform.position - bulletSpawnTransform.forward * 0.5f, 0.5f, LayerMask.GetMask("Collider")) == false;

        Movement();

        UpdateLineRenderer();

        // Use pc controls
        if (useKeyboardControls)
        {
            // Pause button
            if (useKeyboardControls && (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape)))
            {
                GameManager.Instance.Pause();
            }
        }
        else
        {
            // Pause button
            if (XCI.GetButtonDown(pauseButton, controllerNumber))
            {
                GameManager.Instance.Pause();
            }
        }

        if (canShoot && shootTimer <= 0)
        {
            if (useKeyboardControls)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                    Shoot();
            }
            else
            {
                if (XCI.GetButtonDown(shootButton, controllerNumber) || XCI.GetAxis(XboxAxis.RightTrigger, controllerNumber) > 0)
                {
                    Shoot();
                }
            }
        }
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

        Vector3 moveDirection = Vector3.zero;
        Vector3 lookDirection = Vector3.zero;
        bool sprint = true;
        if (useKeyboardControls)
        {
            // PC Movement
            if (Input.GetKey(KeyCode.W))
                moveDirection.z++;
            if (Input.GetKey(KeyCode.S))
                moveDirection.z--;
            if (Input.GetKey(KeyCode.D))
                moveDirection.x++;
            if (Input.GetKey(KeyCode.A))
                moveDirection.x--;

            // Sprint if left shift is down
            sprint = Input.GetKey(KeyCode.LeftShift);


            // Raycast to find the look direction
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Plane plane = new Plane(Vector3.up, transform.position);
            float enter;
            // If the ray hit
            if (plane.Raycast(ray, out enter))
            {
                // Get the point in the ray that hit the plane
                Vector3 hitPos = ray.GetPoint(enter);
                // Set the look direction from the player to that position
                lookDirection = hitPos - transform.position;
                lookDirection.y = 0;
            }

            // Can't shoot when sprinting && look in the direction of movement
            if (sprint == true)
            {
                lookDirection = moveDirection;
                canShoot = false;
            }
        }
        else
        {
            Vector3 leftStickDirection = Vector3.zero;
            // Left stick movement
            leftStickDirection.x = XCI.GetAxis(XboxAxis.LeftStickX, controllerNumber);
            leftStickDirection.z = XCI.GetAxis(XboxAxis.LeftStickY, controllerNumber);
            // Make sure the movement is normalized
            leftStickDirection = leftStickDirection.normalized;
            // Set the move direction to the left stick
            moveDirection = leftStickDirection;
            // Set the look direction to the left stick
            lookDirection = leftStickDirection;

            // Right stick
            Vector3 rightStickDirection = Vector3.zero;
            rightStickDirection.x = XCI.GetAxis(XboxAxis.RightStickX, controllerNumber);
            rightStickDirection.z = XCI.GetAxis(XboxAxis.RightStickY, controllerNumber);
            // Prioritize right stick for rotation
            if (rightStickDirection.x != 0 || rightStickDirection.z != 0)
            {
                lookDirection = rightStickDirection;
                sprint = false;
            }

        }

        // Normalize the vectors
        moveDirection = moveDirection.normalized;
        lookDirection = lookDirection.normalized;

        // Lerp the looking
        bodyRotation = Vector3.Lerp(bodyRotation, lookDirection, rotateSpeed * Time.deltaTime);
        // Rotate the player
        transform.LookAt(transform.position + bodyRotation);
        // Move the player
        cc.Move(moveDirection * (sprint ? sprintSpeed : moveSpeed) * Time.deltaTime);
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
                // Add health
                health += pickUpHealth;
                // Cap Health
                if (health > maxHealth)
                    health = maxHealth;
                // Add a second jar
                jarCount++;
                // Cap the jar count
                if (jarCount > maxJars)
                    jarCount = maxJars;
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