using UnityEngine;
using System.Collections;
using System;

public class Minelayer : Parts {

    [SerializeField]
    private int _nrOfSpawners;
    private GameObject[] _spawnPoints;
    [SerializeField]
    private GameObject _minePrefab;

 
    //Maybe add max mines ??
    // Use this for initialization
    void Awake () {

        _partName = "Minelayer";
        _partDescription = "basic mine layer";
        _partCost = 0;

        _spawnPoints = new GameObject[_nrOfSpawners];

        for (int i = 0; i < _nrOfSpawners; i++)
        {
            _spawnPoints[i] = transform.GetChild(i).gameObject;
        }

	}

    public override void PartAction()
    {
        for (int i = 0; i < _spawnPoints.Length; i++)
        {
            GameObject mine = (GameObject)Instantiate(_minePrefab, _spawnPoints[i].transform.position, _spawnPoints[i].transform.rotation);
            mine.GetComponent<Mine>().Launch();

        }

    }

    // Update is called once per frame
    void Update ()
    {

	}
}
