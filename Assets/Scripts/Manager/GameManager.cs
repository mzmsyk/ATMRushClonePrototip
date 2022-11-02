using Commands;
using Enums;
using Keys;
using Signals;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Self Variables
    public LoadGameCommand loadGameCommand;

    #region Private Variables

    private SaveGameCommand _SaveGameCommand;

    #endregion

    #endregion

    private void Awake()
    {
        Application.targetFrameRate = 60;

        loadGameCommand = new LoadGameCommand();
        _SaveGameCommand = new SaveGameCommand();


        if (!ES3.FileExists())
        {
            ES3.Save("Score", 0);
            ES3.Save("Level", 0);
        }
    }
    private void Start()
    {
        //Kaydedilmi� de�erlerin s�f�rlanmas�nda kullan�l�r.
        //CoreGameSignals.Instance.onSaveGameData(new SaveGameDataParams() { Level = 0, Money = 0 });

    }

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        CoreGameSignals.instance.onSaveGameData += _SaveGameCommand.OnSaveGameData;//kay�t

        ScoreSignals.Instance.loadSavedLevelValue += LoadSavedLevelValue;//y�kleme
        ScoreSignals.Instance.loadSavedMoneyValue += LoadSavedMoneyValue;
    }

    private void UnsubscribeEvents()
    {
       // CoreGameSignals.instance.onSaveGameData -= _SaveGameCommand.OnSaveGameData;

        ScoreSignals.Instance.loadSavedLevelValue -= LoadSavedLevelValue;
        ScoreSignals.Instance.loadSavedMoneyValue -= LoadSavedMoneyValue;
    }
    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    private int LoadSavedLevelValue()
    {
        return loadGameCommand.OnLoadGameData(SaveLoadStates.Level);
    }
    private int LoadSavedMoneyValue()
    {
        return loadGameCommand.OnLoadGameData(SaveLoadStates.Money);
    }
}