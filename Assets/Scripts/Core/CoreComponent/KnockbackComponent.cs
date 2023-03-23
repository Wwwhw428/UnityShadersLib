using UnityEngine;
using MyCell.Modifiers;
using MyCell.Interfaces;

namespace MyCell.CoreSystem.CoreComponent
{
    public class KnockbackComponent : CoreComponent, IKnockbackable
    {
        [SerializeField] private float maxKnockbackTime = 0.2f;

        private Movement Movement => _movement ?? core.GetCoreComponent(ref _movement);
        private Movement _movement;

        private CollisionScene _collisionSenses;
        private CollisionScene CollisionSenses => _collisionSenses ?? core.GetCoreComponent(ref _collisionSenses);

        private bool _isKnockbackActive;
        private float _knockbackStartTime;

        public ModifierContainer<KnockbackModifiers, KnockbackData> KnockbackModifiers { get; private set; } =
            new ModifierContainer<KnockbackModifiers, KnockbackData>();

        public override void LogicUpdate()
        {
            CheckKnockback();
        }

        public void Knockback(KnockbackData data)
        {
            var modifiedData = KnockbackModifiers.ApplyModifiers(data);

            Movement?.SetVelocity(modifiedData.Strength, modifiedData.Angle.normalized, modifiedData.Direction);
            Movement.CanSetVelocity = false;
            _isKnockbackActive = true;
            _knockbackStartTime = Time.time;
        }

        private void CheckKnockback()
        {
            if (_isKnockbackActive && Movement.CurrentVelocity.y <= 0.01f &&
                (CollisionSenses.Ground || Time.time >= _knockbackStartTime + maxKnockbackTime))
            {
                _isKnockbackActive = false;
                Movement.CanSetVelocity = true;
            }
        }
    }
}