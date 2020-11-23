using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Gun", menuName = "Gun")]
public class GunScriptObj : ScriptableObject
{

    public new string name;

    public GameObject gunModel;
    public GameObject bullet;

    public int damage;
    public int ammoCapacity;
    public int bulletsPerShot;

    public float rateOfFire;
    public float shotForce;
    public float spread;
    public float reloadTime;

    public bool isBouncing;
    public bool isExplosive;
}
