using UnityEngine;
using System.Collections.Generic;

public class FlowArea : MonoBehaviour {

	public void Attract(Rigidbody bodyRB)
    {
		Vector3 fVectorUP = transform.forward;
        bodyRB.AddForce(fVectorUP, ForceMode.Force);
    }

}
