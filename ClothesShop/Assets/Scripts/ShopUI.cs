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
    private ObjectPool<GameObject> clothesButtonPool;
    public List<ClothesButton> clothesButtonList;
    public CanvasGroup canvasGroup;
    const int clothesButtonPoolMinSize = 0;
    const int clothesButtonPoolMaxSize = 100;
    const float fadeTime = 1;
    public static ShopUI instance;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
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

        canvasGroup.alpha = 0;
        ToggleShopUI(false);
    }

    public void SetClothesButton(List<Clothes> clothes, Shop shop)
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
    }

    public void ToggleShopUI(bool value)
    {
        canvasGroup.blocksRaycasts = value;
        canvasGroup.interactable = value;

        if (value)
        {
            canvasGroup.DOFade(1, fadeTime);
        }
        else
        {
            canvasGroup.DOFade(0, fadeTime);
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
