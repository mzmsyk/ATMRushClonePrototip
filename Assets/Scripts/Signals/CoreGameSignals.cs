using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Keys;
using System;

public class CoreGameSignals : MonoBehaviour
{
    #region vars
    public static CoreGameSignals instance;
    #endregion
    #region awake singleton
    private void Awake()
    {
        if (instance==null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    #endregion

    public UnityAction onChangeGameState = delegate { };
    public UnityAction onLevelInitialize = delegate { };
    public UnityAction onClearActiveLevel = delegate { };
    public UnityAction onLevelFailed = delegate { };
    public UnityAction onLevelSuccessful = delegate { };
    public UnityAction onNextLevel = delegate { };
    public UnityAction onRestartLevel = delegate { };
    public UnityAction onPlay = delegate { };
    public UnityAction onReset = delegate { };
    public UnityAction onGameEnd = delegate { };
    public UnityAction onCameraInitialized = delegate { };

    //public UnityAction<SaveGameDataParams> onSaveGameData = delegate { };

    //public Func<int> onGetLevelID = delegate { return 0; };




}
