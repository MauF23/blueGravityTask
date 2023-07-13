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
    public ObjectPool<GameObject> clothesButtonPool;
    public CanvasGroup canvasGroup;
    const int clothesButtonPoolMinSize = 10;
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
            Destroy(clothes.gameObject);
        }, false, clothesButtonPoolMinSize, clothesButtonPoolMaxSize);

        for(int i = 0; i < clothesButtonPoolMinSize; i++)
        {
            GameObject clothesButton = clothesButtonPool.Get();
            clothesButton.transform.SetParent(clothesButtonContainer, false);
        }

        canvasGroup.alpha = 0;
        ToggleShopUI(false);
    }

    public void SetClothesButton(List<Clothes> clothes, Shop shop)
    {
        ClothesButton button = null;
        for(int i = 0; i < clothesButtonPool.CountAll; i++)
        {
            GameObject go = clothesButtonPool.Get();
            if (i < clothes.Count)
            {
                button = go.GetComponent<ClothesButton>();
                if(button != null)
                {
                    Debug.Log("clothesCountIs: " +clothes.Count);
                    Debug.Log("iIs: " + i);
                    button.SetClothesButton(clothes[i], shop);
                }
            }
            /*else
            {
                clothesButtonPool.Release(go);
            }*/
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


}
