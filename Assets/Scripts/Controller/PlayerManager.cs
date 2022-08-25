using Keys;
using Signals;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region self vars
    #region public vars
    #endregion
    #region serializefield vars
    
    #endregion
    #region private vars
    PlayerMovementController _playerMovementController;
    private PlayerData _playerData;
    #endregion
    #endregion

    private void Awake()
    {
        _playerMovementController = GetComponent<PlayerMovementController>();
        
        SendPlayerDataToController();
    }
    

    #region Event Subsicription

    void OnEnable()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        
        InputSignals.Instance.onInputDragged += OnInputDragged;
        InputSignals.Instance.onInputReleased += OnInputReleased;
        
    }
    private void UnsubscribeEvents()
    {
        
        InputSignals.Instance.onInputDragged -= OnInputDragged;
        InputSignals.Instance.onInputReleased -= OnInputReleased;
        
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    #endregion
    private void OnActivateMovement()
    {
        _playerMovementController.ActivateMovement();
    }

    private void OnDeactivateMovement()
    {
        _playerMovementController.DeactivateMovement();
        
    }

    private void SendPlayerDataToController()
    {
        _playerMovementController.SetMovementData(_playerData.playerMovementData);
    }

    private void OnInputDragged(HorizontalInputParams horizontalInput)
    {
        _playerMovementController.SetSideForces(horizontalInput);
    }
    private void OnInputReleased()
    {
        _playerMovementController.SetSideForces(0);
    }

    private void OnPlayerAndObstacleCrash()
    {
        _playerMovementController.PushPlayerBack();
    }
   
}
