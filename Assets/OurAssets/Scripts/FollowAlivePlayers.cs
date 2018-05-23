using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowAlivePlayers : MonoBehaviour
{

    [SerializeField] List<GameObject> players;

    [SerializeField] float speed = 5;

    //Vector3 offset;

    bool moving = false;

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
        transform.position = GetAvgPos();// + offset;
    }

    public void AddObject(GameObject go)
    {
        players.Add(go);
    }

    void LateUpdate()
    {
        if (moving)
        {
            Vector3 targetpos = GetAvgPos();// + offset;
            transform.position += (targetpos - transform.position) * Time.deltaTime * speed;
        }
    }

    private Vector3 GetAvgPos()
    {
        Vector3 avgPosition = Vector3.zero;

        int aliveCount = 0;

        // Clac avg position 
        for (int i = 0; i < players.Count; i++)
        {
            // Only add pos of alive
            if (players[i].GetComponent<PlayerController>().IsAlive)
                avgPosition += players[i].transform.position;
            aliveCount++;
        }
        avgPosition = avgPosition / (float)aliveCount;
        return avgPosition;
    }
}
