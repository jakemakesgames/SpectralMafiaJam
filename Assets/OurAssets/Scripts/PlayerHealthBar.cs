using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthBar : MonoBehaviour
{

    Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        transform.position = transform.parent.position - cam.transform.right * 1;

        transform.LookAt(cam.transform);

        transform.position += transform.forward * 2;

    }
}
