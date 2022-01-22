using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private Vector2 minMaxZPos;
    [SerializeField] private float maxXDistance;

    [SerializeField] private Transform frontPlayer;
    [SerializeField] private Transform backPlayer;


    private void LateUpdate()
    {
        LerpCameras();
    }

    private void LerpCameras()
    {
        float currentDistance = Mathf.Abs(frontPlayer.position.x - backPlayer.position.x);

        float distancePercent = currentDistance / maxXDistance;

        if (distancePercent <= 1)
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, Mathf.Lerp(minMaxZPos.x, minMaxZPos.y, distancePercent));
        }
        else
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, minMaxZPos.y);

        }
    }
}
