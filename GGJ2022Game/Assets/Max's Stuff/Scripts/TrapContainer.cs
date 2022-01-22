using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public enum ETrapType
{
    platform,
    wall,
    slow
}

public class TrapContainer : MonoBehaviour
{
    public static TrapContainer Instance;

    [SerializeField]
    private PlayerTrap[] allTraps;

    private PlayerTrap currentTrap;

    [SerializeField]
    private int playerLayer;

    [SerializeField]
    private GameObject player;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
    }

    public void ChangeNextTrap(float _xPos)
    {
        
    }

    public int PlayerLayer() => playerLayer;
    public GameObject Player() => player;

}
