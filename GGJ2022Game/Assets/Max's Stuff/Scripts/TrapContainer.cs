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


    public void SetNextTrapAndActivate(float _xPos)
    {
        for (int i = 0; i < allTraps.Length; i++)
        {
            if (_xPos < allTraps[i].transform.position.x)
            {
                currentTrap = allTraps[i];
            }
        }

        currentTrap.TriggerTrap();
    }

    public int PlayerLayer() => playerLayer;
    public GameObject Player() => player;

}
