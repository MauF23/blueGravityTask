using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ClothesButton : MonoBehaviour
{
    private Clothes clothes;
    private new string name;
    public TextMeshProUGUI priceText;
    public Image buttonIcon;
    public Button button;
    private Shop shop;
    private ShopUI shopUI;

    void Start()
    {
        shopUI = ShopUI.instance;
    }
    public void SetClothesButton(Clothes clothes, Shop shop)
    {
        this.clothes = clothes;
        priceText.text = clothes.price.ToString();
        buttonIcon.sprite = clothes.icon;
        name = clothes.name;
        this.shop = shop;
        AddButtonListeners();
    }

    private void AddButtonListeners()
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(delegate { shop.SelectClothes(clothes); });
        button.onClick.AddListener(delegate { shopUI.ToggleConfirmationPanel(true, name); });
    }
}
