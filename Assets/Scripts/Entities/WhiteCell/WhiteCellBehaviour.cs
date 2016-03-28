using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CellBody), typeof(NavMeshAgent), typeof(Rigidbody))]
public class WhiteCellBehaviour : MonoBehaviour
{

    private NavMeshAgent _cellAgent;
    private Rigidbody _cellRB;
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
    private CellBody _cellBody;

    // Use this for initialization
    void Awake()
    {
        _cellAgent = GetComponent<NavMeshAgent>();
        _cellRB = GetComponent<Rigidbody>();
        _cellBody = GetComponent<CellBody>();
        _cellAgent.enabled = false;
        _reachedDest = true;
    }

    // Update is called once per frame
    private void Update()
    {
        BehaviourMode();
        Behaviour();

    }

    private void BehaviourMode()
    {
        if (!_agentDriven && _cellAgent.enabled)
        {
            _cellRB.isKinematic = false;
            _cellRB.velocity = _cellAgent.velocity;
            _cellAgent.enabled = false;
        }
        else if (_agentDriven && !_cellAgent.enabled)
        {
            _cellAgent.enabled = true;
            _cellAgent.velocity = _cellRB.velocity;
            _cellRB.isKinematic = true;
        }
    }
    private void Behaviour()
    {
        if (_cellBody.isDead() == false)
        {
            Aggro();
            Chase();
        }
        else
        {
            _agentDriven = false;
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
