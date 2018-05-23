using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyJar : MonoBehaviour
{

    [SerializeField] float trappedTime = 5;
    [SerializeField] float showFullWait = 0.8f;
    [SerializeField] GameObject escapePatriclePrefab = null;

    GameObject containedEnemy = null;

    float timer = 1000;
    float showFullTimer = 1000;

    public void Startup(GameObject enemy)
    {
        containedEnemy = enemy;
        timer = trappedTime;
        showFullTimer = showFullWait;
        transform.GetChild(0).gameObject.SetActive(false);
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
                // Create a escape particle
                if (escapePatriclePrefab != null)
                    Instantiate(escapePatriclePrefab, transform.position, escapePatriclePrefab.transform.rotation);
                // Destroy this jar
                Destroy(gameObject);
            }
            else
                timer -= Time.deltaTime;
            if (showFullTimer < 0)
            {
                // Show the sphere in the jar
                transform.GetChild(0).gameObject.SetActive(true);
            }
            else
                showFullTimer -= Time.deltaTime;
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
