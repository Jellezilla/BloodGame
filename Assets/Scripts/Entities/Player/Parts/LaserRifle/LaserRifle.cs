using UnityEngine;
using System.Collections;
using System;

public class LaserRifle : Parts {

    [SerializeField]
    private float _rateOfFire;
    [SerializeField]
    private GameObject _laserProjectile;
    private GameObject _spawnPoint;
    private WaitForSeconds ws;
    private bool _fired;
    private bool _cd;
	// Use this for initialization
	void Awake ()
    {
        ws = new WaitForSeconds(1/_rateOfFire);
        _spawnPoint = transform.GetChild(0).gameObject;
	}

    IEnumerator Shoot()
    {
        _fired = true;
        Instantiate(_laserProjectile,_spawnPoint.transform.position,_spawnPoint.transform.rotation);
        yield return ws;
        _fired = false;
    }

    public override void PartAction()
    {
        if (!_fired)
        {
            StartCoroutine(Shoot());
        }
    }
}
