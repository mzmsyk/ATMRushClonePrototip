using Enum;
using UnityEngine;


namespace Controller
{
    public class PlayerAnimationController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private Animator animator;
        [SerializeField] private AnimationStates animationStates;

        #endregion
        #endregion

        private void Awake()
        {
            StartIdleAnim();
            animator = GetComponent<Animator>();
        }
        private void Start()
        {
            SubscribeEvents();
        }

        #region EventSubsicription
        private void OnEnable()
        {
            //SubscribeEvents();
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.instance.onPlay += StartRunAnim;
            CoreGameSignals.instance.onLevelSuccessful += StartFinishAnim;
        }
        private void UnsubscribeEvents()
        {
            CoreGameSignals.instance.onPlay -= StartRunAnim;
            CoreGameSignals.instance.onLevelSuccessful -= StartFinishAnim;
        }
        #endregion

        #region Animation State Change

        private void ChangeAnimationData(AnimationStates animationStates)
        {
            this.animationStates = animationStates;
        }

        public void StartIdleAnim()
        {
            ChangeAnimationData(AnimationStates.Idle);
            ResetAllAnims();
            //animator.SetBool("Idle", true); //butonlar eklendiðinde aktif olacaktýr...
            animator.SetBool("Run", true);
        }

        public void StartRunAnim()
        {
            ChangeAnimationData(AnimationStates.Run);
            ResetAllAnims();
            animator.SetBool("Run", true);
        }

        public void StartFinishAnim()
        {
            ChangeAnimationData(AnimationStates.Finish);
        }

        public void ResetAllAnims()
        {
            animator.SetBool("Idle", false);
            animator.SetBool("Run", false);
        }
        #endregion


    }
}

