using UnityEngine;

namespace OfficeBreak.InteractionSystem
{
    public class PickupStrategy : InteractionStrategy
    {
        private Item _item;

        public PickupStrategy(Interactor interactor, Item item) : base(interactor)
        {
            _item = item;
        }

        public override void Execute()
        {
            Debug.Log("Executing pickup strategy");
            Interactor.GetComponent<ItemHolder>().Pickup(_item);
        }
    }
}