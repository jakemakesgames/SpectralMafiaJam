using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    [SerializeField] EnemyData enemyScritableObject;

    [SerializeField] GameObject enemyBulletPrefab;
    [SerializeField] float bulletSpeed;
    [SerializeField] Transform bulletSpawnTransform;

    [SerializeField] float rotationSpeed;

    [SerializeField] float agroRange = 10;

    private GameObject player1GO;
    private GameObject player2GO;
    private PlayerController player1;
    private PlayerController player2;
    private float attackTimer;


    void Start()
    {
        player1GO = GameManager.Instance.Player1GO;
        player2GO = GameManager.Instance.Player2GO;
        attackTimer = enemyScritableObject.AttackCD;
        player1 = player1GO.GetComponent<PlayerController>();
        if(player2GO != null)
          player2 = player2GO.GetComponent<PlayerController>();
    }


    void Update()
    {
        attackTimer += Time.deltaTime;
        Vector3 vecBetween1 = player1GO.transform.position - transform.position;
        Vector3 vecBetween;
        if (player2GO != null)
        {
            Vector3 vecBetween2 = player2GO.transform.position - transform.position;
            vecBetween = vecBetween1.magnitude < vecBetween2.magnitude ? vecBetween1 : vecBetween2;
        }
        else
        {
            vecBetween = vecBetween1;
        }

        // MAke sure we're within the agro range
        if (vecBetween.magnitude < agroRange)
        {
            // Check if this enemy is out of the attack range
            if (vecBetween.magnitude > enemyScritableObject.AttackRange)
            {
                // Move to player
                transform.position += vecBetween.normalized * enemyScritableObject.MovementSpeed * Time.deltaTime;
            }
            else // Attack
            {
                // Check for the attack cooldown
                if (attackTimer > enemyScritableObject.AttackCD)
                {
                    attackTimer = 0;
                    // If we are melee
                    if (enemyScritableObject.Ranged == false)
                    {
                        player1.TakeDamage(enemyScritableObject.DamageToPlayer);
                    }
                    else // Shoot
                    {
                        // Create a bullet
                        GameObject newBullet = Instantiate(enemyBulletPrefab, bulletSpawnTransform.position, transform.rotation);
                        newBullet.GetComponent<EnemyBullet>().Damage = enemyScritableObject.DamageToPlayer;
                        newBullet.GetComponent<Rigidbody>().AddForce(vecBetween.normalized * bulletSpeed, ForceMode.VelocityChange);
                    }
                }
            }
        }

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(vecBetween.normalized), rotationSpeed * Time.deltaTime);

    }
}
