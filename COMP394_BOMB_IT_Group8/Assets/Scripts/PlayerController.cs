using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    //private AudioSource Audio;

    public bool isTrueRound = false;

    public int Playerindex;
    public TextMeshProUGUI playerScoreText;
    [Header("Player Stats")]
    [SerializeField]private int playerScore = 0;
    public bool playerInputActive;

    // Start is called before the first frame update
    void Start()
    {
        playerScoreText.text = playerScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInput();

    }

    private void UpdatePlayerScore(int scoreToAdd)
    {
        playerScore += scoreToAdd;
        playerScoreText.text = playerScore.ToString();
    }

    private void PlayerInputONTrueRound()
    {
        if (isTrueRound)
        {
            UpdatePlayerScore(1);
        }
    }
    
    private void PlayerInputONDecoyRound()
    {
        if (!isTrueRound)
        {
            UpdatePlayerScore(-1);
        }
    }

    protected void PlayerInput()
    {
        if (Playerindex == 1)
        {
            if (Input.GetKeyDown(KeyCode.Tab) && playerInputActive)
            {
                Debug.Log("Player 1 Pressed Tab");
                PlayerInputONDecoyRound();
                PlayerInputONTrueRound();
                playerInputActive = false;
            }
        }
        if (Playerindex == 2)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) && playerInputActive)
            {
                Debug.Log("Player 2 Pressed LeftShift");
                PlayerInputONDecoyRound();
                PlayerInputONTrueRound();
                playerInputActive = false;
            }
        }
        if (Playerindex == 3)
        {
            if (Input.GetKeyDown(KeyCode.Backspace) && playerInputActive)
            {
                Debug.Log("Player 3 Pressed Backspace");
                PlayerInputONDecoyRound();
                PlayerInputONTrueRound();
                playerInputActive = false;
            }
        }
        if (Playerindex == 4)
        {
            if (Input.GetKeyDown(KeyCode.RightShift) && playerInputActive)
            {
                Debug.Log("Player 4 Pressed RightShift");
                PlayerInputONDecoyRound();
                PlayerInputONTrueRound();
                playerInputActive = false;
            }
        }       
    }
}
