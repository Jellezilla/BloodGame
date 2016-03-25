using UnityEngine;
using System.Collections;

[RequireComponent(typeof(FlowObject), typeof(NavMeshAgent), typeof(Rigidbody))]
public class WhiteCellBehaviour : MonoBehaviour
{


    private NavMeshAgent _cellAgent;
    private Rigidbody _cellRB;
    private FlowObject _flowObject;
    private bool _agentDriven;
    [SerializeField]
    private float _aggroRange;
    private bool _reachedDest;
    [SerializeField]
    private double _wCellDmg;
    [SerializeField]
    private float _knockBack;
    private bool _hit;
    [SerializeField]
    private float _hitTreshhold;
    // Use this for initialization
    void Awake()
    {
        _cellAgent = GetComponent<NavMeshAgent>();
        _cellRB = GetComponent<Rigidbody>();
        _flowObject = GetComponent<FlowObject>();
        _cellAgent.enabled = false;
        _reachedDest = true;
    }

    // Update is called once per frame
    private void Update()
    {
        Aggro();
        BehaviourMode();
        Chase();
    }

    private void BehaviourMode()
    {
        if (!_agentDriven && _cellAgent.enabled)
        {
            _cellAgent.enabled = false;
            _cellRB.isKinematic = false;
        }
        else if (_agentDriven && !_cellAgent.enabled)
        {
            _cellAgent.enabled = true;
            _cellRB.isKinematic = true;
        }
    }


    private void Aggro()
    {
        if (Vector3.Distance(transform.position, GameController.Instance.Player.transform.position) <= _aggroRange)
        {
            _agentDriven = true;
        }
        else
        {
            _agentDriven = false;
        }
    }

    private void Chase()
    {
        if (_agentDriven && _cellAgent.enabled)
        {
            //  _cellAgent.velocity = _flowObject.CurrentFlowArea.AreaHeading;
            float distPlayer = Vector3.Distance(transform.position, GameController.Instance.Player.transform.position);

            if ((distPlayer > _cellAgent.stoppingDistance) && _reachedDest)
            {
                Debug.Log("CALL");
                _cellAgent.SetDestination(GameController.Instance.Player.transform.position);
                _reachedDest = false;
            }
            else if (_cellAgent.remainingDistance <= _cellAgent.stoppingDistance)
            {
                Debug.Log("UnCALL");
                _reachedDest = true;
            }

            Debug.Log(_cellAgent.velocity);

        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == Tags.playerTag && !_hit && _cellAgent.velocity.sqrMagnitude >= _hitTreshhold)
        {
            Player p = collision.gameObject.GetComponent<Player>();
            p.PlayerRB.AddForce(transform.forward * _knockBack,ForceMode.Impulse);
            p.Chasis.TakeDamage(_wCellDmg);
            _hit = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == Tags.playerTag && _hit && _cellAgent.velocity.sqrMagnitude < _hitTreshhold)
        {
            _hit = false;
        }
    }

}
