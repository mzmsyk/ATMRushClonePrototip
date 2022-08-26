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

    public UnityAction onPlay = delegate { };
    public UnityAction onLevelSuccessful = delegate { };




}
