using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

    private GameObject player;
    private Rigidbody rigid;


    private float aggroRange = 5.0f;
    private float attackRange = 0.5f;

	// Use this for initialization
	void Start ()
    {
        player = GameObject.FindWithTag("Player");
        rigid = GetComponent<Rigidbody>();




	}
	
	
    
    

    void FixedUpdate()
    {
        FSM();

    }


    void FSM()
    {
        if(Vector3.Distance(player.transform.position, transform.position) < aggroRange) {
            Chase();
        } else if(Vector3.Distance(player.transform.position, transform.position) < attackRange)  {
            Attack();
        } else {
            return;
        }
    }

    void Chase()
    {
        rigid.AddForce(Input.GetAxis("Vertical") * transform.forward * 4f, ForceMode.Acceleration);
        transform.LookAt(player.transform);
    }

    void Attack()
    {

    }

}


