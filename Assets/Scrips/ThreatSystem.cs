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
        _threat = StartCoroutine(ThreatDecay());
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
            }
        }

    }
    private bool CoolDown()
    {
        if (!_onCoolDown)
        {
            _onCoolDown = true;
            _cd = _cooldown;
        }
        else
        {
            _cd -= Time.deltaTime;
        }

        if (_cd > 0.0f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private IEnumerator ThreatDecay()
    {
        while (CoolDown())
        {
            yield return _threatDecayRate;
            if (_cThreat > 0)
            {
                _cThreat -= _threatDecayValue;
            }
        }
    }

}
