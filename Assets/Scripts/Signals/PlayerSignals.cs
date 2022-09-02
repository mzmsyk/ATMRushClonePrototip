using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerSignals : MonoBehaviour
{
    #region self vars
    #region public vars
    public static PlayerSignals Instance;
    #endregion
    #region serializefield vars
    #endregion
    #region private vars

    #endregion
    #endregion

    #region Singleton Awake
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    public UnityAction onPlayerAndObstacleCrash = delegate { };
    public UnityAction<Transform> onPlayerAndATMCrash = delegate { };
    public UnityAction onPlayerEnterFinishLine = delegate { };
    public UnityAction<GameObject> onFinishPlayerCollideWithBox = delegate (GameObject arg0) { };
}

