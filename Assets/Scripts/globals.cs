using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class globals
{
    public static int numKills = 0;
    
    // Weapon Values for Upgrades
    public static int pistolShootSpeed;
    public static int pistolAmmo;
    public static int pistolDamage;

    public static int smgShootSpeed;
    public static int smgAmmo;
    public static int smgDamage;

    public static int shotgunShootSpeed;
    public static int shotgunAmmo;
    public static int shotgunDamage;

    public static int grenadeShootSpeed;
    public static int grenadeAmmo;
    public static int grenadeDamage;
    // try instead later
    public static Dictionary<string, int> weaponStats = new Dictionary<string, int>() {
        {"pistolShootSpeed", 1},
        {"pistolAmmo", 1},
        {"pistolDamage", 1},
        {"smgShootSpeed", 1},
        {"smgAmmo", 1},
        {"smgDamage", 1},
        {"shotgunShootSpeed", 1},
        {"shotgunAmmo", 1},
        {"shotgunDamage", 1},
        {"grenadeShootSpeed", 1},
        {"grenadeAmmo", 1},
        {"grenadeDamage", 1},
    };

    // Enemy Values
    public static int warriorHealth;

    public static int droneHealth;
}