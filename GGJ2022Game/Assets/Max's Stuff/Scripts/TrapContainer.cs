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

    private PlayerTrap previousTrap;

    [SerializeField]
    private int upperPlayerLayer;
    [SerializeField]
    private int lowerPlayerLayer;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
    }

    private void Start()
    {
        currentTrap = allTraps[0];
    }

    public ETrapType SetNextTrapAndActivate(float _xPos)
    {
        float lowestD = 100;

        for (int i = 0; i < allTraps.Length; i++)
        {
            float distance = allTraps[i].transform.position.x - _xPos;

            if (distance < lowestD && allTraps[i].transform.position.x > _xPos)
            {
                currentTrap = allTraps[i];
                lowestD = distance;
            }
        }

        currentTrap.TriggerTrap();
        return currentTrap.GetType();
    }

    public int UpperPlayerLayer() => upperPlayerLayer;
    public int LowerPlayerLayer() => lowerPlayerLayer;

    private void OnDrawGizmos()
    {
        if (currentTrap == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(currentTrap.transform.position, 0.5f);
    }
}
