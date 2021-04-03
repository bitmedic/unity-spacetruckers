using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Cargo", menuName = "ScriptableObjects/Cargo", order = 1)]
public class CargoSO : ScriptableObject
{
    public string cargoName;

    public Color cargoColor;
    public Sprite cargoSprite;
    public EnumCargo cargoType;
}
