using UnityEngine;
using System.Collections;

public class CellBody : MonoBehaviour {

    public double cellHealth;
    public double cellSynth;
    private ThreatSystem ts;

    [SerializeField]
    private int threatValue;

    private bool amIDeadYet = false;

    void Start()
    {
        ts = GameObject.FindWithTag("GameController").GetComponent<ThreatSystem>();
    }

	// Update is called once per frame
	void Update () {

        if (isDead() && !amIDeadYet)
        {
            CellDead();
        }
	}
    /// <summary>
    ///  This method is called only once, when the cell dies. 
    /// </summary>
    private void CellDead()
    {
        amIDeadYet = true;
        ts.IncreaseThreat(threatValue);
        Destroy(gameObject, 1.0f);
    }

    public bool isDead()
    {
        if (cellHealth <= 0 || cellSynth <= 0)
        {
            if (cellHealth < 0)
            {
                cellHealth = 0;
            }

            if (cellSynth < 0)
            {
                cellSynth = 0;
            }
            return true;
        }
        else
        {
            return false;
        }
    }

    public void TakeDamage(double dmg)
    {
        if (cellHealth > 0)
        {
            cellHealth -= dmg;
        }
    }

    public void TakeSynthDamage(double sDmg)
    {
        if (cellSynth > 0)
        {
            cellSynth -= sDmg;
        }
    }
}
