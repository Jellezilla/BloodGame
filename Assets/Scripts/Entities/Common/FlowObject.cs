using UnityEngine;
using System.Collections;

public class FlowObject : MonoBehaviour
{
    private FlowArea _currentFlowA;
    private Rigidbody _bodyRB;

    void Awake()
    {
        _bodyRB = GetComponent<Rigidbody>();
    }


    void FixedUpdate()
    {
        WithinFlow();
    }

    #region Collision Handle

    public void OnTriggerEnter(Collider other)
    {
        _currentFlowA = other.GetComponent<FlowArea>();
    }

    public void OnTriggerExit(Collider other)
    {
        if (_currentFlowA != null && other.gameObject == _currentFlowA.gameObject)
        {
            _currentFlowA = null;
        }
    }
    #endregion

    #region Flow Methods

    void WithinFlow()
    {
        if (_currentFlowA != null)
        {
            _currentFlowA.Attract(_bodyRB);
        }
    }


    #endregion
}
