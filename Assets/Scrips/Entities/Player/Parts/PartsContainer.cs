using UnityEngine;
using System.Collections.Generic;


// WILL HANDLE SKILLS/PARTS

/// <summary>Used to store all parts currently attached to the player.</summary>
public class PartsContainer : MonoBehaviour {

    private Dictionary<PartType, Parts> _currentParts;

    public Launcher testPart; // test var before we link stuff
	// Use this for initialization
	void Awake ()
    {
        _currentParts = new Dictionary<PartType, Parts>();
        _currentParts.Add(PartType.Launcher, testPart); // adding test part xD
	
	}

    /// <summary>Add part to the dictionary</summary>
    /// <param name="partType">Passed Part script type</param>
    /// <param name=”part”>Part script reference</param>
    public void AddPart(PartType partType,Parts part)
    {
        switch (partType)
        {
            case PartType.Launcher:
                {
                    _currentParts.Add(partType, part);
                    break;
                }
        }
    }


    /// <summary>Used to remove a part from the dictionary</summary>
    /// <param name=”part”>Part type</param>
    public void RemovePart(PartType part)
    {
        if (_currentParts.ContainsKey(part))
        {
            _currentParts.Remove(part);
        }
    }

    /// <summary>Used to get a part from the dictionary, returns null if part is not found</summary>
    /// <param name=”part”>Wanted part type</param>
    public Parts GetPart(PartType part)
    {
        if (_currentParts.ContainsKey(part))
        {
            return _currentParts[part];
        }
        else
        {
            return null;
        }

    }
}
