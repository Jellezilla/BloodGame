using UnityEngine;
using System.Collections;

public class ThreatSystem : MonoBehaviour {

    private float threatLevel = 1.0f;

    public float GetThreatLevel()
    {
        return threatLevel;
    }
    public void SetThreatLevel(float adj)
    {
        threatLevel += adj;
    }

    void Update()
    {
        if(threatLevel < 1.0f)
        {
            threatLevel = 1.0f;
        }
        if(threatLevel > 100.0f)
        {
            threatLevel = 100.0f;
        }
    }


    /// <summary>
    /// This method is used to simulate the threat system used in the prototype.
    /// </summary>
    void PrototypeThresholds()
    {
        
    }


}
