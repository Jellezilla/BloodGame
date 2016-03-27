using UnityEngine;
using System.Collections;

public class CellBody : MonoBehaviour {

    [SerializeField]
    private int _combatCD;
    [SerializeField]
    private double _cellHealth;
    [SerializeField]
    private double _cellSynth;
    private bool died;
    [SerializeField]
    private int _initAtkThreat;
    [SerializeField]
    private int _deathThreat;
    private WaitForSeconds _combatStep;
    private bool _inCombat;
    private int _cmbtCD;
    [SerializeField]
    private int _cellDecayTime;
    private Coroutine _combatState;

    void Start()
    {
        _cmbtCD = _combatCD;
        _combatStep = new WaitForSeconds(1);
        _combatState = StartCoroutine(CombatState());
    }
	// Update is called once per frame
	void Update () {


        Death();
	
	}

    /// <summary>
    /// Death state Function
    /// </summary>
    private void Death()
    {
        if (isDead() && !died)
        {
            died = true;
            GameController.Instance.ThreatSystem.IncreaseThreat(_deathThreat);
            StopCoroutine(_combatState);
            Destroy(gameObject, _cellDecayTime);

        }
    }
    /// <summary>
    /// Returns true if the cell is dead
    /// </summary>
    /// <returns></returns>
    public bool isDead()
    {
        if (_cellHealth <= 0 || _cellSynth <= 0)
        {
            if (_cellHealth < 0)
            {
                _cellHealth = 0;
            }

            if (_cellSynth < 0)
            {
                _cellSynth = 0;
            }
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Used to damage the cell.
    /// </summary>
    /// <param name="dmg"></param>
    public void TakeDamage(double dmg)
    {
        if (!_inCombat)
        {
            GameController.Instance.ThreatSystem.IncreaseThreat(_initAtkThreat);
        }

        if (_cellHealth > 0)
        {
            _inCombat = true;
            _cmbtCD = _combatCD;
            _cellHealth -= dmg;
        }
    }
    /// <summary>
    /// Used to syphon the cell for its resource value
    /// </summary>
    /// <param name="sDmg"></param>
    public void TakeSynthDamage(double sDmg)
    {

        if (!_inCombat)
        {
            GameController.Instance.ThreatSystem.IncreaseThreat(_initAtkThreat);
        }

        if (_cellSynth > 0)
        {
            _inCombat = true;
            _cmbtCD = _combatCD;
            _cellSynth -= sDmg;
        }
    }

    /// <summary>
    /// Combat state coroutine, starts with the start function
    /// </summary>
    /// <returns></returns>
    private IEnumerator CombatState()
    {
        while (true)
        {
            if (_cmbtCD > 0 && _inCombat)
            {
                _cmbtCD--;
            }
            else
            {
                _inCombat = false;
                _cmbtCD = _combatCD; 
            }

            yield return _combatStep;
        }
    }
}
