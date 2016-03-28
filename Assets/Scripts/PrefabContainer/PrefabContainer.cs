using UnityEngine;
using System.Collections;

public class PrefabContainer : Singleton<PrefabContainer> {

  
    public readonly GameObject Chasis = (GameObject)Resources.Load("");
    public readonly GameObject  MainThruster = (GameObject)Resources.Load("");
    public readonly GameObject LateralThruster = (GameObject)Resources.Load("");
    public readonly GameObject HookLauncher = (GameObject)Resources.Load("");
    public readonly GameObject LaserRifle = (GameObject)Resources.Load("");
    public readonly GameObject RocketLauncher = (GameObject)Resources.Load("");
    public readonly GameObject MineLayer = (GameObject)Resources.Load("");


    public readonly GameObject WhiteCell = (GameObject)Resources.Load("");
    public readonly GameObject WhiteCellBoss = (GameObject)Resources.Load("");
    public readonly GameObject RedCell = (GameObject)Resources.Load("");
    public readonly GameObject VirusCell = (GameObject)Resources.Load("");
    public readonly GameObject VirusCellBoss = (GameObject)Resources.Load("");



}
