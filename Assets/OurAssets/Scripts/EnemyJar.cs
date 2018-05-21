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
	
	void Update()
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
