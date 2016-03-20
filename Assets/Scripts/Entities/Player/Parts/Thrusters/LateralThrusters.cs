using UnityEngine;
using System.Collections;
using System;

public class LateralThrusters : Parts {

    private Rigidbody _playerRB;
    [SerializeField]
    private float _speed = 4.0f;
    private string _axis;

    void Start()
    {
        GrabAxis();
    }


    void GrabAxis()
    {
        if (transform.forward == _playerRB.transform.forward || -_playerRB.transform.forward == transform.forward)
        {
            _axis = "Vertical";
        }
        else if (transform.forward == _playerRB.transform.right || -_playerRB.transform.right == transform.forward)
        {
            _axis = "Horizontal";
        }
    }

    public Rigidbody PlayerRB
    {
        set
        {
            _playerRB = value;
        }
    }
    
    public override void PartAction()
    {
        _playerRB.AddTorque(Input.GetAxis(_axis) * _speed * transform.up);
    }
}
