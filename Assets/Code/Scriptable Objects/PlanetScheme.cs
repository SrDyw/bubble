using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/Scheme/Planet", fileName = "PlanetScheme")]
public class PlanetScheme : ScriptableObject
{
    public string Name;
    [TextArea(2, 5)] public string Description;
    public Color Color = Color.white;
    public float Weight = 1;
}
