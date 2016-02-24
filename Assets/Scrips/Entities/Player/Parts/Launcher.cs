using UnityEngine;



/// <summary>Launcher base class, handles launching the hook...further updates required.</summary>
public class Launcher : Parts {

    private Transform _launchPoint;
    public GameObject hookPrefab;
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
        Debug.Log("LAUNCHER OK");
        GameObject hook = (GameObject)Instantiate(hookPrefab, _launchPoint.position, _launchPoint.rotation);
        Debug.Log(hook);
        hook.GetComponent<GrappleHook>().Launch();
    }


}
