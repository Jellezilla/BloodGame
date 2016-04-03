using UnityEngine;
using System.Collections;

[RequireComponent(typeof(VehicleUserControl))]
public class VehicleTurret : MonoBehaviour {

    [SerializeField]
    private GameObject m_VehicleTurret;
    [SerializeField]
    private float spawnPointOffset;
    private Vector3 m_spawnPoint;
    private float m_OldRotation;
    [Range(0,1)]
    [SerializeField]
    private float m_rotationAsist;
	// Use this for initialization
	void Start () {



    }


    private void CalculateSpawnPoint()
    {

    }

    void Update()
    {
        Aim();
    }
    public void Aim()
    {
        m_VehicleTurret.transform.LookAt(new Vector3(Input.mousePosition.x,0, Input.mousePosition.z));
    }

    public void Fire()
    {

    }
	
}
