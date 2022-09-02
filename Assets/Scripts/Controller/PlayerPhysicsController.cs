using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhysicsController : MonoBehaviour
{
    #region Self Variables

    #region Serialized Variables


    #endregion

    #endregion

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            PlayerSignals.Instance.onPlayerAndObstacleCrash?.Invoke();
        }
        if (other.CompareTag("ATM"))
        {
            PlayerSignals.Instance.onPlayerAndATMCrash?.Invoke(other.transform);
        }

        if (other.CompareTag("FinishFlag"))
        {
            PlayerSignals.Instance.onPlayerEnterFinishLine?.Invoke();
            CoreGameSignals.instance.onLevelSuccessful?.Invoke();
        }
    }
}
