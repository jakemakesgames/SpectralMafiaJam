using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{

    [SerializeField] EnemyData enemyScritableObject;

    [SerializeField] GameObject playerGO;

    [SerializeField] GameObject enemyJarPrefab;

    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float bulletSpeed;

    [SerializeField] float rotationSpeed;

    private PlayerController player;
    private float attackTimer;

    private bool isAlive = true;

    void Awake()
    {
        //playerGO = GameObject.FindGameObjectWithTag("Player"); // This function call is banned
        attackTimer = enemyScritableObject.AttackCD;
        player = playerGO.GetComponent<PlayerController>();
    }

    void Update()
    {
        attackTimer += Time.deltaTime;
        Vector3 vecBetween = playerGO.transform.position - transform.position;

        if (vecBetween.magnitude > enemyScritableObject.AttackRange)
        {
            transform.position += vecBetween.normalized * enemyScritableObject.MovementSpeed * Time.deltaTime;
        }
        else
        {
            if (attackTimer > enemyScritableObject.AttackCD)
            {
                attackTimer = 0;
                if (enemyScritableObject.Ranged == false)
                {
                    player.TakeDamage(enemyScritableObject.DamageToPlayer);
                }
                else
                {   
                    //shoot
                    
                    GameObject bullet = Instantiate(bulletPrefab, transform.position + (vecBetween.normalized * 2), transform.rotation);
                    bullet.GetComponent<Rigidbody>().AddForce(vecBetween.normalized * bulletSpeed, ForceMode.VelocityChange);
                    
                }
            }
        }

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(vecBetween.normalized), rotationSpeed * Time.deltaTime);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            isAlive = false;
            // Turn off this enemy
            gameObject.SetActive(false);
            // Create a enemy jar with this enemy contained in it
            GameObject jar = Instantiate(enemyJarPrefab, transform.position, enemyJarPrefab.transform.rotation);
            jar.GetComponent<EnemyJar>().Startup(gameObject);
        }
    }
}
