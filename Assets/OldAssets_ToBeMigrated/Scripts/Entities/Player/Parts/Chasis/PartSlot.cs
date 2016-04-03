using UnityEngine;
using System.Collections;

/// <summary>
/// Stores the current part attached to the slot
/// </summary>
public class PartSlot : MonoBehaviour {

    private Parts _part;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    /// <summary>
    /// Add parts to the player.
    /// </summary>
    /// <param name="partPrefab"></param>
    public void addPart(GameObject partPrefab)
    {
        GameObject part = (GameObject)Instantiate(partPrefab, transform.position, transform.rotation);
        part.transform.SetParent(transform);
        _part = part.GetComponent<Parts>();
        part = null;
    }


    public void RemovePart() 
    {
        if (_part != null) 
        {
            _part = null;
            Destroy(transform.GetChild(0).gameObject);
        }

    }
    /// <summary>
    /// Add parts to the player. Used for parts that require a rigidbody reference.
    /// </summary>
    /// <param name="partPrefab"></param>
    /// <param name="rb"></param>
    public void addPart(GameObject partPrefab, Rigidbody rb)
    {
        GameObject part = (GameObject)Instantiate(partPrefab, transform.position, transform.rotation);
        part.transform.SetParent(transform);
        _part = part.GetComponent<Parts>();
        if (_part.GetType() == typeof(MainThrusters))
        {
            ((MainThrusters)_part).PlayerRB = rb;
        }
        else if (_part.GetType() == typeof(LateralThrusters))
        {
            ((LateralThrusters)_part).PlayerRB = rb;
        }
        part = null;
    }


    public Parts GetPart()
    {
        return _part;
    }

}
