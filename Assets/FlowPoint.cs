using UnityEngine;
using System.Collections.Generic;

public class FlowPoint : MonoBehaviour {

    private int _id;
    [SerializeField]
	private float _flowpower = -5;
    [SerializeField]
    private float _pointSize;
    [SerializeField]
    private int _numOfPoints;

    private Node x;

    private List<Vector3> _pPositions;

    void Awake()
    {
        GeneratePoints();
    }


    void GeneratePoints()
    {
        _pPositions = new List<Vector3>();
        for (int i = 0; i < _numOfPoints; i++)
        {
            Vector3 v = Random.insideUnitSphere * _pointSize;
            v.y = 0;
            _pPositions.Add(Random.insideUnitSphere*_pointSize);
        }
    }



    public Vector3 ReturnRandomfpPos()
    {
        return _pPositions[Random.Range(0, _pPositions.Count)];
    }


	public void Attract(Transform body, Vector3 fpPos) {
		Vector3 fVectorUP = (body.position - transform.position+fpPos).normalized;
		body.GetComponent<Rigidbody>().AddForce(fVectorUP * _flowpower,ForceMode.Force);

	}


    #region
    public int ID
    {
        get
        {
            return _id;
        }

    }
    #endregion

}
