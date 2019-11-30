using System;
using System.Collections;
using TMPro;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// Display User Coin and Text UI also handling corresponding display events
/// </summary>
public class UserCurrencyUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI goldText;
    [SerializeField] TextMeshProUGUI coinText;

    [Tooltip("How many seconds does it take for a currency to drop to a specific value")]
    [SerializeField] float changeDur;
    [Tooltip("Text color while value changing")]
    [SerializeField] Color changeColor;

    UserData userData;
    Color coinDefaultColor;
    Color goldDefaultColor;

    private void Start()
    {
        userData = DataGlobal.instance.userData;

        goldText.text = userData.gold.ToString();
        coinText.text = userData.coin.ToString();

        goldDefaultColor = goldText.color;
        coinDefaultColor = coinText.color; 

        RegisterEvent();
    }

    private void OnDestroy()
    {
        UnRegisterEvent();
    }

    private void RegisterEvent()
    {
        UserData.OnCoinChangeEvent += OnChangeCoin;
        UserData.OnGoldChangeEvent += OnChangeGold;
    }

    void UnRegisterEvent()
    {
        UserData.OnCoinChangeEvent -= OnChangeCoin;
        UserData.OnGoldChangeEvent -= OnChangeGold;
    }


    #region events
    void OnChangeCoin(int changeAmount)
    {
        coinText.text = userData.coin.ToString();
        coinText.DOColor(changeColor, changeDur).OnComplete(() => {
            coinText.DOColor(coinDefaultColor, changeDur);
        });
        // StartCoroutine(ChangeCoinUICoroutine(changeAmount));
    }

    IEnumerator ChangeCoinUICoroutine(int changeAmount)
    {
        int newValue = userData.coin;
        float currentValue = float.Parse(coinText.text);
        coinText.color = changeColor;

        while (newValue != currentValue)
        {
            currentValue += Math.Sign(changeAmount);
            coinText.text = currentValue.ToString();
            yield return new WaitForSeconds(changeDur / Mathf.Abs(changeAmount));
        }

        coinText.color = coinDefaultColor;
    }

    void OnChangeGold(int changeAmount)
    {
        goldText.text = userData.gold.ToString();
        goldText.DOColor(changeColor, changeDur).OnComplete(() => {
            goldText.DOColor(goldDefaultColor, changeDur);
        }); 
        // StartCoroutine(ChangeGoldUICoroutine(changeAmount));
    }

    IEnumerator ChangeGoldUICoroutine(int changeAmount)
    {
        int newValue = userData.gold;
        int currentValue = int.Parse(goldText.text);
        goldText.color = changeColor;

        while (newValue != currentValue)
        {
            currentValue+= Math.Sign(changeAmount);
            goldText.text = currentValue.ToString();
            yield return new WaitForSeconds(changeDur / Mathf.Abs(changeAmount));
        }

        goldText.color = coinDefaultColor;
    }
    #endregion
}
