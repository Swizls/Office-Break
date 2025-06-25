using   IngameDebugConsole;
using System.Linq;
using UnityEngine;
using OfficeBreak.Core;
using OfficeBreak.DestructionSystem;
using OfficeBreak.Characters.Enemies.AI;

namespace OfficeBreak.Cheats
{
    public static class Cheats
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
                .Where(destructable => destructable.Health.IsDead == false)
                .ToList()
                .ForEach(destructable => destructable.TakeHit(hitData));
        }

        [ConsoleMethod("disable_ai", "")]
        public static void DisableAI()
        {
            foreach (EnemyBehaviourController item in Object.FindObjectsByType<EnemyBehaviourController>(FindObjectsSortMode.None))
            {
                item.enabled = false;
            }
        }
    }
}
