using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BombController : MonoBehaviour
{
    private SpriteRenderer bombSpriteRenderer;
    private RoundManager roundManager;
    private Animator animator;
    public enum BombState{idle,canDefuse,cannotDefuse}

    public BombState _bombState;
    private float trueFlashProbability = 0.5f;
    private Color ogColor;
    public bool animating=false;
    
    private static readonly Color[] NonYellowColors =
    {
        Color.red,
        Color.blue,
        Color.green,
        Color.cyan,
        Color.magenta,
    };

    private static string explode = "Explode", defuse= "Defuse";
    void Start()
    {
        bombSpriteRenderer = GetComponent<SpriteRenderer>();
        ogColor = bombSpriteRenderer.color;
        roundManager = FindObjectOfType<RoundManager>();
        animator=GetComponent<Animator>();
        InvokeRepeating("RandomBombFlash", 3f, 8f);
        
    }

    public void RandomBombFlash()
    {
        
        // Use range 1..3 to allow values 1,2 (inclusive 1, exclusive 3).
        float newRandomNumber = Random.Range(0.01f, 1f);
        StartCoroutine(FlashBomb(newRandomNumber));
        
        

        Debug.Log("Bomb flashed with chance: " + trueFlashProbability);
    }

    public void BombExplode()
    {
        if (animating) {return;}
        animating = true;
        animator.SetTrigger(explode);
    }
    public void BombDefuse()
    {
        if (animating) {return;}
        animating=true;
        animator.SetTrigger(defuse);
    }

    public void FinishAnimation()
    {
        animating = false;

    }

    private IEnumerator FlashBomb(float flashChance)
    {
        if (flashChance > trueFlashProbability)
        {
            bombSpriteRenderer.color = Color.yellow;
            _bombState=BombState.canDefuse;
        }
        else
        {
            bombSpriteRenderer.color = GetRandomNonYellowColor();
            _bombState=BombState.cannotDefuse;
        }
        roundManager.SetRoundStatus(_bombState);
        yield return new WaitForSeconds(0.8f);
        bombSpriteRenderer.color = ogColor;
        roundManager.SetRoundStatus(0f);
        _bombState = BombState.idle;
    }

    /// Returns a random color that avoids yellow hues so yellow remains exclusive to the true flash.
    private Color GetRandomNonYellowColor()
    {
        int index = Random.Range(0, NonYellowColors.Length); // max is exclusive
        return NonYellowColors[index];
    }
}
