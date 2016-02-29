using UnityEngine;
using System.Collections;
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

    //current Chasis properties -- test stuff
    int repdurab = 50;
    int durabPerS = 10;
    int rCost = 5;


    int syphVal = 60;
    int syphPerS = 15;


    void Awake()
    {
        _waitforS = new WaitForSeconds(1);
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
