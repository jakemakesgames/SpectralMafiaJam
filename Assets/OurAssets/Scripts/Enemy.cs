using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{

    [SerializeField] EnemyData enemyScritableObject;

    private GameObject playerGO;
    private PlayerController player;
    private float attackTimer;
    //private 


    void Start()
    {
        playerGO = GameObject.FindGameObjectWithTag("Player");
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
                }
            }
        }

    }
}
