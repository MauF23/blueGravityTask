using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.Pool;
using TMPro;

public class ShopUI : MonoBehaviour
{
    const float dateTime = 1;
    public Transform clothesButtonContainer;
    public GameObject clothesButtonPrefab;
    public CanvasGroup canvasGroupShop, canvasGroupConfirmation;
    public Button confirmBuyButton, cancelButton;
    public TextMeshProUGUI confirmationInstructions;
    private ObjectPool<GameObject> clothesButtonPool;
    public List<ClothesButton> clothesButtonList;
    public enum TransactionType { buy, sell}
    private TransactionType currentTransactionType;
    const int clothesButtonPoolMinSize = 0;
    const int clothesButtonPoolMaxSize = 100;
    const string buyInstructions = "What are you buying";
    const string sellInstructions = "What are you selling";
    const string confirmBuy = "Buy:";
    const string confirmSell = "Sell;";
    private string previousInstructions;
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
        cancelButton.onClick.AddListener(delegate { ToggleConfirmationPanel(false, null); });
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
        ToggleConfirmationPanel(false, null);
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
                SetTransactionInstructions(buyInstructions);
                break;

            case TransactionType.sell:
                confirmBuyButton.onClick.AddListener(delegate { shop.SellClothes(); });
                confirmBuyButton.onClick.AddListener(delegate { playerInventory.InitializeStore(); });
                SetTransactionInstructions(sellInstructions);
                break;
        }

        currentTransactionType = transactionType;
        confirmBuyButton.onClick.AddListener(delegate { ToggleConfirmationPanel(false, null); });
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

    public void ToggleConfirmationPanel(bool value, string itemName)
    {
        canvasGroupConfirmation.blocksRaycasts = value;
        canvasGroupConfirmation.interactable = value;

        if (value)
        {
            canvasGroupConfirmation.DOFade(1, fadeTime);
            switch (currentTransactionType)
            {
                case TransactionType.buy:
                    SetTransactionInstructions(buyInstructions);
                    break;

                case TransactionType.sell:
                    SetTransactionInstructions(sellInstructions);
                    break;
            }
        }
        else
        {
            canvasGroupConfirmation.DOFade(0, fadeTime);

            switch (currentTransactionType)
            {
                case TransactionType.buy:
                    SetTransactionInstructions($"{confirmBuy} <color=red>{itemName}</color>?");
                    break;

                case TransactionType.sell:
                    SetTransactionInstructions($"{confirmSell} <color=red>{itemName}</color>?");
                    break;
            }
        }
    }

    private void SetTransactionInstructions(string instructions)
    {
        confirmationInstructions.text = instructions;
    }

    private void PoolButton()
    {
        GameObject go = clothesButtonPool.Get();
        go.transform.SetParent(clothesButtonContainer, false);
        ClothesButton button = go.GetComponent<ClothesButton>();
        clothesButtonList.Add(button);
    }


}
