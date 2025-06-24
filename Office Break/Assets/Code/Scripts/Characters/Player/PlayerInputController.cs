using FabroGames.PlayerControlls;
using OfficeBreak.Characters.FightingSystem;
using OfficeBreak.InteractionSystem;
using UnityEngine;
using UnityEngine.InputSystem;

namespace OfficeBreak.Characters
{
    public class PlayerInputController : MonoBehaviour
    {
        private ItemHolder _itemHolder;
        private PlayerAttackController _attackController;
        private Interactor _interactor;

        private PlayerInputActions _inputActions;

        #region MONO

        private void Awake()
        {
            _itemHolder = GetComponent<ItemHolder>();
            _attackController = GetComponent<PlayerAttackController>();
            _interactor = GetComponent<Interactor>();
        }

        private void OnEnable()
        {
            _inputActions = new PlayerInputActions();

            _inputActions.Enable();
            _inputActions.Player.Interact.performed += OnInteractionKeyPress;
            _inputActions.Player.Drop.performed += OnDropKeyPress;
            _inputActions.Player.Attack.performed += OnAttackKeyPress;

            _itemHolder.ItemPickedUp += OnItemPickUp;
        }

        private void OnDisable()
        {
            _itemHolder.ItemPickedUp -= OnItemPickUp;
            _itemHolder.ItemDropped -= OnItemDrop;

            _inputActions.Player.Interact.performed -= OnInteractionKeyPress;
            _inputActions.Player.Drop.performed -= OnDropKeyPress;
            _inputActions.Player.Disable();
        }

        #endregion

        #region INPUT_CALLBACKS

        private void OnInteractionKeyPress(InputAction.CallbackContext context) => _interactor.Interact();

        private void OnDropKeyPress(InputAction.CallbackContext context) => _itemHolder.Drop();

        private void OnAttackKeyPress(InputAction.CallbackContext context)
        {
            if (!_itemHolder.IsCarringItem)
                return;

            _itemHolder.Throw();
        }

        #endregion

        private void OnItemPickUp()
        {
            SetFightingInputEnable(false);
            _itemHolder.ItemDropped += OnItemDrop;
        }

        private void OnItemDrop()
        {
            SetFightingInputEnable(true);
            _itemHolder.ItemDropped -= OnItemDrop;
        }

        private void SetFightingInputEnable(bool flag) => _attackController.enabled = flag;
    }
}