using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RaceManager : MonoBehaviour
{
    public static RaceManager Instance;

    [SerializeField] private Transform firstPlayer;
    [SerializeField] private Transform secondPlayer;
    [SerializeField] private CameraControl camControl;

    [SerializeField] private TMP_Text firstPlayerScore;
    [SerializeField] private TMP_Text secondPlayerScore;
    [SerializeField] private TMP_Text infoText;
    [SerializeField] private TMP_Text spaceText;
    [SerializeField] private TMP_Text timerText;

    private bool firstIsInFront = true;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        string first = "";
        string second = "";

        GameManager.Instance.ChangeScore(ref first, ref second);

        firstPlayerScore.text = first;
        secondPlayerScore.text = second;

        Time.timeScale = 0;
        StartCoroutine(C_StartWait());
    }

    private void Update()
    {
        SetFirstAndLastPlayer();
    }

    public void SetPlayerScoreText(int _first, int _second)
    {
        firstPlayerScore.text = _first.ToString();
        secondPlayerScore.text = _second.ToString();
    }

    public void SetTexts(string _tosay)
    {
        infoText.text = _tosay;
        spaceText.gameObject.SetActive(true);
    }

    private void SetFirstAndLastPlayer()
    {
        if ((firstPlayer.position.x > secondPlayer.position.x) && !firstIsInFront)
        {
            camControl.SetFirstAndLastPlayer(firstPlayer, secondPlayer);
            firstIsInFront = true;
        }
        else if ((firstPlayer.position.x < secondPlayer.position.x) && firstIsInFront)
        {
            camControl.SetFirstAndLastPlayer(secondPlayer, firstPlayer);
            firstIsInFront = false;
        }
    }

    private IEnumerator C_StartWait()
    {

        for (int i = 0; i < 4; i++)
        {
            if (i == 3)
            {
                timerText.text = "GO";
                Time.timeScale = 1;
            }
            else
            {
                timerText.text = (3 - i).ToString();
            }

            yield return new WaitForSecondsRealtime(1);
        }

        
        timerText.text = "";
    }

    public Transform LeadingPlayer() => firstPlayer;
}
