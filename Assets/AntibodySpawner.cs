using UnityEngine;
using System.Collections;

public class AntibodySpawner : MonoBehaviour {


    private GameObject whiteBloodCellPrefab;


    private GameObject[] spawnPoints;

    private float spawnTime;
    ThreatSystem ts;



	// Use this for initialization
	void Start () {
        whiteBloodCellPrefab = Resources.Load("WhiteBloodCell") as GameObject;
        spawnPoints = GetSpawnPoints();
        ts = GameObject.Find("ThreatSystem").GetComponent<ThreatSystem>();
        StartCoroutine(Spawn());
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

    IEnumerator Spawn()
    {
        while (true)
        {
            int spawnPointIndex = Random.Range(0, spawnPoints.Length);

            float offsetX = 0.0f;
            float offsetZ = 0.0f;

            BoxCollider bc = spawnPoints[spawnPointIndex].GetComponent<BoxCollider>();
            offsetX = Random.Range(-(bc.size.x / 2.2f), (bc.size.x / 2.2f));
            offsetZ = Random.Range(-(bc.size.z / 2.2f), (bc.size.z / 2.2f));

            Vector3 spawnPos = new Vector3(spawnPoints[spawnPointIndex].transform.position.x + offsetX, spawnPoints[spawnPointIndex].transform.position.y, spawnPoints[spawnPointIndex].transform.position.z + offsetZ);

            GameObject clone = Instantiate(whiteBloodCellPrefab, spawnPos, Quaternion.identity) as GameObject;

            clone.transform.parent = transform;

            spawnTime = CalcSpawnTime(spawnTime);
            Debug.Log("spawn! time: " + spawnTime.ToString());
            yield return new WaitForSeconds(spawnTime);

        }
    }

    private float CalcSpawnTime(float time)
    {
       // time = ts.GetThreatLevel() 
        return 10.0f;
    }
}
