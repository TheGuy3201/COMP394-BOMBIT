using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BombController : MonoBehaviour
{
    private SpriteRenderer bombSpriteRenderer;
    private RoundManager roundManager;
    private AudioSource _audioSource;

    public string trueFlashSfx = "";
    public string decoyFlashSfx = "";

    private int trueFlashChance = 1;
    private int decoyFlashChance = 2;
    private Color ogColor;

    void Start()
    {
        // gets the audio source component
        _audioSource = GetComponent<AudioSource>();
        bombSpriteRenderer = GetComponent<SpriteRenderer>();
        ogColor = bombSpriteRenderer.color;
        roundManager = FindObjectOfType<RoundManager>();
        InvokeRepeating("RandomBombFlash", 3f, 8f);
    }

    public void RandomBombFlash()
    {
        // Use range 1..3 to allow values 1,2 (inclusive 1, exclusive 3).
        int flashChance = Random.Range(1, 3);
        StartCoroutine(FlashBomb(flashChance));

        roundManager.SetRoundStatus(flashChance);

        Debug.Log("Bomb flashed with chance: " + flashChance);
    }

    void BombExplode()
    {
        //Implement bomb explosion logic here
        throw new System.NotImplementedException();
    }

    private IEnumerator FlashBomb(int flashChance)
    {
        if (flashChance == trueFlashChance)
        {   // plays true flash sound effect and changes bomb color to yellow
            if (trueFlashSfx != null) AudioManager.Play(trueFlashSfx);
            bombSpriteRenderer.color = Color.yellow;
        }
        else if (flashChance == decoyFlashChance)
        {   // plays decoy flash sound effect and changes bomb color to a random non-yellow color
            if (decoyFlashSfx != null) AudioManager.Play(decoyFlashSfx);
            bombSpriteRenderer.color = GetRandomNonYellowColor();
        }
        yield return new WaitForSeconds(0.8f);
        bombSpriteRenderer.color = ogColor;
        roundManager.SetRoundStatus(0);
    }

    /// Returns a random color that avoids yellow hues so yellow remains exclusive to the true flash.
    private Color GetRandomNonYellowColor()
    {
        const int maxAttempts = 12;
        // Hue for yellow is approximately 0.10 - 0.17 in 0..1 range (around 36°-61°).
        const float yellowHueMin = 0.10f;
        const float yellowHueMax = 0.17f;

        for (int i = 0; i < maxAttempts; i++)
        {
            Color c = new Color(Random.value, Random.value, Random.value);
            Color.RGBToHSV(c, out float h, out float s, out float v);

            // Reject colors that are close to yellow: hue in yellow range and reasonably bright and saturated.
            if (!(h >= yellowHueMin && h <= yellowHueMax && v > 0.45f && s > 0.35f))
            {
                return c;
            }
        }

        // Fallback: return a safe non-yellow color
        return Color.magenta;
    }
}
