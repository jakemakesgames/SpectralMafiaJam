using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : MonoBehaviour
{

    [SerializeField] EnemyData data;

    private GameObject player;
    private float attackTimer;



    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        attackTimer = data.AttackCD;
    }


    void Update()
    {
        attackTimer += Time.deltaTime;
        Vector3 vecBetween = transform.position - player.transform.position;

        

        if (vecBetween.magnitude > data.AttackRange)
        {
            transform.position += vecBetween.normalized * data.MovementSpeed * Time.deltaTime;
        }
        else
        {
            if(attackTimer > data.AttackCD)
            {
                attackTimer = 0;
            }
        }

    }
}
