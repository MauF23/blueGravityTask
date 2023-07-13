using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newClothes", menuName = "Clothes")]
public class Clothes:ScriptableObject
{
    public new string name;
    public int price;
    public Sprite icon;
}