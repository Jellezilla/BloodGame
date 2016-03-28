using UnityEngine;
using System.Collections;

public class WorldEventManager : MonoBehaviour {

    [SerializeField]
    private float timeStepValue;
    private bool _boss;
    [SerializeField]
    private int bossTimer;
    private WaitForSeconds _timeStep;

    public GameObject bossPrefab;

	// Use this for initialization
	void Awake () {

        _timeStep = new WaitForSeconds(timeStepValue);
        WorldEventController.Instance.SubscribeEvent(WorldEvents.Boss, SpawnBoss);
        StartCoroutine(WorldEventsRunnable());
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}



    IEnumerator WorldEventsRunnable()
    {
        float bossCounter = bossTimer;
        while (true)
        {
            if (bossCounter == 0 && !_boss)
            {
                bossCounter = bossTimer;
                WorldEventController.Instance.TriggerEvent(WorldEvents.Boss);
                _boss = true;
            }

            if (!_boss)
            {
                bossCounter--;
            }
            yield return _timeStep;
        }
    }

    private void SpawnBoss()
    {
        Vector3 pos;
        GameObject[] tmpSpawnPoints = GetComponent<CellSpawner>().SpawnPoints;
        pos = tmpSpawnPoints[Random.Range(0, tmpSpawnPoints.Length)].transform.position;

        Instantiate(bossPrefab, pos, Quaternion.identity);
        
           
    }






    #region Getter/Setter
    public bool Boss
    {
        set
        {
            _boss = value;
        }
        get
        {
            return _boss;
        }
    }
    #endregion
}
