using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BombController : MonoBehaviour
{
    private SpriteRenderer bombSpriteRenderer;

    private int doubleFlashChance = 1;
    private int singleFlashChance = 2;
    private Color ogColor;
    // Start is called before the first frame update
    void Start()
    {
        bombSpriteRenderer = GetComponent<SpriteRenderer>();
        ogColor = bombSpriteRenderer.color;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RandomBombFlash();
    }

    public void RandomBombFlash()
    {
        // Don't instantiate UnityEngine.Random (it's a static class).
        // Use UnityEngine.Random.Range for integer random values.
        // Use range 1..3 to allow values 1,2 (inclusive 1, exclusive 3).
        int flashChance = Random.Range(1, 3);
        if (flashChance == doubleFlashChance)
        {
            StartCoroutine(FlashBomb());
            StartCoroutine(FlashBomb());
        }
        else if (flashChance == singleFlashChance)
        {
            StartCoroutine(FlashBomb());
        }
    }
    private IEnumerator FlashBomb()
    {
        bombSpriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        bombSpriteRenderer.color = ogColor;
    }
}
