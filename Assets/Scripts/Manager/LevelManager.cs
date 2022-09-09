using System;
using Controllers;
using Data.UnityObject;
using Data.ValueObject;
//using Extentions;
using Keys;
using Signals;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Managers
{
    public class LevelManager : MonoBehaviour
    {


        #region Self Variables

        #region Public Variables

        [Header("Data")] public LevelData Data;

        [HideInInspector] public int LevelID => _levelID;

        #endregion

        #region Serialized Variables

        [Space] [SerializeField] private GameObject levelHolder;
        [SerializeField] private LevelLoaderCommand levelLoader;
        [SerializeField] private ClearActiveLevelCommand levelClearer;

        //[SerializeField] private ScoreManager _scoreManager;


        #endregion

        #region Private Variables

        [ShowInInspector] private int _levelID;
        [ShowInInspector] private int _score;


        #endregion

        #endregion

        protected void Awake()
        {
            _levelID = GetActiveLevel();
            OnInitializeLevel();
        }

        private int GetActiveLevel()
        {
            return ScoreSignals.Instance.loadSavedLevelValue();
        }



        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.instance.onLevelInitialize += OnInitializeLevel;
            CoreGameSignals.instance.onClearActiveLevel += OnClearActiveLevel;
            CoreGameSignals.instance.onNextLevel += OnNextLevel;
            CoreGameSignals.instance.onRestartLevel += OnRestartLevel;
            ScoreSignals.Instance.onCompleteScore += OnComplateScore;
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.instance.onLevelInitialize -= OnInitializeLevel;
            CoreGameSignals.instance.onClearActiveLevel -= OnClearActiveLevel;
            CoreGameSignals.instance.onNextLevel -= OnNextLevel;
            CoreGameSignals.instance.onRestartLevel -= OnRestartLevel;
            ScoreSignals.Instance.onCompleteScore -= OnComplateScore;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion



        private void OnNextLevel()
        {
            _levelID++;
            CoreGameSignals.instance.onClearActiveLevel?.Invoke();
            CoreGameSignals.instance.onReset?.Invoke();
            CoreGameSignals.instance.onSaveGameData?.Invoke(new SaveGameDataParams()
            {
                Level = _levelID,
                Money = _score
                //Money = _scoreManager.totalScore
            });
            CoreGameSignals.instance.onLevelInitialize?.Invoke();
        }

        private void OnRestartLevel()
        {
            CoreGameSignals.instance.onClearActiveLevel?.Invoke();
            CoreGameSignals.instance.onReset?.Invoke();
            CoreGameSignals.instance.onSaveGameData?.Invoke(new SaveGameDataParams()
            {
                Level = _levelID
            });
            CoreGameSignals.instance.onLevelInitialize?.Invoke();
        }

        private void OnInitializeLevel()
        {
            UnityEngine.Object[] Levels = Resources.LoadAll("Levels");
            var newLevelData = _levelID % Levels.Length;
            levelLoader.InitializeLevel(newLevelData, levelHolder.transform);
            CoreGameSignals.instance.onCameraInitialized?.Invoke();
        }

        private void OnClearActiveLevel()
        {
            levelClearer.ClearActiveLevel(levelHolder.transform);
        }

        private void OnComplateScore(float score)
        {
            _score = Convert.ToInt32(score);
        }
    }
}