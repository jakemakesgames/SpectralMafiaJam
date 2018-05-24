using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyJar : MonoBehaviour
{

    [SerializeField] float trappedTime = 5;
    [SerializeField] float showFullWait = 0.8f;
    [SerializeField] GameObject escapePatriclePrefab = null;
    [SerializeField] AnimationCurve curve = null;

    GameObject containedEnemy = null;

    float timer = 1000;
    float timeSinceSpawn = 0;

    Vector3 sphereScale = Vector3.one;


    public void Startup(GameObject enemy)
    {
        containedEnemy = enemy;
        timer = trappedTime;
        timeSinceSpawn = 0;
        //transform.GetChild(0).gameObject.SetActive(false);
        sphereScale = transform.GetChild(0).localScale;
        float scale = curve.Evaluate(0);
        transform.GetChild(0).localScale = new Vector3(scale, scale, scale);
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
                // Respawn the enemy with the position of this jar
                containedEnemy.transform.position = transform.position;
                containedEnemy.SetActive(true);
                // Create a escape particle
                if (escapePatriclePrefab != null)
                    Instantiate(escapePatriclePrefab, transform.position, escapePatriclePrefab.transform.rotation);
                // Destroy this jar
                Destroy(gameObject);
            }
            else
                timer -= Time.deltaTime;

            {
                timeSinceSpawn += Time.deltaTime;
                float scale = curve.Evaluate(timeSinceSpawn);
                transform.GetChild(0).localScale = new Vector3(scale, scale, scale);
            }
        }
    }

    private void OnTriggerStay(Collider other)
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
