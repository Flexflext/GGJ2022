using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private Vector2 xLerpZone;

    [SerializeField] private Vector2 minMaxZPos;
    [SerializeField] private float maxXDistance;

    [SerializeField] private Transform frontPlayer;
    [SerializeField] private Transform backPlayer;

    private float currentSmoothTime;
    private float currentDistance;
    private float distancePercent;


    private void FixedUpdate()
    {
        LerpCameras();
    }

    private void LerpCameras()
    {
        currentDistance = Mathf.Abs(frontPlayer.position.x - backPlayer.position.x);

        distancePercent = currentDistance / maxXDistance;

        if (distancePercent <= 1)
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, Mathf.Lerp(minMaxZPos.x, minMaxZPos.y, distancePercent));
        }
        else
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, minMaxZPos.y);
        }


        if (frontPlayer.position.x <= this.transform.position.x + xLerpZone.x || frontPlayer.position.x >= this.transform.position.x + xLerpZone.y)
        {
            this.transform.position = new Vector3(Mathf.SmoothDamp(this.transform.position.x, frontPlayer.position.x,ref currentSmoothTime, 1f), this.transform.position.y, this.transform.position.z);
        }
        else
        {
            this.transform.position = new Vector3(Mathf.Lerp(this.transform.position.x, frontPlayer.position.x, 1* Time.fixedDeltaTime), this.transform.position.y, this.transform.position.z);
        }    
    }

    public void SetFirstAndLastPlayer(Transform _front, Transform _back)
    {
        frontPlayer = _front;
        backPlayer = _back;
    }
}
