using FabroGames.PlayerControlls;
using UnityEngine;
using UnityEngine.InputSystem;

namespace OfficeBreak.Characters.FightingSystem
{
    public abstract class AttackAction : ScriptableObject
    {
        [field: SerializeField] protected float AttackDamage { get; private set; }
        [field: SerializeField] protected float AttackForce { get; private set; }

        public abstract void PerformAction();
    }

    public abstract class PlayerAttackAction : AttackAction
    {
        [SerializeField] private InputActionReference[] _requiredActions;

        [SerializeField] protected AnimationClip AnimationClip;
        [SerializeField] protected LayerMask HitablesLayer;

        public abstract void Initialize(GameObject palyer);

        public bool IsRequiredKeysPressed()
        {
            foreach(var action in _requiredActions)
            {
                if(_requiredActions.Length > 1)
                {
                    if (!action.action.IsPressed()) ;
                        return false;
                }
                else
                {
                    if (!action.action.WasPressedThisFrame())
                        return false;
                }

            }

            return true;
        }
    }
}