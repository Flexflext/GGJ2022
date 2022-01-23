using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class SlowTrap : PlayerTrap
{
    [SerializeField]
    private float slowFactor;

    [SerializeField]
    private SlowTrap counterPart;

    [SerializeField]
    private VisualEffect activeEffect;

    PlayerController playerToSlow;

    [SerializeField]
    private Collider triggerCollider;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == playerLayerFirst || other.gameObject.layer == playerLayerSecond)
        {
            if(playerToSlow == null)
                playerToSlow = other.gameObject.GetComponent<PlayerController>();

            if(playerToSlow != null)
                ActivateSlow();
        }
    }

    public void Activate()
    {
        if (!counterPart.IsActivated())
        {
            counterPart.GetActivated();
            DeactivateTrap();
        }
        else
        {
            counterPart.DeactivateTrap();
            GetActivated();
        }
    }

    public void GetActivated()
    {
        if (isActivated)
            return;

        //audioManager.PlaySound(false, ESoundTypes.FireActivate);
        isActivated = true;
        activeEffect.Play();
        triggerCollider.enabled = true;
    }

    public void DeactivateTrap()
    {
        //audioManager.Stop(ESoundTypes.FireActivate);
        isActivated = false;
        activeEffect.Stop();
        triggerCollider.enabled = false;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == playerLayerFirst || other.gameObject.layer == playerLayerSecond)
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

    public bool IsActivated() => isActivated;
}
