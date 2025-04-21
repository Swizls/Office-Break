using OfficeBreak.Characters.Enemies;

namespace OfficeBreak.Characters
{
    public class EnemyRagdollController : RagdollController
    {
        private EnemyMover _movement;
        private Enemy _enemy;

        protected override void Initialize()
        {
            _movement = GetComponentInParent<EnemyMover>();
            _enemy = GetComponentInParent<Enemy>();

            base.Initialize();
        }

        public override void EnableRagdoll()
        {
            base.EnableRagdoll();

            _movement.enabled = false;
            _enemy.enabled = false;
        }

        public override void DisableRagdoll()
        {
            base.DisableRagdoll();

            _movement.enabled = true;
            _enemy.enabled = true;
        }
    }
}