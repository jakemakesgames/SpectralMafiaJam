using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData")]
public class EnemyData : ScriptableObject
{
    [SerializeField] float movementspeed;
    [SerializeField] float damageToPlayer;
    [SerializeField] float attackCD;
    [SerializeField] float attackRange;


    //gets 
    #region gets & sets
    public float MovementSpeed
    {
        get
        {
            return movementspeed;
        }
    }
    public float DamageToPlayer
    {
        get
        {
            return damageToPlayer;
        }
    }
    public float AttackCD
    {
        get
        {
            return attackCD;
        }
    }
    public float AttackRange
    {
        get
        {
            return AttackRange;
        }
    }
    #endregion





}
