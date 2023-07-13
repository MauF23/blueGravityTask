using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ClothesButton : MonoBehaviour
{
    public Clothes clothes;
    public TextMeshProUGUI priceText;
    public Image buttonIcon;
    public new string name;
    
    public void SetClothesButton(Clothes clothes)
    {
        this.clothes = clothes;
        priceText.text = clothes.price.ToString();
        buttonIcon.sprite = clothes.icon;
        name = clothes.name;
    }
}
