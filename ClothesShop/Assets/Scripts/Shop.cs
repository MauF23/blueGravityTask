using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class Shop : MonoBehaviour
{
    public List<Clothes> shopClothes;
    public Clothes selectedClothes;
    private PlayerInventory playerInventory;

    [Button("Buy")]
    void Buy()
    {
        BuyClothes();
    }
    void Start()
    {
        playerInventory = PlayerInventory.instance;
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

    public void SellClothes(Clothes clothes)
    {
        playerInventory.ModifyWalletBalance(clothes.price, PlayerInventory.walletBallanceModifier.add);
        playerInventory.RemoveClothes(clothes);
        shopClothes.Add(clothes);

    }
}
