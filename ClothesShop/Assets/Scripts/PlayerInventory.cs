using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening;
using UnityEngine.Pool;

public class PlayerInventory : MonoBehaviour
{
    public int wallet;
    public CanvasGroup playerInventoryCanvas;
    public Transform playerInventoryContainer;
    private ObjectPool<GameObject> clothesButtonPool;
    public List<Clothes> playerClothes;
    private Clothes equippedClothes;
    public List<ClothesInventoryButton> clothesInventoryButtonList;
    public GameObject clothesInventoryButtonPrefab;
    public SpriteRenderer pelvis, torso, hood;
    public enum walletBallanceModifier {add, remove}

    [ReadOnly]
    public Shop currentShop;
    private ShopUI shopUI;
    private Player player;
    const int clothesButtonPoolMinSize = 0;
    const int clothesButtonPoolMaxSize = 100;
    private bool inventoryUI;
    const float fadeTime = 0.25f;
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
        player = Player.instance;
        playerInventoryCanvas.alpha = 0;
        ToggleInventoryCanvas(false);

        clothesButtonPool = new ObjectPool<GameObject>(() =>
        {
            return Instantiate(clothesInventoryButtonPrefab);
        }, clothes =>
        {
            clothes.gameObject.SetActive(true);
        }, clothes =>
        {
            clothes.gameObject.SetActive(false);
        }, clothes =>
        {
            clothes.gameObject.SetActive(false);
        }, false, clothesButtonPoolMinSize, clothesButtonPoolMaxSize);

        for (int i = 0; i < clothesButtonPoolMaxSize; i++)
        {
            PoolButton();
        }

        EquipStartingClothes();
    }

    void Update()
    {
        if (player.Inventory() && player.canMove)
        {
            ToggleInventoryCanvas(!inventoryUI);
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

    public void ToggleInventoryCanvas(bool value)
    {
        inventoryUI = value;
        playerInventoryCanvas.blocksRaycasts = value;
        playerInventoryCanvas.interactable = value;

        if (value)
        {
            playerInventoryCanvas.DOFade(1, fadeTime);
            SetClothesButton();
        }
        else
        {
            playerInventoryCanvas.DOFade(0, fadeTime);
        }
    }

    public void ChangeClothes(Clothes clothes)
    {
        if (equippedClothes != null)
        {
            playerClothes.Add(equippedClothes);
        }

        pelvis.sprite = clothes.pelvis;
        torso.sprite = clothes.torso;
        hood.sprite = clothes.hood;

        if (playerClothes.Contains(clothes))
        {
            playerClothes.Remove(clothes);
        }

        equippedClothes = clothes;
    }

    private void PoolButton()
    {
        GameObject go = clothesButtonPool.Get();
        go.transform.SetParent(playerInventoryContainer, false);
        ClothesInventoryButton button = go.GetComponent<ClothesInventoryButton>();
        clothesInventoryButtonList.Add(button);
    }

    public void SetClothesButton()
    {
        for (int i = 0; i < clothesInventoryButtonList.Count; i++)
        {
            if (i < playerClothes.Count)
            {
                ClothesInventoryButton clothesButton = clothesInventoryButtonList[i];
                clothesButton.SetButton(playerClothes[i]);
                clothesButton.gameObject.SetActive(true);
            }
            else
            {
                clothesButtonPool.Release(clothesInventoryButtonList[i].gameObject);
            }
        }
    }

    private void EquipStartingClothes()
    {
        if(playerClothes.Count > 0)
        {
            ChangeClothes(playerClothes[0]);
        }
    }
}
