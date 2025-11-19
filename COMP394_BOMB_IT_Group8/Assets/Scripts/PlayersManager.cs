using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using TMPro;
using Button = UnityEngine.UI.Button;
using UnityEngine.PlayerLoop;
using UnityEngine.EventSystems;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;

public class PlayersManager : MonoBehaviour
{
    public string buttonPressSfx = "";

    public bool isTrueRound = false;
    [System.Serializable]
    public class BombItPlayer
    {
        public TextMeshProUGUI playerScoreText;
        [SerializeField] public int playerScore = 0;
        [SerializeField] public bool playerInputActive;
        public Button btn;

        private MonoBehaviour _runner;
        private Coroutine _pressRoutine;
        private PointerEventData _data;

        public void InitEvents(MonoBehaviour runner)
        {
            _runner = runner;
            _data = new PointerEventData(EventSystem.current);
        }

        private void PrepEventData()
        {
            _data.Reset();
            _data.pointerId = -1;
            _data.button = PointerEventData.InputButton.Left;
            _data.clickCount = 1;
        }

        public void PressFor(float seconds)
        {
            if (_runner == null || btn == null) return;

            if (_pressRoutine != null) _runner.StopCoroutine(_pressRoutine);
            _pressRoutine = _runner.StartCoroutine(PressRoutine(seconds));
        }

        public void CancelPress()
        {
            if (_runner != null && _pressRoutine != null)
            {
                _runner.StopCoroutine(_pressRoutine);
                _pressRoutine = null;
            }
            // ensure visual release
            if (btn)
            {
                PrepEventData();
                ExecuteEvents.Execute(btn.gameObject, _data, ExecuteEvents.pointerUpHandler);
                ExecuteEvents.Execute(btn.gameObject, _data, ExecuteEvents.pointerExitHandler);
            }
        }

        private IEnumerator PressRoutine(float seconds)
        {
            if (EventSystem.current == null || !btn || !btn.interactable) yield break;

            PrepEventData();
            ExecuteEvents.Execute(btn.gameObject, _data, ExecuteEvents.pointerEnterHandler);
            ExecuteEvents.Execute(btn.gameObject, _data, ExecuteEvents.pointerDownHandler);

            float t = 0f;
            while (t < seconds) { t += Time.unscaledDeltaTime; yield return null; }

            PrepEventData();
            ExecuteEvents.Execute(btn.gameObject, _data, ExecuteEvents.pointerUpHandler);
            ExecuteEvents.Execute(btn.gameObject, _data, ExecuteEvents.pointerExitHandler);

            _pressRoutine = null;
        }

        // Optional helpers you already had
        public void UpdatePlayerScore(int add) { playerScore += add; playerScoreText.text = playerScore.ToString(); }
        public void UpdatePlayerScore() { playerScoreText.text = playerScore.ToString(); }
    }

    PointerEventData pointerEventData;

    [SerializeField] public BombItPlayer[] Players;

    // Start is called before the first frame update
    void Start()
    {
        foreach (BombItPlayer pl in Players)
        {

            pl.InitEvents(this);
            pl.UpdatePlayerScore();
        }
    }


    // Update is called once per frame
    void Update()
    {
        PlayerInput();

    }


    private void PlayerInputOnRound(BombItPlayer player)
    {
        if (isTrueRound)
        {
            player.UpdatePlayerScore(1);
        }
        else
        {
            player.UpdatePlayerScore(-1);
        }
    }

    protected void PlayerInput()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("TitleScreen");
        }
        // P1 — Tab
        if (Input.GetKeyDown(KeyCode.Tab) && Players[0].playerInputActive)
        {   // Play sound effect when button is pressed
            if (buttonPressSfx != null) AudioManager.Play(buttonPressSfx);
            Debug.Log("Player 1 Pressed Tab");
            PlayerInputOnRound(Players[0]);
            Players[0].playerInputActive = false;
            Players[0].PressFor(0.25f);
        }
        if (Input.GetKeyUp(KeyCode.Tab)) Players[0].CancelPress();

        // P2 — LeftShift
        if (Input.GetKeyDown(KeyCode.LeftShift) && Players[1].playerInputActive)
        {   // Play sound effect when button is pressed
            if (buttonPressSfx != null) AudioManager.Play(buttonPressSfx);
            Debug.Log("Player 2 Pressed LeftShift");
            PlayerInputOnRound(Players[1]);
            Players[1].playerInputActive = false;
            Players[1].PressFor(0.25f);
        }
        if (Input.GetKeyUp(KeyCode.LeftShift)) Players[1].CancelPress();

        // P3 — RightShift
        if (Input.GetKeyDown(KeyCode.RightShift) && Players[2].playerInputActive)
        {   // Play sound effect when button is pressed
            if (buttonPressSfx != null) AudioManager.Play(buttonPressSfx);
            Debug.Log("Player 3 Pressed RightShift");
            PlayerInputOnRound(Players[2]);
            Players[2].playerInputActive = false;
            Players[2].PressFor(0.25f);
        }
        if (Input.GetKeyUp(KeyCode.RightShift)) Players[2].CancelPress();

        // P4 — Backspace
        if (Input.GetKeyDown(KeyCode.Backspace) && Players[3].playerInputActive)
        {   // Play sound effect when button is pressed
            if (buttonPressSfx != null) AudioManager.Play(buttonPressSfx);
            Debug.Log("Player 4 Pressed Backspace");
            PlayerInputOnRound(Players[3]);
            Players[3].playerInputActive = false;
            Players[3].PressFor(0.25f);
        }
        if (Input.GetKeyUp(KeyCode.Backspace)) Players[3].CancelPress();
    }
}
