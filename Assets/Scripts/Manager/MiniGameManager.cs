using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Signals;
using UnityEngine;
using UnityEngine.Events;

namespace Managers
{
    public class MiniGameManager : MonoBehaviour
    {
        #region Self Variables

        #region Public


        #endregion

        #region Seriliazed Field
        [Header("Referances")]
        [SerializeField] private Transform finishPlayerTransform;
        [SerializeField] private Transform moneyHolder;
        [SerializeField] private Transform firstMoney;
        [Space]
        [Header("Adjustments")]
        [SerializeField] private float stackDistanceAmount = 1.1f;
        [SerializeField] [Range(0, 1)] private float stackUpTimeMultipler;
        [SerializeField] [Range(1, 10)] private float increaseStackUpMultiplier = 1f;
        #endregion

        #region Private Field

        private Vector3 _nextMoneyTransform;
        private List<GameObject> _Dollars = new List<GameObject>();
        public int _playerScore;//private

        #endregion

        #endregion

        private void Awake()
        {
            _nextMoneyTransform = firstMoney.position;
        }

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.instance.onLevelSuccessful += MoveFinishPlayerUp;
            CollectableSignals.Instance.onMiniGameStackCollected += StackUpCollectables;
            //ScoreSignals.Instance.onTotalScoreUpdated += OnTotalScoreUpdated;
        }


        private void UnSubscribeEvents()
        {
            CoreGameSignals.instance.onLevelSuccessful -= MoveFinishPlayerUp;
            CollectableSignals.Instance.onMiniGameStackCollected -= StackUpCollectables;
            //ScoreSignals.Instance.onTotalScoreUpdated -= OnTotalScoreUpdated;
        }


        #endregion

        public void StackUpCollectables(GameObject collectable)
        {
            _Dollars.Add(collectable);
            collectable.tag = "Untagged";
            collectable.transform.SetParent(moneyHolder);
            _nextMoneyTransform = new Vector3(_nextMoneyTransform.x, _nextMoneyTransform.y - stackDistanceAmount, _nextMoneyTransform.z);
            // collectable.transform.position = _nextMoneyTransform;
            collectable.transform.DOMove(_nextMoneyTransform, 0.1f, false);
            collectable.transform.localScale = new Vector3(1.5f, 1f, 1.5f);
        }

        private void MoveFinishPlayerUp()
        {

            StartCoroutine(Move());
        }

        private IEnumerator Move()
        {
            yield return new WaitForSeconds(1.5f);
            finishPlayerTransform.gameObject.SetActive(true);
            SetActiveAllCollectables();
            finishPlayerTransform.DOMoveY(_playerScore * 3.5f * increaseStackUpMultiplier,
                _Dollars.Count * stackUpTimeMultipler).SetEase(Ease.OutQuart).OnComplete(() =>
                {
                    CoreGameSignals.instance.onGameEnd?.Invoke();
                });
        }

        private void OnTotalScoreUpdated(int score)
        {
            _playerScore = score;
        }

        private void SetActiveAllCollectables()
        {
            foreach (var D in _Dollars)
            {
                D.SetActive(true);
                D.transform.localPosition = new Vector3(0, D.transform.localPosition.y, 0);
                D.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            }
        }
    }
}
