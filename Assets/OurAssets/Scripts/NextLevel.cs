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
        if (player1In == true && player2In == true)
        {
            gm.ChangeLevel();
            player1In = false;
            player2In = false;
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
