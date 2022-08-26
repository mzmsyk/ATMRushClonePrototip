using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollectableSignals : MonoBehaviour
{
    #region singleton
    public static CollectableSignals Instance;
    #endregion
    #region singleton Awake
    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    #endregion

    public UnityAction<Transform> onCollectableAndCollectableCollide = delegate { };
    public UnityAction<Transform> onCollectableAndObstacleCollide = delegate { };
    public UnityAction<Transform> onCollectableUpgradeCollide = delegate { };
    public UnityAction<Transform> onCollectableATMCollide = delegate { };
    public UnityAction<Transform> onCollectableWalkingPlatformCollide = delegate { };
    public UnityAction<Transform> onCollectableWinZoneCollide = delegate { };
    public UnityAction<GameObject> onMiniGameStackCollected = delegate { };



}
