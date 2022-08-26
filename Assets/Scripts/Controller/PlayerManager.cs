using Controller;
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
    [SerializeField] PlayerAnimationController playerAnimationController;
    #endregion
    #region private vars
    PlayerMovementController _playerMovementController;
    private PlayerData _playerData;
    #endregion
    #endregion

    private void Awake()
    {
        _playerMovementController = GetComponent<PlayerMovementController>();
        _playerData = GetPlayerData();
        SendPlayerDataToController();
    }

    private PlayerData GetPlayerData() => Resources.Load<CD_Player>("Datas/UnityObjects/CD_Player").Data;
    #region Event Subsicription

    void OnEnable()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        CoreGameSignals.instance.onPlay += OnActivateMovement;
        InputSignals.Instance.onInputDragged += OnInputDragged;
        InputSignals.Instance.onInputReleased += OnInputReleased;
        
    }
    private void UnsubscribeEvents()
    {
        CoreGameSignals.instance.onPlay -= OnActivateMovement;
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
