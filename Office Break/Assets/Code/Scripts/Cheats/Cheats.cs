using IngameDebugConsole;
using OfficeBreak.Core;
using OfficeBreak.DestructionSystem;
using System.Linq;
using UnityEngine;

namespace OfficeBreak.Cheats
{
    public class Cheats
    {
        [ConsoleMethod("destroy", "destroys all destructables on level")]
        public static void DestroyAllDestructables()
        {
            HitData hitData = new HitData()
            {
                Damage = 1000000,
                HitDirection = Vector3.zero,
                AttackForce = 0
            };

            Object.FindObjectsByType<Destructable>(FindObjectsSortMode.None)
                .ToList()
                .ForEach(destructable => destructable.TakeHit(hitData));
        }
    }
}
