using UnityEngine;
using System.Collections;

public class CellSpawner : MonoBehaviour {

    [SerializeField]
    private Transform _whiteCells;
    [SerializeField]
    private Transform _redCells;
    [SerializeField]
    private float _playerSpawnerDistance;
    [SerializeField]
    private GameObject _whiteCellPrefab;
    [SerializeField]
    private GameObject _redCellPrefab;
    private GameObject _player;
    private GameObject[] _spawnPoints;

    [SerializeField]
    private float _wCellSpawnTMult;
    [SerializeField]
    private float _rCellSpawnDelay;
    ThreatSystem _ts;



	// Use this for initialization
	void Start () {

        _player = GameObject.FindGameObjectWithTag(Tags.playerTag);
        _spawnPoints = GameObject.FindGameObjectsWithTag(Tags.flowAreaTag);
        _ts = gameObject.GetComponent<ThreatSystem>();
        StartCoroutine(SpawnWhiteCells());
        StartCoroutine(SpawnRedCells());
    }

    private GameObject ClosestSpawnPoint()
    {
        if (_spawnPoints != null)
        {
            GameObject closestPoint = _spawnPoints[0];

            for (int i = 1; i < _spawnPoints.Length; i++)
            {
                if (Vector3.Distance(_player.transform.position, closestPoint.transform.position) >
                    Vector3.Distance(_player.transform.position, _spawnPoints[i].transform.position))
                {
                    closestPoint = _spawnPoints[i];
                }
            }

            return closestPoint;
        }
        else
        {
            return null;
        }
       

    }

    IEnumerator SpawnWhiteCells()
    {
        while (true)
        {
            float offsetX = 0.0f;
            float offsetZ = 0.0f;
            float sp = 1.0f - _ts.GetSpawnRate();
            yield return new WaitForSeconds(sp * _wCellSpawnTMult);
            GameObject cp = ClosestSpawnPoint();
            BoxCollider bc = cp.GetComponent<BoxCollider>();
            offsetX = Random.Range(-(bc.bounds.extents.x - bc.bounds.extents.x / 0.8f), bc.bounds.extents.x - bc.bounds.extents.x / 0.8f);
            offsetZ = Random.Range(-(bc.bounds.extents.z - bc.bounds.extents.z / 0.8f), bc.bounds.extents.z - bc.bounds.extents.z / 0.8f);

            //offsetX = Random.Range((-bc.bounds.extents.x + 0.1f), (bc.bounds.extents.x - 0.1f));
            //offsetZ = Random.Range((-bc.bounds.extents.z + 0.1f), (bc.bounds.extents.z - 0.1f));
            Vector3 spawnPos = new Vector3(cp.transform.position.x + offsetX, cp.transform.position.y, cp.transform.position.z + offsetZ);
            GameObject clone = Instantiate(_whiteCellPrefab, spawnPos, Quaternion.identity) as GameObject;
            clone.transform.parent = _whiteCells;
            Debug.Log("spawn rate :  " + _ts.GetSpawnRate());
           

        }
    }


    IEnumerator SpawnRedCells()
    {
        while (true)
        {
            float offsetX = 0.0f;
            float offsetZ = 0.0f;
            GameObject cp = ClosestSpawnPoint();
            BoxCollider bc = cp.GetComponent<BoxCollider>();
            offsetX = Random.Range(-(bc.bounds.extents.x- bc.bounds.extents.x/0.8f), bc.bounds.extents.x - bc.bounds.extents.x / 0.8f);
            offsetZ = Random.Range(-(bc.bounds.extents.z - bc.bounds.extents.z / 0.8f), bc.bounds.extents.z - bc.bounds.extents.z / 0.8f);
            //offsetX = Random.Range((-bc.bounds.extents.x + 0.1f), (bc.bounds.extents.x - 0.1f));
            //offsetZ = Random.Range((-bc.bounds.extents.z + 0.1f), (bc.bounds.extents.z - 0.1f));

            Vector3 spawnPos = new Vector3(cp.transform.position.x + offsetX, cp.transform.position.y, cp.transform.position.z + offsetZ);
            GameObject clone = Instantiate(_redCellPrefab, spawnPos, Quaternion.identity) as GameObject;
            clone.transform.parent = _redCells;
            yield return new WaitForSeconds(_rCellSpawnDelay);

        }
    }

    public GameObject[] SpawnPoints
    {
        get
        {
            return _spawnPoints;
        }
    }
}
