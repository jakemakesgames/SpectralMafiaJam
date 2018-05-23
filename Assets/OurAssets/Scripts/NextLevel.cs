using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevel : MonoBehaviour
{
    GameManager gm;
    bool player1In;
    bool player2In;

    void Start()
    {
        gm = GameManager.Instance;
    }

    void Update()
    {
        // P1 alive and in 
        if (gm.Player1GO.GetComponent<PlayerController>().IsAlive && player1In == true)
        {
            // p2 dead
            if (gm.Player2GO.GetComponent<PlayerController>().IsAlive == false)
            {
                gm.ChangeLevel();
                player1In = false;
                player2In = false;
            }
            // p2 alive and in
            else if (player2In == true)
            {
                gm.ChangeLevel();
                player1In = false;
                player2In = false;
            }
        }
        // P1 not alive or not in
        // P2 alive and in
        else if (gm.Player2GO.GetComponent<PlayerController>().IsAlive && player2In == true)
        {
            // P1 dead
            if (gm.Player1GO.GetComponent<PlayerController>().IsAlive == false)
            {
                gm.ChangeLevel();
                player1In = false;
                player2In = false;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == gm.Player1GO)
        {
            if (gm.Player1GO != null)
            {
                if (gm.Player2GO == null)
                {
                    gm.ChangeLevel();
                }
                else
                {
                    player1In = true;
                }
            }

        }
        if (other.gameObject == gm.Player2GO)
        {
            player2In = true;
        }
    }


    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == gm.Player1GO)
        {
            player1In = false;
        }
        if (other.gameObject == gm.Player2GO)
        {
            player2In = false;
        }


    }
}
