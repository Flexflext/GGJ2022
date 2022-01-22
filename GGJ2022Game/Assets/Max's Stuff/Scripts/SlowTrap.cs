using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowTrap : PlayerTrap
{
    [SerializeField]
    private float slowFactor;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.layer == playerLayer)
        {
            ActivateTrap();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.gameObject.layer == playerLayer)
        {
            DeactivateTrap();
        }
    }

    private void ActivateTrap()
    {
        //player.GetSlowed(slowFactor);
    }


    private void DeactivateTrap()
    {
        //player.GetSlowed(1);
    }
}
