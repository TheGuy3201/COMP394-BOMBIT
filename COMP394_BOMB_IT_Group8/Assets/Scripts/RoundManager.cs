using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class RoundManager : MonoBehaviour
{
    // 0 = not active round, 1 = decoy round, 2 = true round
    private int roundStatus = 0;
    public int numberOfRoundsPlayed = 0;

    public List<PlayerController> players = new List<PlayerController>();

    public void Start()
    {
        players.AddRange(FindObjectsOfType<PlayerController>());
    }
    private void FixedUpdate()
    {
        // Implement round update logic here
        switch (roundStatus)
        {
            case 0:
                // Not a round active
                // Set player input inactive
                SetPlayerInputActive(false);
                SetRoundMode(false);
                break;
            case 1:
                // true round logic
                // Set player input to active
                SetPlayerInputActive(true);
                SetRoundMode(true);

                break;
            case 2:
                // decoy round logic
                // Set player input to active
                SetPlayerInputActive(true);
                SetRoundMode(false);
                break;
        }
    }

    private void SetPlayerInputActive(bool isActive)
    {
        foreach (PlayerController player in players)
        {
            player.playerInputActive = isActive;
        }
    }

    private void SetRoundMode(bool isTrueRound)
    {
        foreach (PlayerController player in players)
        {
            player.isTrueRound = isTrueRound;
        }
    }

    public void SetRoundStatus(int status)
    {
        roundStatus = status;
        numberOfRoundsPlayed++;
    }

    public int GetRoundStatus()
    {
        return roundStatus;
    }

}