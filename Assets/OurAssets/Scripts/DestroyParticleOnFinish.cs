using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParticleOnFinish : MonoBehaviour
{
    void Awake ()
    {
        ParticleSystem ps = GetComponent<ParticleSystem>();
        Destroy(gameObject, ps.main.duration);
	}
}
