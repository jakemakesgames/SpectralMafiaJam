﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] GameObject emptyJarPrefab;
    [SerializeField] GameObject destroyParticlesPrefab = null;

    private void Awake()
    {
        Destroy(gameObject, 20);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Wall" || other.tag == "Enemy")
        {
            if (other.tag == "Wall")
            {
                // Create a empty jar
                Instantiate(emptyJarPrefab, transform.position + gameObject.GetComponent<Rigidbody>().velocity.normalized * -0.5f, emptyJarPrefab.transform.rotation);
            }

            // Do some particles
            if (destroyParticlesPrefab != null)
                Instantiate(destroyParticlesPrefab, transform.position, destroyParticlesPrefab.transform.rotation);
            // Destroy this bullet
            Destroy(gameObject);
        }
    }

}
