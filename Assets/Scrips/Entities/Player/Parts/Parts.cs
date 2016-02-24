using UnityEngine;
using System.Collections;


/// <summary>Player part base class, used to define common characteristics between all the parts.</summary>
public abstract class Parts :MonoBehaviour {

    protected string _partName;
    protected int _partCost;
    protected string _partDescription;
    protected float _launchVelocity;

    /// <summary>Abstract method used to define what each part does. Refer to each part class.</summary>
    public abstract void PartAction();

}
