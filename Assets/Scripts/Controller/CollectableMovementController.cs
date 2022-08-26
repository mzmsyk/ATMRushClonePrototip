using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableMovementController : MonoBehaviour
{
    #region vars
    public Transform ConnectedNode;
    private bool _isReadyToMove = false;
    private CollectableData collectableData;
    private CollectableManager collectableManager;
    private Rigidbody _rig;
    private Sequence _sequence;
    #endregion
    private void Awake()
    {
        _rig = GetComponent<Rigidbody>();
        ConnectedNode = transform;
    }
    private void FixedUpdate()
    {
        if (!_isReadyToMove)
        {
            Stop();
        }

    }
    public void DeactivateMovement()
    {
        _isReadyToMove = false;
    }

    private void Stop()
    {
        _rig.velocity = Vector3.zero;
    }
    public void MoveToWinZone()
    {
        if (_sequence==null)
        {
            _sequence = DOTween.Sequence();
            _sequence.Append(transform.DOMove(new Vector3(-8, _rig.position.y, _rig.position.z), .75f));
        }
        ConnectedNode = null;
        _sequence.Play();
    }
    public void StopMoveToWinZone()
    {
        if (_sequence != null)
            _sequence.Kill();
    }
}
