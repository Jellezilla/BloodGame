using UnityEngine;
using System.Collections;

public class Rocket : MonoBehaviour
{

    private Rigidbody _rb;
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _explosionRadius;
    [SerializeField]
    private float _explosionForce;
    [SerializeField]
    private double _dmg;
    [SerializeField]
    private float _maxDistance;
    private Vector3 _spawnPosition;

    // Use this for initialization
    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _spawnPosition = transform.position;
    }

    public void OnCollisionEnter(Collision collision)
    {
        Rigidbody targetRB = null;
        CellBody target = null;
        Chasis player = null;
        if (collision.gameObject.tag != Tags.playerTag)
        {

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

                if (targetRB != null)
                {
                    targetRB.AddExplosionForce(_explosionForce, transform.position, _explosionRadius, 0, ForceMode.Impulse);
                    Debug.Log("EXPLODE");

                    if (target != null)
                    {
                        target.TakeDamage(_dmg);
                    }

                    if (player != null)
                    {
                        player.TakeDamage(_dmg);
                    }
                }


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

    public void Launch()
    {
        _rb.AddForce(transform.forward * _speed, ForceMode.Impulse);

    }

    void Update()
    {
        DistanceCheck();
    }


}
