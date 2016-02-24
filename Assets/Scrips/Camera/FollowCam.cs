using UnityEngine;
using System.Collections;

public class FollowCam : MonoBehaviour {

    
    private Transform _player;
	// Use this for initialization
	void Awake () {

        _player = GameObject.FindGameObjectWithTag(Tags.Player.ToString()).transform;
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {


        transform.position = new Vector3(_player.position.x, transform.position.y, _player.position.z);
	
	}
}
