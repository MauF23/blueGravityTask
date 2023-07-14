using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClothesInventoryButton : MonoBehaviour
{
    private Clothes clothes;
    private new string name;
    public Image buttonIcon;
    public Button button;
    public TextMeshProUGUI nameText;
    private PlayerInventory playerInventory;

    void Start()
    {
        playerInventory = PlayerInventory.instance;
    }

    public void SetButton(Clothes clothes)
    {
        this.clothes = clothes;
        name = clothes.name;
        nameText.text = name;
        buttonIcon.sprite = clothes.icon;
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(delegate { playerInventory.ChangeClothes(this.clothes); });
    }
}
