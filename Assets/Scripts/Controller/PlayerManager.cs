using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region vars
    PlayerMovementController _playerMovementController;
    private PlayerData _playerData;
    #endregion
    private void Awake()
    {
        _playerMovementController = GetComponent<PlayerMovementController>();
    }
    void OnEnable()
    {
        OnActivateMovement();
    }
    private void OnDisable()
    {
        OnDeactivateMovement();
    }
    private void OnActivateMovement()
    {
        _playerMovementController.ActivateMovement();
    }
    private void OnDeactivateMovement()
    {
        _playerMovementController.DeactivateMovement();
        
    }
}
