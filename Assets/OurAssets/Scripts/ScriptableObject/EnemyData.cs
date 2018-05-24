using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData")]
public class EnemyData : ScriptableObject
{
    [SerializeField] float movementSpeed = 0;
    [SerializeField] int damageToPlayer = 0;
    [SerializeField] float attackCD = 0;
    [SerializeField] float attackRange = 0;
    [SerializeField] bool ranged = false;

    //gets 
    #region gets & sets

    public float MovementSpeed
    {
        get
        {
            return movementSpeed;
        }
    }
    public int DamageToPlayer
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
            return attackRange;
        }
    }
    public bool Ranged
    {
        get
        {
            return ranged;
        }

    }


    #endregion





}
