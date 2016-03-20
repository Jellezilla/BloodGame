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
        if (other.gameObject.tag == Tags.flowAreaTag)
        {
            _currentFlowA = other.GetComponent<FlowArea>();
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
