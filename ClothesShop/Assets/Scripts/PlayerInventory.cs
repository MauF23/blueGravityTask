using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int wallet;
    public List<Clothes> playerClothes;
    public enum walletBallanceModifier {add, remove}
    public static PlayerInventory instance;
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
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
}
