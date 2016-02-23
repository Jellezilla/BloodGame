using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody))]
public class FlowObject : MonoBehaviour
{

    public GameObject atractorContainer;
    private List<FlowPoint> _attractors;
    private FlowPoint _attractor;
    private Transform _myTransform;
    private Vector3 _currentFpPos;
    private int _attractorIterator = 0;

    void Start()
    {
        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        _attractors = new List<FlowPoint>();
        getAtractors();
        _myTransform = transform;
        _attractor = _attractors[_attractorIterator];
        _currentFpPos = _attractor.ReturnRandomfpPos();
    }


    void getAtractors()
    {
        for (int i = 0; i < atractorContainer.transform.childCount; i++)
        {
            _attractors.Add(atractorContainer.transform.GetChild(i).GetComponent<FlowPoint>());
        }
    }

    void OnTriggerEnter(Collider col)
    {

        if (_attractorIterator == _attractors.Count - 1)
        {
            _attractorIterator = 0;
            _attractor = _attractors[_attractorIterator];
            _currentFpPos = _attractor.ReturnRandomfpPos();

        }
        else
        {
            _attractorIterator++;
            _attractor = _attractors[_attractorIterator];
            _currentFpPos = _attractor.ReturnRandomfpPos();

        }
    }

    void FixedUpdate()
    {
        if (_attractor)
        {
            _attractor.Attract(_myTransform,_currentFpPos);
        }
    }

}
