using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Enums;
using System;

public class UIManager : MonoBehaviour
{
    #region Self Vars
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI moneyText;
    UIPanelController _uiPanelController;
    private int _levelID;
    private int _money;
    #endregion

    #region Event Subscription

    void OnEnable()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        CoreGameSignals.instance.onPlay += OnPlay;
        CoreGameSignals.instance.onGameEnd += OnEndGame;
        CoreGameSignals.instance.onNextLevel += OnNextLevel;
        UISignals.Instance.onOpenPanel += OnOpenPanel;
        UISignals.Instance.onClosePanel += OnClosePanel;
        CoreGameSignals.instance.onRestartLevel += OnRestartLevel;
        //ScoreSignals.nstance.onCompleteScore += OnCompleteScore;
    }

    private void UnsubscribeEvents()
    {
        CoreGameSignals.instance.onPlay -= OnPlay;
        CoreGameSignals.instance.onGameEnd -= OnEndGame;
        CoreGameSignals.instance.onNextLevel -= OnNextLevel;
        UISignals.Instance.onOpenPanel -= OnOpenPanel;
        UISignals.Instance.onClosePanel -= OnClosePanel;
        CoreGameSignals.instance.onRestartLevel -= OnRestartLevel;
        //ScoreSignals.Instance.onCompleteScore -= OnCompleteScore;
    }
    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    #endregion
    private void Awake()
    {
        _uiPanelController = GetComponent<UIPanelController>();

    }

    private void Start()
    {
        //_levelID = GetLevelIDData();
        //_money = GetMoneyData();

        UpdateLevelText(0);
        UpdateMoneyText();
    }

    //private int GetLevelIDData()
    //{
    //    return ScoreSignals.Instance.loadSavedLevelValue();
    //}

    //private int GetMoneyData()
    //{
    //    return ScoreSignals.Instance.loadSavedMoneyValue();

    //}

    private void UpdateLevelText(int incremental)
    {
        _levelID += incremental;
        levelText.text = "LEVEL " + (_levelID + 1);
    }

    private void UpdateMoneyText()
    {

        moneyText.text = _money.ToString();
    }



    private void OnPlay()
    {
        UISignals.Instance.onOpenPanel?.Invoke(UIPanels.InGamePanel);
        UISignals.Instance.onClosePanel?.Invoke(UIPanels.StartPanel);
    }
    private void OnEndGame()
    {
        UISignals.Instance.onOpenPanel?.Invoke(UIPanels.EndGamePanel);
        UISignals.Instance.onClosePanel?.Invoke(UIPanels.InGamePanel);
        UISignals.Instance.onClosePanel?.Invoke(UIPanels.MoneyAndLevelPanel);
    }

    private void OnNextLevel()
    {
        UISignals.Instance.onOpenPanel?.Invoke(UIPanels.StartPanel);
        UISignals.Instance.onClosePanel?.Invoke(UIPanels.EndGamePanel);
        UISignals.Instance.onOpenPanel?.Invoke(UIPanels.MoneyAndLevelPanel);
        UpdateLevelText(1);
    }
    private void OnRestartLevel()
    {
        UISignals.Instance.onOpenPanel?.Invoke(UIPanels.StartPanel);
        UISignals.Instance.onClosePanel?.Invoke(UIPanels.InGamePanel);
        UISignals.Instance.onClosePanel?.Invoke(UIPanels.EndGamePanel);
    }

    private void OnCompleteScore(float completescore)
    {
        moneyText.text = Convert.ToInt32(completescore).ToString();
    }

    public void PlayBtn()
    {
        CoreGameSignals.instance.onPlay?.Invoke();
    }

    public void NextLevelBtn()
    {
        CoreGameSignals.instance.onNextLevel?.Invoke();
    }

    public void RestartBtn()
    {
        CoreGameSignals.instance.onRestartLevel?.Invoke();
    }

    private void OnOpenPanel(UIPanels panel)
    {
        _uiPanelController.OpenPanel(panel);
    }

    private void OnClosePanel(UIPanels panel)
    {
        _uiPanelController.ClosePanel(panel);
    }
}
