using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    [SerializeField] EnemyData enemyScritableObject;   

    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float bulletSpeed;

    [SerializeField] float rotationSpeed;

    private GameObject playerGO;
    private PlayerController player;
    private float attackTimer;


    void Start()
    {
        
        playerGO = GameManager.Instance.PlayerGO;
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
}
