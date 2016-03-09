using UnityEngine;
using System.Collections;


/// <summary>Main player script, controlls all interraction</summary>
public class Player : MonoBehaviour {

    private Rigidbody _rb;
    private PartsContainer _partsContainer;
    private bool _isPlayerHooked;
    private UtilityHookLauncher _launcher;
    private Chasis _chasis;
	// Use this for initialization
	void Start () {

        _rb = GetComponent<Rigidbody>();
        _partsContainer = GetComponent<PartsContainer>();
        GetParts();
	
	}

    void GetParts()
    {
        //Load Part refs
        _chasis = (Chasis)_partsContainer.GetPart(PartType.Chasis);
        _launcher = (UtilityHookLauncher)_partsContainer.GetPart(PartType.UtilityHookLauncher);
    }

    /// <summary>Used to make the player move.</summary>
    void Movement()
    {
        _rb.AddForce(Input.GetAxis("Vertical") * transform.forward * 4f, ForceMode.Acceleration);
        _rb.AddTorque(Input.GetAxis("Horizontal") * transform.up);
        //transform.Rotate(0, Input.GetAxis("Horizontal") * 2.5f, 0);
    }


    // Update is called once per frame
    void Update () {

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("HIT!");
            _launcher.PartAction();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Repair");
            _chasis.PartAction();
        }


        if (Input.GetKeyDown(KeyCode.E))
        {
            if (_launcher.CurrentHook != null)
            {
                if (_launcher.CurrentHook.HookTarget != null)
                {
                    Debug.Log(_launcher.CurrentHook.HookTarget);
                    _chasis.SyphonTarget(_launcher.CurrentHook.HookTarget);
                    Debug.Log("Syphon");
                }
            }
            
            
        }
	
	}

    void FixedUpdate()
    {
        Movement();
    }
}
