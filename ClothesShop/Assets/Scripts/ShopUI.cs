using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.Pool;

public class ShopUI : MonoBehaviour
{
    const float dateTime = 1;
    public Transform clothesButtonContainer;
    public GameObject clothesButtonPrefab;
    public CanvasGroup canvasGroupShop, canvasGroupConfirmation;
    public Button confirmBuyButton;
    private ObjectPool<GameObject> clothesButtonPool;
    public List<ClothesButton> clothesButtonList;
    public enum TransactionType { buy, sell}
    const int clothesButtonPoolMinSize = 0;
    const int clothesButtonPoolMaxSize = 100;
    const float fadeTime = 0.25f;
    public static ShopUI instance;
    private PlayerInventory playerInventory;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        playerInventory = PlayerInventory.instance;
        clothesButtonPool = new ObjectPool<GameObject>(() =>
        {
            return Instantiate(clothesButtonPrefab);
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

        for(int i = 0; i < clothesButtonPoolMaxSize; i++)
        {
            PoolButton();
        }

        canvasGroupShop.alpha = 0;
        canvasGroupConfirmation.alpha = 0;
        ToggleShopUI(false);
        ToggleConfirmationPanel(false);
    }

    public void SetClothesButton(List<Clothes> clothes, Shop shop, TransactionType transactionType)
    {
        for (int i = 0; i < clothesButtonList.Count; i++)
        {
            if (i < clothes.Count)
            {
                ClothesButton clothesButton = clothesButtonList[i];
                clothesButton.SetClothesButton(clothes[i], shop);
                clothesButton.gameObject.SetActive(true);
            }
            else
            {
                clothesButtonPool.Release(clothesButtonList[i].gameObject);
            }
        }

        confirmBuyButton.onClick.RemoveAllListeners();
        switch (transactionType)
        {
            case TransactionType.buy:
                confirmBuyButton.onClick.AddListener(delegate { shop.BuyClothes(); });
                confirmBuyButton.onClick.AddListener(delegate { shop.InitializeStore(); });
                break;

            case TransactionType.sell:
                confirmBuyButton.onClick.AddListener(delegate { shop.SellClothes(); });
                confirmBuyButton.onClick.AddListener(delegate { playerInventory.InitializeStore(); });
                break;
        }
    
        confirmBuyButton.onClick.AddListener(delegate { ToggleConfirmationPanel(false); });
    }


    public void ToggleShopUI(bool value)
    {
        canvasGroupShop.blocksRaycasts = value;
        canvasGroupShop.interactable = value;

        if (value)
        {
            canvasGroupShop.DOFade(1, fadeTime);
        }
        else
        {
            canvasGroupShop.DOFade(0, fadeTime);
        }
    }

    public void ToggleConfirmationPanel(bool value)
    {
        canvasGroupConfirmation.blocksRaycasts = value;
        canvasGroupConfirmation.interactable = value;

        if (value)
        {
            canvasGroupConfirmation.DOFade(1, fadeTime);
        }
        else
        {
            canvasGroupConfirmation.DOFade(0, fadeTime);
        }
    }

    private void PoolButton()
    {
        GameObject go = clothesButtonPool.Get();
        go.transform.SetParent(clothesButtonContainer, false);
        ClothesButton button = go.GetComponent<ClothesButton>();
        clothesButtonList.Add(button);
    }


}
