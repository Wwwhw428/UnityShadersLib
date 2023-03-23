using UnityEngine;

namespace MyCell.CoreSystem.CoreComponent
{
    public class Combat : CoreComponent
    {
        private Stats Stats
        {
            get => _stats ?? core.GetCoreComponent(ref _stats);
        }
        private Movement Movement
        {
            get => _movement ?? core.GetCoreComponent(ref _movement);
        }

        private Stats _stats;
        private Movement _movement;

        [SerializeField] private float _maxKnockbackTime = 0.2f;

        private bool _isKnockbackActive;
        private float _knockbackStartTime;

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            CheckKnockback();
        }

        public void Damage(int amount)
        {
            Debug.Log(core.transform.parent.name + "Damaged!");
            Stats?.Health.Decrease(amount);
        }

        private void CheckKnockback()
        {
            if (_isKnockbackActive
                && ((Movement?.CurrentVelocity.y <= 0.01f)
                    || Time.time >= _knockbackStartTime + _maxKnockbackTime)
               )
            {
                _isKnockbackActive = false;
                Movement.CanSetVelocity = true;
            }
        }
    }
}

