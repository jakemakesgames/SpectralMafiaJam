using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] int damage = 10;
    [SerializeField] GameObject destroyParticlesPrefab = null;

    public int Damage { get { return damage; } set { damage = value; } }

    private void Awake()
    {
        Destroy(gameObject, 20);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Wall" || other.tag == "Player")
        {
            if (other.tag == "Player")
            {
                other.GetComponent<PlayerController>().TakeDamage(damage);
            }

            // Do some particles
            if (destroyParticlesPrefab != null)
                Instantiate(destroyParticlesPrefab, transform.position, destroyParticlesPrefab.transform.rotation);
            // Destroy this bullet
            Destroy(gameObject);
        }
    }
}
