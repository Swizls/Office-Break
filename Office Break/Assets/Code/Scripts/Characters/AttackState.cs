using UnityEngine;

namespace OfficeBreak.Characters.Animations 
{
    public class AttackState : StateMachineBehaviour
    {
        [SerializeField][Range(0f, 1f)] private float _attackCooldownDuration;

        private AnimatorController _animatorController;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _animatorController = animator.GetComponent<AnimatorController>();
        }

        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (animator.IsInTransition(layerIndex)) 
                return;

            if (stateInfo.normalizedTime < _attackCooldownDuration)
                return;

            _animatorController.AttackAnimationEnded?.Invoke();
        }
    }
}
