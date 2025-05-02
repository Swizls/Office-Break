using UnityEngine;

namespace OfficeBreak.Characters.Animations 
{
    public class AttackState : StateMachineBehaviour
    {
        [SerializeField][Range(0f, 1f)] private float _attackCooldownDuration;

        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (animator.IsInTransition(layerIndex)) 
                return;

            if (stateInfo.normalizedTime < _attackCooldownDuration)
                return;

            animator.GetComponent<AnimatorController>().AttackAnimationEnded?.Invoke();
        }
    }
}
