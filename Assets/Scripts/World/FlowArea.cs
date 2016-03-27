using UnityEngine;
using System.Collections.Generic;

public class FlowArea : MonoBehaviour {

    private Vector3 fVector;

    void Awake()
    {
        fVector = transform.forward;
    }


	public void Attract(Rigidbody bodyRB)
    {
        bodyRB.AddForce(fVector, ForceMode.Force);
    }

    public Vector3 AreaHeading
    {
        get
        {
            return fVector;
        }
    }

}
