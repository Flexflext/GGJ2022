using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private int firstPlayerWon = 0;
    private int secondPlayerWon = 0;


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

        DontDestroyOnLoad(this.gameObject);
    }

    public void ChangeScore(ref string _first, ref string _second)
    {
        _first = firstPlayerWon.ToString();
        _second = secondPlayerWon.ToString();
    }

    public void SetPlayerScore(EPlayer _playertype, bool _win = false)
    {
        
        if (_win)
        {
            switch (_playertype)
            {
                case EPlayer.First:
                    firstPlayerWon++;
                    break;
                case EPlayer.Second:
                    secondPlayerWon++;
                    break;
            }
        }
        else
        {
            switch (_playertype)
            {
                case EPlayer.First:
                    secondPlayerWon++;
                    break;
                case EPlayer.Second:
                    firstPlayerWon++;
                    break;
            }
        }

        
        RaceManager.Instance.SetPlayerScoreText(firstPlayerWon, secondPlayerWon);

        if (firstPlayerWon == 2 || secondPlayerWon == 2)
        {
            //End Game
            

            firstPlayerWon = 0;
            secondPlayerWon = 0;

            RaceManager.Instance.SetTexts(firstPlayerWon == 2 ? "Player 1 Won" : "Player 2 Won");

            StartCoroutine(LoadSceneAfterDelay(SceneManager.GetActiveScene().buildIndex));
        }
        else
        {
            RaceManager.Instance.SetTexts($"{firstPlayerWon} - {secondPlayerWon}");

            StartCoroutine(LoadSceneAfterDelay(SceneManager.GetActiveScene().buildIndex));
        }
    }


    private IEnumerator LoadSceneAfterDelay(int _scenetoload)
    {
        float cur;

        while (Time.timeScale > 0.5f)
        {
            cur = Time.timeScale - Time.unscaledDeltaTime * 0.5f;


            if (cur <= 0)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = cur;
            }

            yield return null;
        }

        

        Time.timeScale = 0;

        while (!Input.GetKey(KeyCode.Space))
        {
            yield return null;
        }
        Time.timeScale = 1;
        SceneManager.LoadScene(_scenetoload);
    }
}
