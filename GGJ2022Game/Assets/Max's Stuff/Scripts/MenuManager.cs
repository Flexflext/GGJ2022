using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private Slider volumenSlider;

    private bool playerOneReady = true;
    private bool playerTwoReady = true;

    [SerializeField]
    private TextMeshProUGUI playerOneText;
    [SerializeField]
    private TextMeshProUGUI playerTwoText;

    [SerializeField]
    private Color32 selectColour;
    [SerializeField]
    private Color32 unselectColour;

    private void Start()
    {
        volumenSlider.value = 50;

        TogglePlayerOneStatus();
        TogglePlayerTwoStatus();
    }

    public void StarGame()
    {
        if (playerOneReady && playerTwoReady)
            SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OnSlidderValueChange()
    {
        AudioManager.Instance.ChangeVolume(volumenSlider.value);

        Debug.Log(volumenSlider.value);
    }

    public void TogglePlayerOneStatus()
    {
        playerOneReady = !playerOneReady;

        if (playerOneReady)
            playerOneText.faceColor = selectColour;
        else
            playerOneText.faceColor = unselectColour;
    }
    public void TogglePlayerTwoStatus()
    {
        playerTwoReady = !playerTwoReady;

        if (playerTwoReady)
            playerTwoText.faceColor = selectColour;
        else
            playerTwoText.faceColor = unselectColour;
    }
}
