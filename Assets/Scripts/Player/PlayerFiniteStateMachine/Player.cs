using Wwwhw.CoreSystem;
using Wwwhw.CoreSystem.Component;
using UnityEngine;
using Wwwhw.SO.Player;

public class Player : MonoBehaviour
{
    #region State Variable
    private PlayerStateMachine _stateMachine;
    // InAir State
    public PlayerInAirState InAirState { get; private set; }
    // Ability State
    public PlayerJumpState JumpState { get; private set; }
    // Ground State
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerLandState LandState { get; private set; }
    public PlayerCrouchIdleState CrouchIdleState { get; private set; }
    public PlayerCrouchMoveState CrouchMoveState { get; private set; }

    [SerializeField]
    private PlayerData_SO _playerData;
    #endregion

    #region Component
    public Core Core { get; private set; }
    public Animator Anim { get; private set; }
    [HideInInspector]
    public PlayerInputHandler InputHandler;
    public BoxCollider2D MovementCollider { get; private set; }

    #endregion

    #region Other Variable
    private Vector2 _vector2WorkSpace;
    #endregion

    #region  Unity Callback
    private void Awake()
    {
        _stateMachine = new PlayerStateMachine();

        Anim = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        MovementCollider = GetComponent<BoxCollider2D>();
        Core = GetComponentInChildren<Core>();

        // InAir State
        InAirState = new PlayerInAirState(this, _stateMachine, _playerData, "InAir");
        // Ability State
        JumpState = new PlayerJumpState(this, _stateMachine, _playerData, "InAir");
        // Ground State
        IdleState = new PlayerIdleState(this, _stateMachine, _playerData, "Idle");
        MoveState = new PlayerMoveState(this, _stateMachine, _playerData, "Move");
        LandState = new PlayerLandState(this, _stateMachine, _playerData, "Land");
        CrouchIdleState = new PlayerCrouchIdleState(this, _stateMachine, _playerData, "CrouchIdle");
        CrouchMoveState = new PlayerCrouchMoveState(this, _stateMachine, _playerData, "CrouchMove");
    }

    // Start is called before the first frame update
    private void Start()
    {
        _stateMachine.Initialize(IdleState);
    }

    // Update is called once per frame
    private void Update()
    {
        Core.LogicUpdate();
        _stateMachine.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        _stateMachine.CurrentState.PhysicsUpdate();
    }
    #endregion

    #region Set Function
    public void SetColliderHeight(float height)
    {
        Vector2 center = MovementCollider.offset;
        _vector2WorkSpace.Set(MovementCollider.size.x, height);
        center.y += (height - MovementCollider.size.y) / 2;

        MovementCollider.offset = center;
        MovementCollider.size = _vector2WorkSpace;
    }
    #endregion

    #region Other Funtion

    public void AnimationTrigger() => _stateMachine.CurrentState.AnimationTrigger();

    public void AnimationFinishedTrigger() => _stateMachine.CurrentState.AnimationFinishedTrigger();

    #endregion
}