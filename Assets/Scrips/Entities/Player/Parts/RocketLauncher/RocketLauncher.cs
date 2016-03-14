using UnityEngine;
using System.Collections;
using System;

public class RocketLauncher : Parts {

    private GameObject _spawnPoint;
    [SerializeField]
    private GameObject _rocketPrefab;

    // Use this for initialization
    void Awake () {

        _partName = "Rocket Launcher";
        _partDescription = "basic rocket launcher";
        _partCost = 0;
        _spawnPoint = transform.GetChild(0).gameObject;

    }

    public override void PartAction()
    {
        GameObject rocket = (GameObject)Instantiate(_rocketPrefab, _spawnPoint.transform.position, _spawnPoint.transform.rotation);
        rocket.GetComponent<Rocket>().Launch();
        //rocket = null;
    }


    // Update is called once per frame
    void Update () {
	
	}
}
