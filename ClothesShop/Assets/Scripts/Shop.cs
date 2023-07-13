using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public List<Clothes> shopClothes;
    public Clothes selectedClothes;
    private PlayerInventory playerInventory;
    void Start()
    {
        playerInventory = PlayerInventory.instance;
    }

    public void SelectClothes()
    {

    }

    public void BuyClothes(Clothes clothes)
    {
        if(playerInventory.wallet - clothes.price < 0)
        {
            //can't buy clothes
        }
        else
        {
            playerInventory.ModifyWalletBalance(clothes.price, PlayerInventory.walletBallanceModifier.remove);
            playerInventory.AddClothes(clothes);
            shopClothes.Remove(clothes);
        }
    }

    public void SellClothes(Clothes clothes)
    {
        playerInventory.ModifyWalletBalance(clothes.price, PlayerInventory.walletBallanceModifier.add);
        playerInventory.RemoveClothes(clothes);
        shopClothes.Add(clothes);
    }
}
