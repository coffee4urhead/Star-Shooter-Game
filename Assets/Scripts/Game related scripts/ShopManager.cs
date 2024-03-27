using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using JetBrains.Annotations;
using System;

public class ShopManager : MonoBehaviour
{
    [SerializeField]
    private ShopItemSO[] _scriptableObj;
    [SerializeField]
    private List<ShopTemplate> _shopItems;
    [SerializeField]
    private List<Button> _buttons;
    private Text _scoreFromTheDestrScene;
    private void Start()
    {
        _scoreFromTheDestrScene = GameObject.Find("Score_text").GetComponent<Text>();

        if (_scriptableObj.Length != _shopItems.Count)
        {
            int max = Math.Max(_scriptableObj.Length, _shopItems.Count);
            int min = Math.Min(_scriptableObj.Length, _shopItems.Count);

            for (int m = 0; m < max - min; m++)
            {
                Destroy(_shopItems[m].gameObject);
                _shopItems.RemoveAt(m);
                Destroy(_buttons[m].gameObject);
                _buttons.RemoveAt(m);
            }
        }
        LoadItemsInfo();
        CheckIfPurchaseable();
    }
    private void Update()
    {
        // CheckIfPurchaseable();
    }
    public void LoadItemsInfo()
    {
        for (int i = 0; i < _scriptableObj.Length; i++)
        {
            _shopItems[i].titleOfProduct.text = _scriptableObj[i].title.ToString();
            _shopItems[i].descriptionOfProduct.text = _scriptableObj[i].description.ToString();
            _shopItems[i].costTxt.text = _scriptableObj[i].baseCost.ToString();
        }
    }

    public void CheckIfPurchaseable()
    {
        string actualScoreNumber = "";

        foreach (char ch in _scoreFromTheDestrScene.text)
        {
            // 48 to 57
            if (ch >= 48 && ch <= 57)
            {
                actualScoreNumber += ch;
            }
        }

        int castOfAvailableMoney = int.Parse(actualScoreNumber);

        if (_scoreFromTheDestrScene == null)
        {
            Debug.LogError("Here is a error!");
        }

        for (int i = 0; i < _scriptableObj.Length; i++)
        {
            Debug.Log("We stepped in");
            string num = "";

            foreach (char letter in _scriptableObj[i].baseCost)
            {
                if ((int)letter >= 48 && (int)letter <= 57)
                {
                    num += letter.ToString();
                }
            }
            if (castOfAvailableMoney >= int.Parse(num))
            {
                _buttons[i].interactable = false;
            }
            else
            {
                _buttons[i].interactable = true;
            }
        }
    }
}
