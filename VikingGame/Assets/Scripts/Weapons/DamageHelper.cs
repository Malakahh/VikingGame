using UnityEngine;
using System;
using System.Collections.Generic;

public enum ObstacleArmorType { Unarmored }
public enum AmmunitionDamageType { Piercing }

[ExecuteInEditMode]
public class DamageHelper : MonoBehaviour
{
    public static DamageHelper Instance;

    static List<DamageHelperTableEntry> GetDamageTable()
    {
        List<DamageHelperTableEntry> table = new List<DamageHelperTableEntry>();

        foreach (AmmunitionDamageType dt in Enum.GetValues(typeof(AmmunitionDamageType)))
        {
            foreach (ObstacleArmorType at in Enum.GetValues(typeof(ObstacleArmorType)))
            {
                table.Add(new DamageHelperTableEntry() {
                    DamageType = dt,
                    ArmorType = at,
                    MultiplicativeDamageModifier = -1f
                });
            }
        }

        return table;
    }

    public List<DamageHelperTableEntry> DamageTable = GetDamageTable();

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        CheckDamageTable();
    }

    void CheckDamageTable()
    {
        int target = Enum.GetValues(typeof(AmmunitionDamageType)).Length * Enum.GetValues(typeof(ObstacleArmorType)).Length;
        
        if (DamageTable.Count != target || target <= 0)
        {
            Debug.LogError("DamageTable not right...");
        }

        foreach (DamageHelperTableEntry entry in DamageTable)
        {
            if (entry.MultiplicativeDamageModifier == -1)
            {
                Debug.LogError("DamageTable not right...");
            }
        }
    }
}

public class DamageHelperException : Exception
{ }

[Serializable]
public class DamageHelperTableEntry
{
    public AmmunitionDamageType DamageType;
    public ObstacleArmorType ArmorType;
    public float MultiplicativeDamageModifier;
}