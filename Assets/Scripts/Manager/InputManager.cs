using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    #region vars
    [SerializeField] private FixedJoystick fixedJoystick;
    #endregion
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (fixedJoystick.Horizontal>0.1f||fixedJoystick.Horizontal<-0.1f)
        {
            
        }
    }
}
