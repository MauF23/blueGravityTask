using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class Shop : MonoBehaviour
{
    public List<Clothes> shopClothes;

    [ReadOnly]
    public Clothes selectedClothes;
    public ShopTrigger shopTrigger;
    private PlayerInventory playerInventory;
    private ShopUI shopUI;

    void Start()
    {
        playerInventory = PlayerInventory.instance;
        shopUI = ShopUI.instance;
        shopTrigger.SetShop(this);
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

    public void OpenShop()
    {
        shopUI.ToggleShopUI(true);
        InitializeStore();
    }
}
