using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTrap : PlayerTrap
{

    [SerializeField]
    private WallTrap counterPart;

    [SerializeField]
    private float movementSpeed;

    [SerializeField]
    private float moveOffset;

    private Vector3 movePosUp;
    private Vector3 movePosDown;

    private bool isMoving;

    public bool debugBool;

    private void Start()
    {
        float rndIdx = Random.Range(0, 1);

        if (rndIdx > 0.5f)
            SetActiveStart();
        else
            counterPart.SetActiveStart();

        movePosDown = transform.position;

        movePosUp = new Vector3(transform.position.x, transform.position.y + moveOffset, transform.position.z);

        if (isActivated)
            this.transform.position = movePosUp;

        if (isActivated)
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
            while (transform.position.y > movePosDown.y)
            {
                this.transform.position = Vector3.MoveTowards(transform.position, movePosDown, movementSpeed * Time.deltaTime);

                if (!counterPart.IsMoving())
                    counterPart.StartCoroutine(counterPart.C_LetPlatformBeMoved());

                Debug.Log("Down" + gameObject.name);
                yield return null;
            }

            isActivated = false;
            isMoving = false;
            StopCoroutine(C_MovePlatform());
        }
        else
        {
            while (transform.position.y < movePosUp.y)
            {
                this.transform.position = Vector3.MoveTowards(transform.position, movePosUp, movementSpeed * Time.deltaTime);

                if(!counterPart.IsMoving())
                    counterPart.StartCoroutine(counterPart.C_LetPlatformBeMoved());

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
            while (transform.position.y > movePosDown.y)
            {
                this.transform.position = Vector3.MoveTowards(transform.position, movePosDown, movementSpeed * Time.deltaTime);

                yield return null;
            }

            isActivated = false;
            isMoving = false;
            StopCoroutine(C_LetPlatformBeMoved());
        }
        else
        {
            while (transform.position.y < movePosUp.y)
            {
                this.transform.position = Vector3.MoveTowards(transform.position, movePosUp, movementSpeed * Time.deltaTime);

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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(movePosUp, 0.5f);
    }
}
