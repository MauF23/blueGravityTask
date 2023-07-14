using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class Shop : MonoBehaviour
{
    public List<Clothes> shopClothes;
    public Clothes selectedClothes;
    private PlayerInventory playerInventory;
    private ShopUI shopUI;

    [Button("Buy")]
    void Buy()
    {
        BuyClothes();
    }

    [Button("OpenShop")]
    void Open()
    {
        ToggleShop(true);
    }

    [Button("CloseShop")]
    void Close()
    {
        ToggleShop(false);
    }
    void Start()
    {
        playerInventory = PlayerInventory.instance;
        shopUI = ShopUI.instance;
    }

    public void SelectClothes(Clothes clothes)
    {
        selectedClothes = clothes;
    }

    public void BuyClothes()
    {
        if (selectedClothes != null)
        {
            if (playerInventory.wallet - selectedClothes.price < 0)
            {
                //can't buy clothes
            }
            else
            {
                playerInventory.ModifyWalletBalance(selectedClothes.price, PlayerInventory.walletBallanceModifier.remove);
                playerInventory.AddClothes(selectedClothes);
                shopClothes.Remove(selectedClothes);
                SelectClothes(null);
            }
        }
    }

    public void SellClothes()
    {
        if (selectedClothes != null)
        {
            playerInventory.ModifyWalletBalance(selectedClothes.price, PlayerInventory.walletBallanceModifier.add);
            playerInventory.RemoveClothes(selectedClothes);
            shopClothes.Add(selectedClothes);
        }

    }

    public void InitializeStore()
    {
        shopUI.SetClothesButton(shopClothes, this, ShopUI.TransactionType.buy);
    }

    public void ToggleShop(bool value)
    {
        shopUI.ToggleShopUI(value);

        if (value)
        {
            InitializeStore();
        }
    }
}
