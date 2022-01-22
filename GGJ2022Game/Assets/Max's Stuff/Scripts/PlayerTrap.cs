using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrap : MonoBehaviour
{
    [SerializeField]
    protected ETrapType type;

    protected bool isActivated;

    WallTrap wallType;
    PlatformTrap platformType;
    SlowTrap slowType;

    protected int playerLayer;

    private void Start()
    {
        playerLayer = TrapContainer.Instance.PlayerLayer();

        switch (type)
        {
            case ETrapType.platform:
                platformType = GetComponent<PlatformTrap>();
                break;
            case ETrapType.wall:
                wallType = GetComponent<WallTrap>();
                break;
            case ETrapType.slow:
                slowType = GetComponent<SlowTrap>();
                break;
            default:
                break;
        }
    }

    public void TriggerTrap() 
    {
        switch (type)
        {
            case ETrapType.platform:
                platformType.Activate();
                break;
            case ETrapType.wall:
                wallType.Activate();
                break;
            case ETrapType.slow:
                slowType.Activate();
                break;
            default:
                break;
        }
    }
}
