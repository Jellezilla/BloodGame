using UnityEngine;
using System.Collections.Generic;

public class FlowArea : MonoBehaviour {

  

	public void Attract(Transform body) {
		Vector3 fVectorUP = (transform.forward).normalized;
		body.GetComponent<Rigidbody>().AddForce(fVectorUP,ForceMode.Force);

	}

}
