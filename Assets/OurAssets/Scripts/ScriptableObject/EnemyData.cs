using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData")]
public class EnemyData : ScriptableObject
{
    [SerializeField] float movementSpeed;
    [SerializeField] int damageToPlayer;
    [SerializeField] float attackCD;
    [SerializeField] float attackRange;
    [SerializeField] bool ranged;

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
