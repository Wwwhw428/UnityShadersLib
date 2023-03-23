using UnityEngine;

namespace MyCell.CoreSystem.CoreComponent
{
    public class Movement : CoreComponent
    {
        public Vector2 CurrentVelocity { get; private set; }
        public int CurrentFaceDirection { get; private set; }

        public bool CanSetVelocity { get; set; }

        private Rigidbody2D _rb;

        private Vector2 _vector2WorkSpace;

        #region Unity Callback Function
        protected override void Awake()
        {
            base.Awake();
            _rb = GetComponentInParent<Rigidbody2D>();
            CurrentFaceDirection = 1;
            CanSetVelocity = true;
        }
        #endregion

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            CurrentVelocity = _rb.velocity;
        }

        #region Set Function
        public void SetVelocityX(float velocity)
        {
            _vector2WorkSpace.Set(velocity, CurrentVelocity.y);

            SetFinalVelocity();
        }
        public void SetVelocityY(float velocity)
        {
            _vector2WorkSpace = new Vector2(CurrentVelocity.x, velocity);

            SetFinalVelocity();
        }

        public void SetVelocity(float velocityX, float velocityY)
        {
            _vector2WorkSpace.Set(velocityX, velocityY);

            SetFinalVelocity();
        }
        public void SetVelocity(float velocity, Vector2 angle, int direction)
        {
            angle.Normalize();
            _vector2WorkSpace.Set(angle.x * velocity * direction, angle.y * velocity);

            SetFinalVelocity();
        }

        private void SetFinalVelocity()
        {
            if (CanSetVelocity)
            {
                _rb.velocity = _vector2WorkSpace;
                CurrentVelocity = _vector2WorkSpace;
            }
        }
        #endregion

        #region Check Function
        public void CheckIfShouldFlip(int direction)
        {
            if (direction != 0 && direction != CurrentFaceDirection)
                Flip();
        }
        #endregion

        #region Other Function
        public void Flip()
        {
            _rb.transform.Rotate(0f, 180f, 0f);
            CurrentFaceDirection *= -1;
        }
        #endregion

    }
}
