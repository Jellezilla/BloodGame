using UnityEngine;
using System.Collections;


/// <summary>Main player script, controlls all interraction</summary>
public class Player : MonoBehaviour {

    private Rigidbody _rb;
    private PartsContainer _partsContainer;
	// Use this for initialization
	void Start () {

        _rb = GetComponent<Rigidbody>();
        _partsContainer = GetComponent<PartsContainer>();
	
	}

    /// <summary>Used to add force to the player</summary>
    void accel()
    {
        _rb.AddForce(Input.GetAxis("Vertical") * transform.forward * 4f, ForceMode.Acceleration);
    }

    /// <summary>Used to turn the player</summary>

    void turn()
    {
        transform.Rotate(0, Input.GetAxis("Horizontal") * 2.5f, 0);
    }


    // Update is called once per frame
    void Update () {

        turn();
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Parts launcher = _partsContainer.GetPart(PartType.Launcher);
            Debug.Log("HIT!");
            launcher.PartAction();
        }
	
	}

    void FixedUpdate()
    {
        accel();
    }
}
