using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wwwhw.SO.Player
{
    [CreateAssetMenu(fileName = "PlayerData_SO", menuName = "UnityShderLib/PlayerData_SO", order = 0)]
    public class PlayerData_SO : ScriptableObject
    {
        # region velocity
        public float CrouchMovementVelocity { get; private set; }
        public float MovementVelocity { get; private set; }
        public float RunMovementVelocity { get; private set; }
        # endregion

        # region collider
        public float CrouchColliderHeight { get; private set; }
        public float StandColliderHeigh { get; private set; }
        public float CeilingCheckRadius { get; private set; }
        # endregion

        # region jump
        public float JumpVelocity { get; private set; }
        public int AmountOfJump { get; private set; }
        # endregion

        # region check transform
        public Transform WhatIsGround { get; private set; }
        # endregion
    }
}
