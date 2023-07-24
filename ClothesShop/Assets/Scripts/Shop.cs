using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class Shop : MonoBehaviour
{
    //public List<Clothes> shopClothes;
    public Inventory shopInventory;

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
            if (playerInventory.inventory.wallet - selectedClothes.price < 0)
            {
                //can't buy clothes
            }
            else
            {
                playerInventory.ModifyWalletBalance(selectedClothes.price, Inventory.walletBallanceModifier.remove);
                playerInventory.AddClothes(selectedClothes);
                shopInventory.RemoveClothes(selectedClothes);
                SelectClothes(null);
            }
        }
    }

    public void SellClothes()
    {
        if (selectedClothes != null)
        {
            playerInventory.ModifyWalletBalance(selectedClothes.price, Inventory.walletBallanceModifier.add);
            playerInventory.RemoveClothes(selectedClothes);
            shopInventory.AddClothes(selectedClothes);
        }

    }

    public void InitializeStore()
    {
        shopUI.SetClothesButton(shopInventory.clothes, this, ShopUI.TransactionType.buy);
    }

    public void OpenShop()
    {
        shopUI.ToggleShopUI(true);
        InitializeStore();
    }
}
