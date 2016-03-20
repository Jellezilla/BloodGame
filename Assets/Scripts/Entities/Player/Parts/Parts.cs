using UnityEngine;
using System.Collections;


/// <summary>Player part base class, used to define common characteristics between all the parts.</summary>
public abstract class Parts : MonoBehaviour
{

    protected string _partName;
    protected int _partCost;
    protected string _partDescription;
    protected float _partDurability;
    protected MountType _mountType;

    /// <summary>Abstract method used to define what each part does. Refer to each part class.</summary>
    public abstract void PartAction();

    protected void MountType()
    {
        if (GetType() == typeof(LaserRifle) || GetType() == typeof(RocketLauncher))
        {
            _mountType = global::MountType.Gimbaled;
        }
        else if (GetType() == typeof(MainThrusters) || GetType() == typeof(LateralThrusters) || GetType() == typeof(Minelayer) || GetType() == typeof(Chasis))
        {
            _mountType = global::MountType.Fixed;
        }
        else if (GetType() == typeof(UtilityHookLauncher))
        {
            _mountType = global::MountType.Turreted;
        }

    }


}
