using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Keys;

public class PlayerMovementController : MonoBehaviour
{
    #region private vars
    Rigidbody _rig;
    private bool _isReadyToMove=false;
    private PlayerMovementData _playerMovementData;
    private float _horizontalInput = 0f;
    private float _clamp = 0f;
    [SerializeField] private FixedJoystick joystick;
    #endregion
    void Start()
    {
        _rig = GetComponent<Rigidbody>();
        _horizontalInput = joystick.Horizontal;
    }

    
    void FixedUpdate()
    {
        if (_isReadyToMove)
        {
            Move();
        }
        else
        {
            Stop();
        }
    }
    public void ActivateMovement()
    {
        _isReadyToMove = true;
    }
    public void DeactivateMovement()
    {
        _isReadyToMove = false;
    }
    private void Move()
    {
        //_rig.velocity = new Vector3(_horizontalInput * _playerMovementData.sideWaySpeed,
        //    _rig.velocity.y,
        //    _playerMovementData.forwardSpeed);
        _rig.velocity = new Vector3(_horizontalInput*2,
            _rig.velocity.y,
            5);
    }
    private void Stop()
    {
        _rig.velocity = Vector3.zero;
    }
}
