using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public int Playerindex;
    public TextMeshProUGUI playerScoreText;
    [Header("Player Stats")]
    [SerializeField]private int playerScore = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
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
    
    protected void PlayerInput()
    {
        //Debug.Log("Player Index: " + Playerindex);
        if (Playerindex == 1)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                Debug.Log("Player 1 Pressed Tab");
                UpdatePlayerScore(1);
            }
        }
        if (Playerindex == 2)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                Debug.Log("Player 2 Pressed LeftShift");
                UpdatePlayerScore(1);
            }
        }
        if (Playerindex == 3)
        {
            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                Debug.Log("Player 3 Pressed Backspace");
                UpdatePlayerScore(1);
            }
        }
        if (Playerindex == 4)
        {
            if (Input.GetKeyDown(KeyCode.RightShift))
            {
                Debug.Log("Player 4 Pressed RightShift");
                UpdatePlayerScore(1);
            }
        }       
    }
}
