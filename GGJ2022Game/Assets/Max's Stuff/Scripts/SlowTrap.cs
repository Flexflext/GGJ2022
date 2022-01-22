using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowTrap : PlayerTrap
{
    [SerializeField]
    private float slowFactor;

    [SerializeField]
    private SlowTrap counterPart;

    [SerializeField]
    private GameObject activeEffect;

    PlayerController playerToSlow;

    [SerializeField]
    private Collider triggerCollider;

    private bool isActive;

    private void OnTriggerEnter(Collider other)
    {
        if (isActive && other.gameObject.layer == playerLayer)
        {
            if(playerToSlow == null)
                playerToSlow = other.gameObject.GetComponent<PlayerController>();

            if(playerToSlow != null)
                ActivateSlow();
        }
    }

    public void Activate()
    {
        if (isActive)
            return;

        isActive = true;
        activeEffect.SetActive(true);
        triggerCollider.enabled = true;
        Debug.Log("DDDDDDDDDDDDDDDDDDDDDDDDDDDD");
        counterPart.Activate();
    }

    private void DeactivateTrap()
    {
        isActive = false;
        activeEffect.SetActive(false);
        triggerCollider.enabled = false;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == playerLayer)
        {
            DeactivateSlow();
        }
    }

    private void ActivateSlow()
    {
        playerToSlow.RecieveSlow(slowFactor);
    }


    private void DeactivateSlow()
    {
        playerToSlow.RecieveSlow(1);
    }
}
