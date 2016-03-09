using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>Main player script, controlls all interraction</summary>
public class Player : MonoBehaviour {

    private List<UtilityHookLauncher> _launcher;
    private List<MainThrusters> _mainThrusters;
    private List<LateralThrusters> _lateralThrusters;
    private Chasis _chasis;
	// Use this for initialization
	void Start () {

        _chasis = GetComponent<Chasis>();
        _launcher = new List<UtilityHookLauncher>(0);
        _mainThrusters = new List<MainThrusters>(0);
        _lateralThrusters = new List<LateralThrusters>(0);
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

        //Debug.Log(_launcher.gameObject);
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
        
       // _rb.AddForce(Input.GetAxis("Vertical") * transform.forward * 4f, ForceMode.Acceleration);
        //_rb.AddTorque(Input.GetAxis("Horizontal") * transform.up);
        //transform.Rotate(0, Input.GetAxis("Horizontal") * 2.5f, 0);
    }


    // Update is called once per frame
    void Update () {

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("HIT!");
            for (int i = 0; i < _launcher.Count; i++)
            {
                _launcher[i].PartAction();
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
