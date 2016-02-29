using UnityEngine;
using System.Collections;

public class CellBody : MonoBehaviour {

    public double cellHealth;
    public double cellSynth;
	
	// Update is called once per frame
	void Update () {

        if (isDead())
        {
            Destroy(gameObject, 2.0f);
        }
	
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
