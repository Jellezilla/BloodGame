using UnityEngine;
using System.Collections;

public class BloodCellSpawner : MonoBehaviour {

    private GameObject bloodCellPrefab;
    private float spawnTime = 3f;

    private GameObject[] spawnPoints;

    // Use this for initialization
    void Start () {
        bloodCellPrefab = Resources.Load("BloodCell") as GameObject;

        
        spawnPoints = GetSpawnPoints();

        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }
    /// <summary>
    /// Returns all flowareas in level tagged as "FlowArea"
    /// </summary>
    /// <returns></returns>
   GameObject[] GetSpawnPoints()
    {
        GameObject[] tmp;
        tmp = GameObject.FindGameObjectsWithTag("FlowArea");
        return tmp;
    }

    /// <summary>
    /// Spawns a bloodcell every <spawnTime> second at a random location within a random picked flowArea.
    /// </summary>
    void Spawn()
    {

        int spawnPointIndex = Random.Range(0, spawnPoints.Length);

        float offsetX = 0.0f;
        float offsetZ = 0.0f;

        BoxCollider bc = spawnPoints[spawnPointIndex].GetComponent<BoxCollider>();
        offsetX = Random.Range(-(bc.size.x / 2.2f), (bc.size.x / 2.2f));
        offsetZ = Random.Range(-(bc.size.z / 2.2f), (bc.size.z / 2.2f));

        Vector3 spawnPos = new Vector3(spawnPoints[spawnPointIndex].transform.position.x + offsetX, spawnPoints[spawnPointIndex].transform.position.y, spawnPoints[spawnPointIndex].transform.position.z + offsetZ);
        
        GameObject clone = Instantiate(bloodCellPrefab, spawnPos, Quaternion.identity) as GameObject;

        string output = "Spawned: " + bloodCellPrefab.name;
        output += ". At pos: " + spawnPos.ToString();
        output += ". At spawnPoint: " + spawnPoints[spawnPointIndex].name;
        Debug.Log(output);

        clone.transform.parent = transform;
    }

}
