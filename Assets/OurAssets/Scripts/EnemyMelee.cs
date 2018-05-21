using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : MonoBehaviour
{

    [SerializeField] EnemyData data;

    private GameObject playerGO;
    private PlayerController player;
    private float attackTimer;



    void Start()
    {
        playerGO = GameObject.FindGameObjectWithTag("Player");
        attackTimer = data.AttackCD;
        player = playerGO.GetComponent<PlayerController>();
    }


    void Update()
    {
        attackTimer += Time.deltaTime;
        Vector3 vecBetween = transform.position - playerGO.transform.position;

        if (vecBetween.magnitude > data.AttackRange)
        {
            transform.position += vecBetween.normalized * data.MovementSpeed * Time.deltaTime;
        }
        else
        {
            if(attackTimer > data.AttackCD)
            {
                attackTimer = 0;
                player.TakeDamage(data.DamageToPlayer);
            }
        }

    }
}
