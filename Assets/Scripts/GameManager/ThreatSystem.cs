using UnityEngine;
using System.Collections;

public class ThreatSystem : MonoBehaviour
{

    [SerializeField]
    private float _maxThreat;
    [SerializeField]
    private float _threatDecayRate;
    [SerializeField]
    private float _threatDecayValue;
    [SerializeField]
    private float _cooldown;
    private float _cThreat;
    private WaitForSeconds _threatDecay;
    private bool _onCoolDown;
    private bool _cdStarted;
    private float _cd;
    private Coroutine _threat;


    void Awake()
    {
        
        _threatDecay = new WaitForSeconds(_threatDecayValue);
      //  _threat = StartCoroutine(ThreatDecay());
    }

    public float GetThreatLevel()
    {
        return _cThreat;
    }

    /// <summary>
    /// Returns the normalized value of the threat.
    /// </summary>
    /// <returns></returns>
    public float GetSpawnRate()
    {
        return (_cThreat - Mathf.Min(0, _maxThreat)) / (Mathf.Max(0, _maxThreat) - Mathf.Min(0, _maxThreat));
    }
    public void IncreaseThreat(int adj)
    {
        
        _onCoolDown = false;
        if (_cThreat < _maxThreat)
        {
            if (_cThreat + adj < _maxThreat)
            {
                _cThreat += adj;
            } else
            {
                _cThreat = _maxThreat;
            }
        }
        StartCoroutine(CooldownDelay());
    }
    /*
    private bool CoolDown()
    {

        Debug.Log("Cooldown method!");
        if (!_onCoolDown)
        {
            _cd = _cooldown;
            _onCoolDown = true;
        }

     
        if (_cd > 0.0f)
        {
            _cd -= Time.deltaTime;
            return true;
        }
        else
        {
            if (_cd < 0)
            {
                _cd = 0;
            }
            _onCoolDown = true;
            return false;
        }
    }*/
    private IEnumerator ThreatDecay()
    {
        //while (CoolDown())
        while (_cThreat > 0.0f  && !_onCoolDown)
        {
            yield return _threatDecayRate;
            if (_cThreat > 0)
            {
                _cThreat -= _threatDecayValue;
            }
        }
    }

    IEnumerator CooldownDelay()
    {
        _onCoolDown = true;
        yield return new WaitForSeconds(5.0f);
        _onCoolDown = false;
        StartCoroutine(ThreatDecay());
    }
        /// <summary>
        /// Added for testing the threat level and spawn rate of white blood cells. To be removed as soon as it is satisfactory. 
        /// </summary>
    void OnGUI()
    {
        string output = "Current Threat Level: " + GetThreatLevel().ToString();
        GUI.Label(new Rect(10, 10, 200, 25), output);
    }

}
