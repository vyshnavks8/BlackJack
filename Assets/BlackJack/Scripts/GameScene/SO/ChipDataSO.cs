using System;
using System.Linq;
using UnityEngine;

[Serializable]
public struct ChipData
{
    public int ChipValue;
    public Color ChipColour;
}

[CreateAssetMenu(menuName = "Create ChipData", fileName = "ChipData", order = 0)]
public class ChipDataSO : ScriptableObject
{
    [SerializeField] private ChipData[] chips;
    public int Count => chips.Length;

    public Color GetChipColour(int chipValue)
    {
        foreach (var chip in chips)
        {
            if (chip.ChipValue == chipValue)
            {
                return chip.ChipColour;
            }
        }
        return Color.white;
    }

    public int GetChipAmount(int selectedID)
    {
        return chips[selectedID].ChipValue;
    }

    public int GetIDBelow(int amount)
    {
        var index = chips.Length;
        foreach (var chip in chips.Reverse())
        {
            index -=1;
            if (chip.ChipValue < amount)
            {
                return index;
            }
        }
        return index;
    }
}