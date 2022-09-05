using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Enums;

public class UISignals : MonoBehaviour
{
    #region Self Vars
    public static UISignals Instance;
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
            Destroy(this.gameObject);
        }
    }
    #endregion




    public UnityAction<UIPanels> onOpenPanel = delegate { };
    public UnityAction<UIPanels> onClosePanel = delegate { };



}
