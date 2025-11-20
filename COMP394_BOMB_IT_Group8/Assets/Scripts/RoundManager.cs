using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    // 0 = not active round, 1 = decoy round, 2 = true round
    private BombController.BombState roundStatus =BombController.BombState.idle;
    public int numberOfRoundsPlayed = 0;
    
    public bool isTrueRound = false;

    public PlayersManager pc;
    public void Start()
    {
    }
    private void FixedUpdate()
    {
            }

    private void SetPlayerInputActive(bool isActive)
    {
        foreach (PlayersManager.BombItPlayer player in pc.Players)
        {
            player.playerInputActive = isActive;
            player.hasPressedBefore = false;
        }
    }

    private void SetRoundMode(bool _isTrueRound)
    {
        isTrueRound= _isTrueRound;
    }

    public void SetRoundStatus(BombController.BombState status)
    {
        roundStatus = status;
        numberOfRoundsPlayed++;
        
        // Implement round update logic here
        switch (status)
        {
            case BombController.BombState.idle:
                // Not a round active
                // Set player input inactive
                SetPlayerInputActive(false);
                SetRoundMode(false);
                break;
            case BombController.BombState.canDefuse:
                // true round logic
                // Set player input to active
                SetPlayerInputActive(true);
                SetRoundMode(true);

                break;
            case BombController.BombState.cannotDefuse:
                // decoy round logic
                // Set player input to active
                SetPlayerInputActive(true);
                SetRoundMode(false);
                break;
        }

    }

}