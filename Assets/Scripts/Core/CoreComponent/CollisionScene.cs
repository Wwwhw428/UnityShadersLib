using System.ComponentModel;
using System.Diagnostics;
using UnityEngine;
using Wwwhw.Generics;

namespace Wwwhw.CoreSystem.Component
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
        public Transform CeilingCheck
        {
            get => GenericNotImplementedError<Transform>.TryGet(_ceilingCheck, core.transform.parent.name);
            private set => _ceilingCheck = value;
        }
        public float GroundCheckRadius { get => _groundCheckRadius; set => _groundCheckRadius = value; }
        public LayerMask WhatIsGround { get => _whatIsGround; set => _whatIsGround = value; }

        [SerializeField] private Transform _groundCheck;
        [SerializeField] private Transform _ceilingCheck;
        [SerializeField] private LayerMask _whatIsGround;
        [SerializeField] private float _groundCheckRadius;

        #endregion

        #region CheckFunction

        public bool Ground
        {
            get => Physics.OverlapSphere(GroundCheck.position, GroundCheckRadius, WhatIsGround).Length > 0 ? true : false;
        }

        public bool Ceiling
        {
            get => Physics.OverlapSphere(CeilingCheck.position, GroundCheckRadius, WhatIsGround).Length > 0 ? true : false;
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
