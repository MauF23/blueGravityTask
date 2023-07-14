using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class PlayerInventory : MonoBehaviour
{
    public int wallet;
    public List<Clothes> playerClothes;
    public enum walletBallanceModifier {add, remove}

    [ReadOnly]
    public Shop currentShop;
    private ShopUI shopUI;
    public static PlayerInventory instance;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        shopUI = ShopUI.instance;
    }

    public void AddClothes(Clothes clothes)
    {
        if (!playerClothes.Contains(clothes))
        {
            playerClothes.Add(clothes);
        }
    }

    public void RemoveClothes(Clothes clothes)
    {
        if (playerClothes.Contains(clothes))
        {
            playerClothes.Remove(clothes);
        }
    }

    public void ModifyWalletBalance(int value, walletBallanceModifier modifier)
    {
        switch (modifier)
        {
            case walletBallanceModifier.add:
                wallet += value;
                break;

            case walletBallanceModifier.remove:
                wallet -= value;
                break;
        }

        if(wallet <= 0)
        {
            wallet = 0;
        }
    }

    public void InitializeStore()
    {
        if (currentShop != null)
        {
            shopUI.SetClothesButton(playerClothes, currentShop, ShopUI.TransactionType.sell);
        }
    }

    public void OpenShop()
    {
        shopUI.ToggleShopUI(true);
        InitializeStore();
    }

    public void SetCurrentShop(Shop shop)
    {
        currentShop = shop;
    }
}
