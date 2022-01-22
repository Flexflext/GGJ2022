using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    private Transform leadingPlayer;

    [SerializeField]
    private GameObject[] islandPrefabs;

    private GameObject[] allIslandsToSpawn;
    [SerializeField]
    private int islandSpawnAmount;



    [SerializeField]
    private float deactivationDistance;

    [SerializeField]
    private float refreshRate;

    [SerializeField]
    private float spawnPointOffset;

    private List<Vector3> spawnPos;

    private void Awake()
    {
        leadingPlayer = RaceManager.Instance.LeadingPlayer();
        allIslandsToSpawn = new GameObject[islandSpawnAmount];
        spawnPos = new List<Vector3>();

        for (int i = 0; i < islandSpawnAmount; i++)
        {
            GameObject island = Instantiate(allIslandsToSpawn[Random.Range(0, allIslandsToSpawn.Length - 1)], transform.position, Quaternion.identity);
            allIslandsToSpawn[i] = island;
        }

        for (int i = 0; i < 3; i++)
        {
            spawnPos.Add(new Vector3(leadingPlayer.transform.position.x + spawnPointOffset,
                                     leadingPlayer.transform.position.y,
                                     leadingPlayer.transform.position.z));

            spawnPos.Add(new Vector3(leadingPlayer.transform.position.x - spawnPointOffset,
                                     leadingPlayer.transform.position.y,
                                     leadingPlayer.transform.position.z));
        }

        StartCoroutine(C_CheckForPlayer());
    }

    private void Update()
    {
        if (leadingPlayer == null)
        {
            leadingPlayer = RaceManager.Instance.LeadingPlayer();

        }
    }

    private IEnumerator C_CheckForPlayer()
    {
        while (Time.timeScale >= 1)
        {
            for (int i = 0; i < spawnPos.Count; i++)
            {

            }

            yield return new WaitForSeconds(refreshRate);
        }

        StopCoroutine(C_CheckForPlayer());
    }

    private void ActivateIsland(Vector3 _spawnPos)
    {
        
    }

    private void DeactivateIsland(GameObject _toDeactivate)
    {

    }

    private void OnDrawGizmos()
    {
        if (spawnPos == null)
            return;

        Gizmos.color = Color.blue;

        for (int i = 0; i < spawnPos.Count; i++)
        {
            Gizmos.DrawWireSphere(spawnPos[i], 0.5f);

        }
    }
}