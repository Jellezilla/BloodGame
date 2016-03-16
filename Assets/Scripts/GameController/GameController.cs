using UnityEngine;
using System.Collections;

/// <summary>
/// Gamecontroller class, Usage GameController.Instance
/// </summary>
public class GameController : Singleton<GameController> {

    private Player _player;
    private ThreatSystem _threatSystem;
	// Use this for initialization
	void Awake ()
    {
       _player = GameObject.FindGameObjectWithTag(Tags.playerTag).GetComponent<Player>();
       _threatSystem = GetComponent<ThreatSystem>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    #region Chasis State
    public int ChasisMaxDurability
    {
        get
        {
            return _player.Chasis.MaxDurability;
        }
    }

    public int ChasisCurrentDurability
    {
        get
        {
            return _player.Chasis.CurrentDurability;
        }
    }

    public double ChasisCurrentResources
    {
        get
        {
            return _player.Chasis.CurrentResources;
        }
    }

    public GameObject Player
    {
        get
        {
            return _player.gameObject;
        }
    }

    public ThreatSystem ThreatSystem
    {
        get
        {
            return _threatSystem;
        }
    }
    #endregion
}
