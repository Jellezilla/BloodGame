using UnityEngine;
using System.Collections;

/// <summary>Grappling hook class, handles grapling hook logic</summary>
public class GrappleHook : MonoBehaviour
{
    public float speed;
    public int maxDistance;
    private Rigidbody _rb;
    private Vector3 _spawnPosition;
    private Rigidbody _playerRB;
    private SpringJoint _spring;
    // Use this for initialization
    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _spawnPosition = transform.position;
        _playerRB = GameObject.FindWithTag(Tags.Player.ToString()).GetComponent<Rigidbody>();
        //grab original Position
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != Tags.Player.ToString() && collision.gameObject.tag != Tags.RedCell.ToString()
            && collision.gameObject.tag != Tags.WhiteCell.ToString() && collision.gameObject.tag != Tags.VirusCell.ToString())
        {
            Debug.Log("Hook HIT"); // if you hit a wall or something that you can attach to set to kinematic
            _rb.isKinematic = true;
            _spring = gameObject.AddComponent<SpringJoint>();
            _spring.connectedBody = _playerRB;
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
        if (Vector3.Distance(_spawnPosition, transform.position) > maxDistance)
        {
           
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //destroy test hook
            Destroy(gameObject);
        }
        DistanceCheck();
    }
}
