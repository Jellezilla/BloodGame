using UnityEngine;



/// <summary>Launcher base class, handles launching the hook...further updates required.</summary>
public class Launcher : Parts {

    private Transform _launchPoint;
    public GameObject hookPrefab;
    private GrappleHook _hook;
    public bool _launchedHook;
	// Use this for initialization
	void Awake () {

        _partName = "Launcher";
        _partCost = 0;
        _partDescription = "Basic Launcher";
        _launchPoint = transform.GetChild(0).transform;
	
	}

    void Update()
    {
       // _launchPoint = transform.GetChild(0).transform.position;
    }
    /// <summary>Used to launch the hook from the launcher.</summary>
    public override void PartAction()
    {
        if (!_launchedHook)
        {
            Debug.Log("LAUNCHER OK");
            GameObject gh = (GameObject)Instantiate(hookPrefab, _launchPoint.position, _launchPoint.rotation);
            _hook = gh.GetComponent<GrappleHook>();
            _launchedHook = true;
            _hook.Launcher = this;
            _hook.Launch();
        }
        else
        {
            Debug.Log("LAUNCHER N_OK");
        }

    }


    public bool isHookLaunched
    {
        set
        {
            _launchedHook = value;
        }
    }

    public GrappleHook CurrentHook
    {
        get
        {
            return _hook;
        }
    }

}
