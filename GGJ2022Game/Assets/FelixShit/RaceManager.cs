using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceManager : MonoBehaviour
{
    public static RaceManager Instance;

    [SerializeField] private Transform firstPlayer;
    [SerializeField] private Transform secondPlayer;
    [SerializeField] private CameraControl camControl;

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

    private void Update()
    {
        SetFirstAndLastPlayer();
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



}
