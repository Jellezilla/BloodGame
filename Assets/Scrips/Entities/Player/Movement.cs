using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

    private Rigidbody _rb;


	// Use this for initialization
	void Start () {

        _rb = GetComponent<Rigidbody>();

	
	}


    void accel()
    {
        _rb.AddForce(Input.GetAxis("Vertical") * transform.forward*4f,ForceMode.Acceleration);
    }

    void turn()
    {
        transform.Rotate(0, Input.GetAxis("Horizontal")*2.5f, 0);
    }
    // Update is called once per frame
    void Update()
    {
        turn();
    }
	void FixedUpdate () {

        accel();
	
	}
}
