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
    private List<LaserRifle> _laserRifles;
    [SerializeField]
    private Chasis _chasis;
    private Rigidbody _playerRB;



	// Use this for initialization
	void Start () {

        _playerRB = GetComponent<Rigidbody>();
        _launcher = new List<UtilityHookLauncher>(0);
        _mainThrusters = new List<MainThrusters>(0);
        _lateralThrusters = new List<LateralThrusters>(0);
        _minelayers = new List<Minelayer>(0);
        _rocketLaunchers = new List<RocketLauncher>(0);
        _laserRifles = new List<LaserRifle>(0);
        UpdateParts();

    }
    public void UpdateParts()
    {
        //Init PartLists
        _launcher.RemoveRange(0, _launcher.Count);
        _mainThrusters.RemoveRange(0, _mainThrusters.Count);
        _lateralThrusters.RemoveRange(0, _lateralThrusters.Count);
        _minelayers.RemoveRange(0, _minelayers.Count);
        _rocketLaunchers.RemoveRange(0, _rocketLaunchers.Count);
        _laserRifles.RemoveRange(0, _laserRifles.Count);
        //Load Part 
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

        parts = _chasis.GetAttachedParts(typeof(LaserRifle));
        if (parts != null)
        {
            for (int i = 0; i < parts.Count; i++)
            {
                _laserRifles.Add((LaserRifle)parts[i]);
            }
        }

    }


    /// <summary>Used to make the player move.</summary>
    void Movement()
    {
        if(_mainThrusters.Count>0)
        for (int i = 0; i < _mainThrusters.Count; i++)
        {
            _mainThrusters[i].PartAction();
        }

        for (int i = 0; i < _lateralThrusters.Count; i++)
        {
            _lateralThrusters[i].PartAction();
        }

    }

    //Input Hook For running parts
    void Parts()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
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

            if (_laserRifles.Count > 0)
            {
                for (int i = 0; i < _laserRifles.Count; i++)
                {
                    _laserRifles[i].PartAction();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.R) && _chasis != null)
        {
            _chasis.PartAction();
        }
    }

    // Update is called once per frame
    void Update () {


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
                    }
                }
            }

            
            
        }
	
	}

    void FixedUpdate()
    {
        if (!_chasis.Dead)
        {
            Movement();
            Parts();
        }

    }


    /// <summary>
    /// Add Chasis to player
    /// </summary>
    /// <param name="chasisPrefab">Chasis GameObject Prefab</param>
    void RegisterChasis(GameObject chasisPrefab)
    {
        GameObject chasis = (GameObject)Instantiate(chasisPrefab, transform.position, transform.rotation);
        chasis.transform.SetParent(transform);
        _chasis = chasis.GetComponent<Chasis>();
    }

    /// <summary>
    /// Remove chasis and all parts from player
    /// </summary>
    void UnRegisterChasis()
    {
        if (transform.childCount > 0)
        {
            Destroy(transform.GetChild(transform.childCount - 1));
        }
    }
    #region Setter/Getter
    public Chasis Chasis
    {
        get
        {
            return _chasis;
        }
    }

    public Rigidbody PlayerRB
    {
        get
        {
            return _playerRB;
        }
    }
    #endregion
}
