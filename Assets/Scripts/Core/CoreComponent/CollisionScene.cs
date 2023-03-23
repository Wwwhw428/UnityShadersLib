using System.ComponentModel;
using System.Diagnostics;
using UnityEngine;

namespace MyCell.CoreSystem.CoreComponent
{
    public class CollisionScene : CoreComponent
    {
        private Movement Movement
        {
            get => movement ?? core.GetCoreComponent(ref movement);
        }
        private Movement movement;

        #region Check Transform

        public Transform GroundCheck
        {
            get => GenericNotImplementedError<Transform>.TryGet(_groundCheck, core.transform.parent.name);
            private set => _groundCheck = value;
        }
        public Transform WallCheck
        {
            get => GenericNotImplementedError<Transform>.TryGet(_wallCheck, core.transform.parent.name);
            private set => _wallCheck = value;
        }
        public Transform LedgeCheckHorizontal
        {
            get => GenericNotImplementedError<Transform>.TryGet(_ledgeCheckHorizontal, core.transform.parent.name);
            private set => _ledgeCheckHorizontal = value;
        }
        public Transform LedgeCheckVertical
        {
            get => GenericNotImplementedError<Transform>.TryGet(_ledgeCheckVertical, core.transform.parent.name);
            private set => _ledgeCheckVertical = value;
        }
        public Transform CeilingCheck
        {
            get => GenericNotImplementedError<Transform>.TryGet(_ceilingCheck, core.transform.parent.name);
            private set => _ceilingCheck = value;
        }

        public Transform PlatformCheck
        {
            get => GenericNotImplementedError<Transform>.TryGet(_platformCheck, core.transform.parent.name);
            private set => _platformCheck = value;
        }
        public Transform TargetRayCastCheck
        {
            get => GenericNotImplementedError<Transform>.TryGet(_targetRayCastCheck, core.transform.parent.name);
            private set => _targetRayCastCheck = value;
        }
        public Transform TargetCheckLeftCorner
        {
            get => GenericNotImplementedError<Transform>.TryGet(_targetCheckLeftCorner, core.transform.parent.name);
            private set => _targetCheckLeftCorner = value;
        }
        public Transform TargetCheckRightCorner
        {
            get => GenericNotImplementedError<Transform>.TryGet(_targetCheckRightCorner, core.transform.parent.name);
            private set => _targetCheckRightCorner = value;
        }
        public float GroundCheckRadius { get => _groundCheckRadius; set => _groundCheckRadius = value; }
        public float PlatformCheckRadius { get => _platformCheckRadius; set => _platformCheckRadius = value; }
        public float WallCheckDistance { get => _wallCheckDistance; set => _wallCheckDistance = value; }
        public float TargetRayCastCheckDistance { get => _targetRayCastCheckDistance; set => _targetRayCastCheckDistance = value; }
        // public float TargetCheckDistance { get => _targetCheckDistance; set => _targetCheckDistance = value; }
        public LayerMask WhatIsGround { get => _whatIsGround; set => _whatIsGround = value; }
        public LayerMask WhatIsTarget { get => _whatIsTarget; set => _whatIsTarget = value; }

        [SerializeField] private Transform _groundCheck;
        [SerializeField] private Transform _wallCheck;
        [SerializeField] private Transform _ledgeCheckHorizontal;
        [SerializeField] private Transform _ledgeCheckVertical;
        [SerializeField] private Transform _ceilingCheck;
        [SerializeField] private Transform _platformCheck;
        [SerializeField] private Transform _targetRayCastCheck;
        [SerializeField] private Transform _targetCheckLeftCorner;
        [SerializeField] private Transform _targetCheckRightCorner;
        [SerializeField] private float _groundCheckRadius;
        [SerializeField] private float _platformCheckRadius;
        [SerializeField] private float _wallCheckDistance;
        [SerializeField] private float _targetRayCastCheckDistance;
        // [SerializeField] private float _targetCheckDistance;
        [SerializeField] private LayerMask _whatIsGround;
        [SerializeField] private LayerMask _whatIsTarget;

        #endregion

        #region CheckFunction

        public bool Ground
        {
            get => Physics2D.OverlapCircle(GroundCheck.position, GroundCheckRadius, WhatIsGround);
        }

        public bool WallFront
        {
            get => Physics2D.Raycast(WallCheck.position, Vector2.right * Movement.CurrentFaceDirection, WallCheckDistance, WhatIsGround);
        }

        public bool Ceiling
        {
            get => Physics2D.OverlapCircle(CeilingCheck.position, GroundCheckRadius, WhatIsGround);
        }

        public bool Platform
        {
            get => Physics2D.OverlapCircle(PlatformCheck.position, PlatformCheckRadius, WhatIsGround);
        }

        public bool LedgeHorizontal
        {
            get => Physics2D.Raycast(LedgeCheckHorizontal.position, Vector2.right * Movement.CurrentFaceDirection, WallCheckDistance, WhatIsGround);
        }

        public bool LedgeVertical
        {
            get => Physics2D.Raycast(LedgeCheckVertical.position, Vector2.down, WallCheckDistance, WhatIsGround);
        }

        //public bool WallBack
        //{
        //    get => Physics2D.Raycast(WallCheck.position, Vector2.right * -Movement.FacingDirection, wallCheckDistance, whatIsGround);
        //}

        public bool TargetRaycastFront
        {
            get => Physics2D.Raycast(TargetRayCastCheck.position, Vector2.right * Movement.CurrentFaceDirection, TargetRayCastCheckDistance, WhatIsTarget);
        }

        public bool TargetOverlapArea
        {
            get => Physics2D.OverlapArea(TargetCheckLeftCorner.position, TargetCheckRightCorner.position, WhatIsTarget);
        }

        #endregion

        #region Collider Recognize

        public Collider2D Floor
        {
            get => Physics2D.OverlapCircle(GroundCheck.position, GroundCheckRadius, WhatIsGround);
        }

        #endregion
    }
}
