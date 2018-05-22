﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObjects : MonoBehaviour
{
    [SerializeField] List<GameObject> objects;

    [SerializeField] float speed = 5;

    Vector3 offset;

    bool moving = true;

    public void Move()
    {
        moving = true;
    }

    public void Stop()
    {
        moving = false;
    }

    public void SnapToTargetPos()
    {
        transform.position = GetAvgPos() + offset;
    }

    public void AddObject(GameObject go)
    {
        objects.Add(go);
    }

    private void Awake()
    {
        offset = transform.position - GetAvgPos();
    }

    void LateUpdate()
    {
        if (moving)
        {
            Vector3 targetpos = GetAvgPos() + offset;
            transform.position += (targetpos - transform.position) * Time.deltaTime * speed;
        }
    }

    private Vector3 GetAvgPos()
    {
        Vector3 avgPosition = Vector3.zero;
        // Clac avg position 
        for (int i = 0; i < objects.Count; i++)
        {
            avgPosition += objects[i].transform.position;
        }
        avgPosition = avgPosition / objects.Count;
        return avgPosition;
    }
}
