using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>Main player script, controlls all interraction</summary>
public class Player : MonoBehaviour {

    private List<UtilityHookLauncher> _launcher;
    private List<MainThrusters> _mainThrusters;
    private List<LateralThrusters> _lateralThrusters;
    private List<Minelayer> _minelayers;
    private List<RocketLauncher> _rocketLaunchers;
    private Chasis _chasis;
	// Use this for initialization
	void Start () {

        _chasis = GetComponent<Chasis>();
        _launcher = new List<UtilityHookLauncher>(0);
        _mainThrusters = new List<MainThrusters>(0);
        _lateralThrusters = new List<LateralThrusters>(0);
        _minelayers = new List<Minelayer>();
        _rocketLaunchers = new List<RocketLauncher>();
        UpdateParts();
	
	}

    void UpdateParts()
    {
        //Load Part 

        Debug.Log(_chasis.gameObject);
        List<Parts> parts = _chasis.GetAttachedParts(typeof(UtilityHookLauncher));
        if (parts!=null)
        {
            for (int i = 0; i < parts.Count; i++)
            {
                _launcher.Add((UtilityHookLauncher)parts[i]);
            }
        }

        parts = _chasis.GetAttachedParts(typeof(MainThrusters));
        if (parts != null)
        {
            for (int i = 0; i < parts.Count; i++)
            {
                _mainThrusters.Add((MainThrusters)parts[i]);
            }
        }

        parts = _chasis.GetAttachedParts(typeof(LateralThrusters));
        if (parts != null)
        {
            for (int i = 0; i < parts.Count; i++)
            {
                _lateralThrusters.Add((LateralThrusters)parts[i]);
            }
        }

        parts = _chasis.GetAttachedParts(typeof(Minelayer));
        if (parts != null)
        {
            for (int i = 0; i < parts.Count; i++)
            {
                _minelayers.Add((Minelayer)parts[i]);
            }
        }

        parts = _chasis.GetAttachedParts(typeof(RocketLauncher));
        if (parts != null)
        {
            for (int i = 0; i < parts.Count; i++)
            {
                _rocketLaunchers.Add((RocketLauncher)parts[i]);
            }
        }

    }

    /// <summary>Used to make the player move.</summary>
    void Movement()
    {
        for (int i = 0; i < _mainThrusters.Count; i++)
        {
            _mainThrusters[i].PartAction();
        }

        for (int i = 0; i < _lateralThrusters.Count; i++)
        {
            _lateralThrusters[i].PartAction();
        }

    }


    // Update is called once per frame
    void Update () {

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("HIT!");
            if (_launcher.Count > 0)
            {
                for (int i = 0; i < _launcher.Count; i++)
                {
                    _launcher[i].PartAction();
                }
            }

            if (_minelayers.Count > 0)
            {
                for (int i = 0; i < _minelayers.Count; i++)
                {
                    _minelayers[i].PartAction();
                }
            }

            if (_rocketLaunchers.Count > 0)
            {
                for (int i = 0; i < _rocketLaunchers.Count; i++)
                {
                    _rocketLaunchers[i].PartAction();
                }
            }


        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Repair");
            _chasis.PartAction();
        }


        if (Input.GetKeyDown(KeyCode.E))
        {
            for (int i = 0; i < _launcher.Count; i++)
            {
                if (_launcher[i].CurrentHook != null)
                {
                    if (_launcher[i].CurrentHook.HookTarget != null)
                    {
                        Debug.Log(_launcher[i].CurrentHook.HookTarget);
                        _chasis.SyphonTarget(_launcher[i].CurrentHook.HookTarget);
                        Debug.Log("Syphon");
                    }
                }
            }

            
            
        }
	
	}

    void FixedUpdate()
    {
        Movement();
    }
}
