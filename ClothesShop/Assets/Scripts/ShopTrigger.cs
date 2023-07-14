using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTrigger : MonoBehaviour
{

    private Player player;
    private Shop shop;
    private PlayerInventory playerInventory;
    private bool openFlag;
    void Start()
    {
        playerInventory = PlayerInventory.instance;
    }

    void Update()
    {
        if(player != null && player.Interact() && !openFlag)
        {
            shop.ToggleShop(true);
            openFlag = true;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        player = col.GetComponent<Player>();
        if(player != null)
        {
            playerInventory.SetCurrentShop(shop);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        player = null;
    }

    public void SetShop(Shop shop)
    {
        this.shop = shop;
    }
}
