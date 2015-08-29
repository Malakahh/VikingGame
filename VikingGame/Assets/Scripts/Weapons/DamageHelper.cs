using UnityEngine;
using System;
using System.Collections.Generic;

public enum ObstacleArmorType { Unarmored }
public enum AmmunitionDamageType { Piercing }

[ExecuteInEditMode]
public class DamageHelper : MonoBehaviour
{
    public static DamageHelper Instance;

    static DamageHelperTableEntry[] GetDamageTable()
    {
        List<DamageHelperTableEntry> table = new List<DamageHelperTableEntry>();

        foreach (AmmunitionDamageType dt in Enum.GetValues(typeof(AmmunitionDamageType)))
        {
            foreach (ObstacleArmorType at in Enum.GetValues(typeof(ObstacleArmorType)))
            {
                table.Add(new DamageHelperTableEntry() {
                    DamageType = dt,
                    ArmorType = at,
                    DamageModifier = -1f
                });
            }
        }

        return table.ToArray();
    }

    public DamageHelperTableEntry[] damageTable = GetDamageTable();

    void Awake()
    {
        Instance = this;
    }
}

[Serializable]
public class DamageHelperTableEntry
{
    public AmmunitionDamageType DamageType;
    public ObstacleArmorType ArmorType;
    public float DamageModifier;
}