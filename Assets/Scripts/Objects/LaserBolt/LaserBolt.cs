using UnityEngine;

public class LaserBolt : MonoBehaviour
{

    private Rigidbody _rb;
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _maxDistance;
    [SerializeField]
    private double _damage;
    private Vector3 _spawnPosition;
    // Use this for initialization
    void Awake()
    {

        _spawnPosition = transform.position;
        _rb = GetComponent<Rigidbody>();
        Launch();

    }


    public void Launch()
    {
        _rb.AddForce(transform.forward * _speed,ForceMode.Impulse);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != Tags.playerTag && other.gameObject.tag != Tags.flowAreaTag)
        {
            if (other.gameObject.tag == Tags.whiteCellTag || other.gameObject.tag == Tags.redCellTag || other.gameObject.tag == Tags.virusCellTag)
            {
                CellBody cb = other.gameObject.GetComponent<CellBody>();
                cb.TakeDamage(_damage);
                Destroy(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    void DistanceCheck()
    {
        if (Vector3.Distance(_spawnPosition, transform.position) > _maxDistance)
        {

            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        DistanceCheck();
    }
}
