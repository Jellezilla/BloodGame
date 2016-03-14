using UnityEngine;
using System.Collections;

public class Mine : MonoBehaviour
{

    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _maxDistance;
    [SerializeField]
    private float _mineTTL;
    [SerializeField]
    private float _activationTime;
    [SerializeField]
    private double _mineDamage;
    [SerializeField]
    private float _explosionForce;
    [SerializeField]
    private float _explosionRadius;

    private Rigidbody _rb;
    private Vector3 _spawnPosition;
    private bool _active;
    private bool _detonating;
    // Use this for initialization
    void Awake()
    {

        _rb = GetComponent<Rigidbody>();
        _spawnPosition = GameObject.FindGameObjectWithTag(Tags.playerTag).transform.position;


    }



    void Activation()
    {
        if (_activationTime > 0)
        {
            _activationTime -= Time.deltaTime;

            if (_activationTime <= 0)
            {
                _active = true;
                _rb.isKinematic = true;
                Debug.Log("ACTIVATED");
            }
        }
    }

    public void Launch()
    {
        _rb.AddForce(transform.forward * _speed, ForceMode.Force);
    }


    void DistanceCheck()
    {
        if (Vector3.Distance(_spawnPosition, transform.position) > _maxDistance)
        {

            Destroy(gameObject);
        }
    }


    public void OnCollisionEnter(Collision collision)
    {
        if (_active)
        {
            Detonate();
        }

    }


    void Detonate()
    {
        Rigidbody targetRB = null;
        CellBody target = null;
        Chasis player = null;
        if (!_detonating)
        {
            _detonating = true;
            Collider[] targets = Physics.OverlapSphere(transform.position, _explosionRadius);

            for (int i = 0; i < targets.Length; i++)
            {

                if (targets[i].gameObject.tag == Tags.playerTag)
                {
                    player = targets[i].GetComponent<Chasis>();
                }
                else
                {
                    target = targets[i].GetComponent<CellBody>();
                }


                targetRB = targets[i].GetComponent<Rigidbody>();

                if (targetRB != null && targetRB != _rb)
                {
                    targetRB.AddExplosionForce(_explosionForce, transform.position, _explosionRadius,0,ForceMode.Impulse);

                    if (target != null)
                    {
                        target.TakeDamage(_mineDamage);
                    }
                    else if (player != null)
                    {
                        player.TakeDamage(_mineDamage);
                        Debug.Log(player.gameObject.name);
                    }
                }
            }

            Destroy(gameObject);
        }
        
        
        


    }

    // Update is called once per frame
    void Update()
    {

        Activation();
        DistanceCheck();

    }
}
