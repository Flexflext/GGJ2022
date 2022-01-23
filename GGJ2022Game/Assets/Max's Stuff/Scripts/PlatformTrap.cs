using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformTrap : PlayerTrap
{
    [SerializeField]
    private PlatformTrap counterPart;

    [SerializeField]
    private float movementSpeed;

    [SerializeField]
    private float moveOffset;

    private Vector3 movePosRight;
    private Vector3 movePosLeft;

    private bool isMoving;

    private PlayerController playerToParent;

    public bool debugBool;

    private void Start()
    {
        float rndIdx = Random.Range(0, 1);

        if (rndIdx > 0.5f)
            SetActiveStart();
        else
            counterPart.SetActiveStart();

        movePosLeft = transform.position;

        movePosRight = new Vector3(transform.position.x + moveOffset, transform.position.y, transform.position.z);

        if (isActivated)
            this.transform.position = movePosRight;

        if(isActivated)
            StartCoroutine(C_MovePlatform());
    }

    private void Update()
    {
        if (debugBool)
        {
            Activate();
            debugBool = false;
        }
    }

    public void Activate()
    {
        if (!isMoving)
            StartCoroutine(C_MovePlatform());
    }

    private IEnumerator C_MovePlatform()
    {
        isMoving = true;

        if (isActivated)
        {
            while (transform.position.x > movePosLeft.x)
            {
              //  audioManager.PlaySound(false, ESoundTypes.PlatformMove);

                this.transform.position = Vector3.MoveTowards(transform.position, movePosLeft, movementSpeed * Time.deltaTime);

                if (!counterPart.IsMoving())
                    counterPart.StartCoroutine(counterPart.C_LetPlatformBeMoved());

                Debug.Log("Left" + gameObject.name);
                yield return null;
            }

            isActivated = false;
            isMoving = false;
            StopCoroutine(C_MovePlatform());
        }
        else
        {
            while (transform.position.x < movePosRight.x)
            {
              // audioManager.PlaySound(false, ESoundTypes.PlatformMove);

                this.transform.position = Vector3.MoveTowards(transform.position, movePosRight, movementSpeed * Time.deltaTime);

                if (!counterPart.IsMoving())
                    counterPart.StartCoroutine(counterPart.C_LetPlatformBeMoved());

                Debug.Log("Right" + gameObject.name);
                yield return null;
            }

            isActivated = true;
            isMoving = false;
            StopCoroutine(C_MovePlatform());
        }

        StopCoroutine(C_MovePlatform());
    }

    public IEnumerator C_LetPlatformBeMoved()
    {
        isMoving = true;

        if (isActivated)
        {
            while (transform.position.x > movePosLeft.x)
            {
              //  audioManager.PlaySound(false, ESoundTypes.PlatformMove);

                this.transform.position = Vector3.MoveTowards(transform.position, movePosLeft, movementSpeed * Time.deltaTime);

                Debug.Log("Left");
                yield return null;
            }

            isActivated = false;
            isMoving = false;
            StopCoroutine(C_LetPlatformBeMoved());
        }
        else
        {
            while (transform.position.x < movePosRight.x)
            {
               // audioManager.PlaySound(false, ESoundTypes.PlatformMove);

                this.transform.position = Vector3.MoveTowards(transform.position, movePosRight, movementSpeed * Time.deltaTime);

                Debug.Log("Right");
                yield return null;
            }

            isActivated = true;
            isMoving = false;
            StopCoroutine(C_LetPlatformBeMoved());
        }

        StopCoroutine(C_LetPlatformBeMoved());
    }

    public bool IsMoving() => isMoving;

    public void SetActiveStart() => isActivated = true;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == playerLayerFirst || collision.gameObject.layer == playerLayerSecond)
        {
            if (playerToParent == null)
                playerToParent = collision.gameObject.GetComponent<PlayerController>();

            if (playerToParent != null)
                playerToParent.transform.SetParent(this.transform);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if(playerToParent != null)
        {
            playerToParent.transform.parent = null;
            playerToParent = null;
        }
    }

}
