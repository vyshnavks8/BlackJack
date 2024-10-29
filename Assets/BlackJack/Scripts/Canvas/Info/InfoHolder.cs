using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create InfoHolder", fileName = "InfoHolder", order = 0)]
public class InfoHolder : ScriptableObject
{
    public List<InfoData> data=new();
}