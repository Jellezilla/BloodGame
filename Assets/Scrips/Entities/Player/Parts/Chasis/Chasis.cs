using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// Resources class - needs rehaul for each part
/// </summary>
public class Chasis : Parts {

    [SerializeField]
    private int _maxDurability;
    [SerializeField]
    private int _durability;
    [SerializeField]
    private double _synthesis;
    private WaitForSeconds _waitforS;
    private bool _selfRepairOn;
    private bool _syphoning;
    Coroutine syphon;
    private List<PartSlot> _slots;
    private Rigidbody _playerRB;
    public GameObject launcherPrefab;
    public GameObject mainThrusterPrefab;
    public GameObject lateralThrusterPrefab;

    //current Chasis properties -- test stuff
    int repdurab = 50;
    int durabPerS = 10;
    int rCost = 5;


    int syphVal = 60;
    int syphPerS = 15;


    void Awake()
    {
        _playerRB = GetComponent<Rigidbody>();
        _slots = new List<PartSlot>();
        _waitforS = new WaitForSeconds(1);
        GetSlots();
        setupBOT();
    }


    void GetSlots()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            _slots.Add(transform.GetChild(i).gameObject.GetComponent<PartSlot>());
        }
    }


    //TEST FUNCTION
    void setupBOT()
    {
        // 0 is front , 1 is back // 2 slot left // 3 slot right
        _slots[0].addPart(launcherPrefab);
        //_slots[1].addPart(launcherPrefab);
        //_slots[2].addPart(launcherPrefab);
        //_slots[3].addPart(launcherPrefab);
        _slots[1].addPart(mainThrusterPrefab,_playerRB);
        _slots[2].addPart(lateralThrusterPrefab, _playerRB);
        _slots[3].addPart(lateralThrusterPrefab, _playerRB);
        
    }
    //END OF TEST FUNCTION
    public List<Parts> GetAttachedParts(Type partType)
    {
        List<Parts> foundParts = new List<Parts>(0);
        for (int i = 0; i < _slots.Count; i++)
        {
            if (_slots[i].GetPart() != null)
            {
                Debug.Log("part");
                if (_slots[i].GetPart().GetType() == partType)
                {
                    foundParts.Add(_slots[i].GetPart());
                    Debug.Log(_slots[i].GetPart());
                }
            }

        }

        if (foundParts.Count > 0)
        {
            return foundParts;
        }
        else
        {
            foundParts = null;
            return null;
        }
        
    }

    void Update()
    {
        Debug.Log(_synthesis);
    }

    public void SyphonTarget(CellBody target)
    {
        if (!_syphoning)
        {
            _syphoning = true;
           syphon = StartCoroutine(SyphonResources(syphVal, syphPerS, 1, target));
        }

    }

    public void StopSyphon()
    {
        StopCoroutine(syphon);
        _syphoning = false;
    }
    //Repair enumerator function ---- note : update to repair per part
    IEnumerator Repair(int durab, int perSecond)
    {

        int passedDurab = durab;

        while (passedDurab > 0)
        {
            yield return _waitforS;
            if (passedDurab >= perSecond)
            {
                _durability += perSecond;
                passedDurab -= perSecond;
            }
            else if (passedDurab < perSecond)
            {
                passedDurab -= perSecond + (passedDurab - perSecond);
                _durability += perSecond + (passedDurab - perSecond);
            }

        }

        _selfRepairOn = false;
    }

    //Syphon function ---- needs special syphonable objects class
    IEnumerator SyphonResources(int synthesis, int sPerSecond, double rarityMod, CellBody syphTarget)
    {
        double tSynth = synthesis * rarityMod;
        double tPerSec = sPerSecond * rarityMod;
        //initial threat --- assaulting the cell

        while (tSynth > 0)
        {
            yield return _waitforS;
            if (!syphTarget.isDead())
            {
                if (tSynth >= tPerSec)
                {
                    tSynth -= tPerSec;
                    syphTarget.TakeSynthDamage(tPerSec);
                    _synthesis += sPerSecond;
                }
                else
                {
                    tSynth -= tPerSec + (tSynth - tPerSec);
                    syphTarget.TakeSynthDamage(tPerSec + (tSynth - tPerSec));
                    _synthesis += tPerSec + (tSynth - tPerSec);
                }
            }
            else
            {
                _syphoning = false;
                // -- threat - has killed the cell
                yield break;
            }

        }

        _syphoning = false;
    }

    public override void PartAction()
    {
        if (!_selfRepairOn && _synthesis >= rCost)
        {
            _synthesis -= rCost;
            _selfRepairOn = true;
            StartCoroutine(Repair(repdurab, durabPerS));
        }
    }
}
