using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ObstacleAnimController : MonoBehaviour
{
    #region Variables
    [Header("Bool")]
    [SerializeField] private bool doMove = true;
    [SerializeField] private bool doShake = false;
    [SerializeField] private bool doRotate = false;
    [SerializeField] private bool piganim = false;

    [Space]
    [Header("Move")]
    [Space]
    [SerializeField] private Ease easeStart = Ease.Linear;
    [SerializeField] private Ease easeEnd = Ease.Linear;
    [Header("Path")]
    [Space]
    [SerializeField] private Vector3[] path = new Vector3[2];
    [Header("Speed")]
    [Space]
    [SerializeField] private float speedStart = 1;
    [SerializeField] private float speedEnd = 1;
    [Header("Delay")]
    [SerializeField] private float firstDelay = 0;
    [SerializeField] private float secondDelay = 0;

    [Header("Shake")]
    [Space]
    [SerializeField] private float shakeSpeed = 1;

    [Header("Rotate")]
    [Space]
    [SerializeField] private float rotateSpeed;



    private Sequence _sequence;
    #endregion

    #region EventSubscription
    private void Start()
    {
        SubscribeEvents();
    }
    private void OnEnable()
    {
        _sequence = DOTween.Sequence();

        if (doMove)
            MoveAnimation();
        if (doShake)
            ShakeAnim();
        if (doRotate)
            RotateAnim();
        if (piganim)
            PigAnim();

        SetLoop();

        SetPlay();

        //SubscribeEvents();
    }

    private void OnDisable()
    {
        UnSubscribeEvents();
    }

    private void SubscribeEvents()
    {
        CoreGameSignals.instance.onNextLevel += SetPlay;
        CoreGameSignals.instance.onGameEnd += KillAnim;
    }

    private void UnSubscribeEvents()
    {
        CoreGameSignals.instance.onNextLevel -= SetPlay;
        CoreGameSignals.instance.onGameEnd -= KillAnim;
    }

    #endregion

    public void MoveAnimation()
    {
        _sequence.Append(transform.DOLocalMove(path[0], (1 / speedStart), false).SetEase(easeStart)
            .SetDelay(firstDelay))
            .Append(transform.DOLocalMove(path[1], (1 / speedEnd), false).SetEase(easeEnd)
            .SetDelay(secondDelay));
    }

    private void ShakeAnim()
    {
        _sequence.Append(transform.DOShakePosition(1 / shakeSpeed, .5f));
    }


    private void RotateAnim()
    {
        _sequence.Append(transform.DORotate(new Vector3(0, 360, 0), 1 / rotateSpeed, RotateMode.FastBeyond360));
    }


    private void SetLoop()
    {
        _sequence.SetLoops(-1, LoopType.Restart);
    }

    private void KillAnim()
    {
        _sequence.Kill();
    }

    private void SetPlay()
    {
        _sequence.Play();
    }

    private void PigAnim()
    {
        transform.DOMove(path[0], 1 / speedStart).SetEase(easeStart).SetLoops(-1, LoopType.Yoyo);
        transform.DORotate(new Vector3(0, 360, 0), 1 / rotateSpeed, RotateMode.FastBeyond360)
            .SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);

    }
}

