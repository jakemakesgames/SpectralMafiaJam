using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] GameObject emptyJarPrefab;
    [SerializeField] GameObject enemyJarPrefab;
    [SerializeField] GameObject destroyParticlesPrefab = null;

    bool isAlive = true;

    public bool IsAlive { get { return isAlive; } set { isAlive = value; } }

    private void Awake()
    {
        Destroy(gameObject, 20);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsAlive && (other.tag == "Wall" || other.tag == "Enemy"))
        {
            if (other.tag == "Wall")
            {
                // Create a empty jar
                Instantiate(emptyJarPrefab, transform.position + gameObject.GetComponent<Rigidbody>().velocity.normalized * -0.5f, emptyJarPrefab.transform.rotation);
            }

            if (other.tag == "Enemy")
            {
                other.gameObject.SetActive(false);
                GameObject newJar = Instantiate(enemyJarPrefab, transform.position, enemyJarPrefab.transform.rotation);
                newJar.GetComponent<EnemyJar>().Startup(gameObject);
            }

            // Do some particles
            if (destroyParticlesPrefab != null)
                Instantiate(destroyParticlesPrefab, transform.position, destroyParticlesPrefab.transform.rotation);
            // Destroy this bullet
            Destroy(gameObject);
            IsAlive = false;
        }
    }

}
