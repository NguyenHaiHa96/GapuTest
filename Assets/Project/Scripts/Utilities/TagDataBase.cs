using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TagDataBase", menuName = "Scriptable Objects/Tag Data Base")]
public class TagDataBase : ScriptableObject
{
    public List<string> strTags = new();
}