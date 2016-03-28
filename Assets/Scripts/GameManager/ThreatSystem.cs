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
    private int _cooldown;
    private int _iCooldown;
    [SerializeField]
    private float _cThreat;
    private WaitForSeconds _threatDecayStep;
    private WaitForSeconds _coolDownStep;
    private bool _onCoolDown;
    private float _cd;
    private Coroutine _threat;
    private Coroutine _threatCoolDown;

    void Awake()
    {
        _iCooldown = _cooldown;
        _coolDownStep = new WaitForSeconds(1);
        _threatDecayStep = new WaitForSeconds(_threatDecayRate);
        _threatCoolDown = StartCoroutine(CoolDown());
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

    /// <summary>
    /// Increase threat by specified value, current threat decay cooldown will be reset.
    /// </summary>
    /// <param name="adj"></param>
    public void IncreaseThreat(int adj)
    {
        _onCoolDown = true;
        _iCooldown = _cooldown;
        if (_cThreat < _maxThreat)
        {
            if (_cThreat + adj < _maxThreat)
            {
                _cThreat += adj;
            }
        }

    }
    private IEnumerator ThreatDecay()
    {
        while (true)
        {
            if (!_onCoolDown)
            {

                if (_cThreat > 0)
                {
                    _cThreat -= _threatDecayValue;
                }
            }

            yield return _threatDecayStep;
            Debug.Log(_cThreat);
        }


    }
    /// <summary>
    /// Cooldown method
    /// </summary>
    /// <returns></returns>
    private IEnumerator CoolDown()
    {
        while (true)
        {
            if (_iCooldown > 0 && _onCoolDown)
            {
                _iCooldown--;
            }
            else
            {
                _onCoolDown = false;
                _iCooldown = _cooldown;
            }

            yield return _coolDownStep;
        }
    }

    public void OnDestroy()
    {
        StopCoroutine(_threat);
        StopCoroutine(_threatCoolDown);
    }

}
