using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrap : MonoBehaviour
{
    [SerializeField]
    protected ETrapType type;

    protected int playerLayer;

    protected GameObject player;

    private void Start()
    {
        playerLayer = TrapContainer.Instance.PlayerLayer();

        player = TrapContainer.Instance.Player();
    }
}
