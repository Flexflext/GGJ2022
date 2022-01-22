using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformTrap : PlayerTrap
{
    [SerializeField]
    private bool isActivated;

    [SerializeField]
    private PlatformTrap counterPart;

    [SerializeField]
    private float movementSpeed;

    [SerializeField]
    private float moveOffset;

    private Vector3 movePosRight;
    private Vector3 movePosLeft;

    private bool isMoving;

    public bool debugBool;

    private void Start()
    {
        movePosLeft = transform.position;

        movePosRight = new Vector3(transform.position.x + moveOffset, transform.position.y, transform.position.z);

        if (isActivated)
            this.transform.position = movePosRight;
    }

    private void Update()
    {
        if (debugBool)
        {
            ActivatedTrap();
            debugBool = false;
        }
    }

    public void ActivatedTrap()
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
}
