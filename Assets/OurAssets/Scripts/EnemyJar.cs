using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyJar : MonoBehaviour
{

    [SerializeField] float trappedTime = 5;

    GameObject containedEnemy = null;

    float timer = 1000;

    public void Startup(GameObject enemy)
    {
        containedEnemy = enemy;
        timer = trappedTime;
    }

    public void PickedUp()
    {
        // Destroy the enemy contained
        if (containedEnemy != null)
        {
            Destroy(containedEnemy);
        }
        // Destroy this jar
        Destroy(gameObject);
    }

    void Update()
    {
        if (containedEnemy != null)
        {
            if (timer < 0)
            {
                // Respawn the enemy
                containedEnemy.SetActive(true);
                // Destroy this jar
                Destroy(gameObject);
            }
            else
                timer -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            // Returns true if the player picked up the jar
            if (other.GetComponent<PlayerController>().PickUpJar(containedEnemy == null))
            {
                PickedUp();
            }
        }
    }
}
