using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThisClassIsTrash : MonoBehaviour
{
    public static ThisClassIsTrash Instance;

    public GameObject win1;
    public GameObject win2;
    public GameObject loose1;
    public GameObject loose2;

    private void Awake()
    {
        Instance = this;
    }
}
