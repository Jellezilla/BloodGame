using UnityEngine;
using System.Collections;

// MIGHT USE THIS FOR SKILL TREE IMPLEMENTATION
public class Node
{
    private MonoBehaviour _value;
    private MonoBehaviour _leftValue;
    private MonoBehaviour _rightValue;

    #region Getter/Setter
    public MonoBehaviour LeftValue
    {
        get
        {
            return _leftValue;
        }

        set
        {
            _leftValue = value;
        }
    }

    public MonoBehaviour RightValue
    {
        get
        {
            return _rightValue;
        }

        set
        {
            _rightValue = value;
        }
    }
    public MonoBehaviour Value
    {
        get
        {
            return _value;
        }

        set
        {
            _value = value;

        }
    }
    #endregion


} 
