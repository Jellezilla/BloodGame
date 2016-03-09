using UnityEngine;
using System.Collections;

/// <summary>Grappling hook class, handles grapling hook logic</summary>
public class GrappleHook : MonoBehaviour
{
    public float speed;
    public float maxDistance;
    private Rigidbody _rb;
    private Vector3 _spawnPosition;
    private Rigidbody _playerRB;
    private UtilityHookLauncher _launcher;
    private bool _isCell;
    private bool _hasTarget;
    private SpringJoint _spring;
    private GameObject _cTarget;
    private CellBody _cb;
    // Use this for initialization
    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _spawnPosition = transform.position;
        _playerRB = GameObject.FindWithTag(Tags.playerTag).GetComponent<Rigidbody>();
        //grab original Position
    }


    public UtilityHookLauncher Launcher
    {
        set
        {
            _launcher = value;
        }
    }

    public CellBody HookTarget
    {
        get
        {
            return _cb;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != Tags.playerTag)
        {
            _hasTarget = true;
            Debug.Log("Hook HIT"); // if you hit a wall or something that you can attach to
            if (collision.gameObject.tag == Tags.wallTag)
            {
                _cTarget = collision.gameObject;
                _rb.isKinematic = true;
                _spring = gameObject.AddComponent<SpringJoint>();
                _spring.axis = transform.forward;
                _spring.minDistance = 0.05f;
                _spring.maxDistance = 0.10f;
                _spring.connectedBody = _playerRB;
               
                
            }
            else if(collision.gameObject.tag == Tags.redCellTag || collision.gameObject.tag == Tags.whiteCellTag || collision.gameObject.tag == Tags.virusCellTag)
            {
                _isCell = true;
                _cTarget = collision.gameObject;
                _cb = _cTarget.GetComponent<CellBody>();
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.forward, out hit))
                {
                    gameObject.transform.position = hit.point;
                    Destroy(_rb);
                    gameObject.transform.SetParent(collision.gameObject.transform);
                    _spring = _cTarget.gameObject.AddComponent<SpringJoint>();
                    _spring.axis = transform.forward;
                    _spring.minDistance = 0.05f;
                    _spring.maxDistance = 0.10f;
                    _spring.connectedBody = _playerRB;

                }
               
            }
            

        }

    }
    /// <summary>Used to launch the hook.</summary>
    /// <param name="player">The player rigidbody component must be passed.</param>
    public void Launch()
    {
        _rb.AddForce(transform.forward * speed, ForceMode.Impulse);
    }


    //check if the hook has reached max distance
    void DistanceCheck()
    {
        if (Vector3.Distance(_spawnPosition, transform.position) > maxDistance && !_isCell)
        {
            _launcher.isHookLaunched = false;
            Destroy(gameObject);
        }
    }

    void Update()
    {
        DistanceCheck();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //destroy test hook
            Destroy(_spring);
            _launcher.isHookLaunched = false;
            Destroy(gameObject);
        }

        if (_hasTarget && _cb!=null)
        {
            if (_cb.isDead())
            {
                Destroy(_spring);
                _launcher.isHookLaunched = false;
                Destroy(gameObject);
            }
        }

    }
}
