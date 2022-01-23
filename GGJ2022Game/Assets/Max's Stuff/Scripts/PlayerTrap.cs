using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrap : MonoBehaviour
{
    [SerializeField]
    protected ETrapType type;

    protected bool isActivated;

    [SerializeField]
    WallTrap wallType;
    [SerializeField]
    PlatformTrap platformType;
    [SerializeField]
    SlowTrap slowType;

    protected int playerLayerFirst;
    protected int playerLayerSecond;

   // [SerializeField]
   // protected PersonalSoundManager audioManager;

    private void Start()
    {
        playerLayerFirst = TrapContainer.Instance.UpperPlayerLayer();
        playerLayerSecond = TrapContainer.Instance.UpperPlayerLayer();



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

    public ETrapType GetType() => type;
}
