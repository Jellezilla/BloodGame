using UnityEngine;
using System.Collections;
using System;

public class MainThrusters : Parts {

    private Rigidbody _playerRB;
    [SerializeField]
    private float _speed = 4;
    private string _axis;
    private Transform _parent;


    void Start()
    {
        _parent = transform.parent;
        GrabAxis();
    }


    public Rigidbody PlayerRB
    {
        set
        {
            _playerRB = value;
        }
    }

    void GrabAxis()
    {
        if (_parent.forward == _playerRB.transform.forward || -_playerRB.transform.forward == _parent.forward)
        {
            _axis = "Vertical";
        }
        else if (_parent.forward == _playerRB.transform.right || -_playerRB.transform.right == _parent.forward)
        {
            _axis = "Horizontal";
        }
    }

    public override void PartAction()
    {
        if (Input.GetAxis(_axis) > 0)
        {
            Vector3 heading = (_playerRB.transform.position-transform.position).normalized;
            _playerRB.AddForce(heading * Input.GetAxis(_axis) * _speed, ForceMode.Acceleration);
        }
    }

}
