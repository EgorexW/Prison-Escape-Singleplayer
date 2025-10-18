using Sirenix.OdinInspector;
using StarterAssets;
using UnityEngine;
using UnityEngine.Events;

public partial class Player
{
    [Tooltip("Move speed of the character in m/s")] public float moveSpeed = 4.0f;

    [Tooltip("Sprint speed of the character in m/s")] public float sprintSpeedMod = 1.5f;

    [BoxGroup("Stamina")] [SerializeField] float staminaUseRate = 0.15f;
    [BoxGroup("Stamina")] [SerializeField] float staminaRegenRate = 0.1f;

    [BoxGroup("Fov")] [SerializeField] float normalFov = 70;
    [BoxGroup("Fov")] [SerializeField] float sprintFov = 90f;
    [FoldoutGroup("Events")] public UnityEvent<Vector3> onMove;

    CharacterController characterController;
    FirstPersonController firstPersonController;
    float speedMod = 1;

    [BoxGroup("Stamina")] [ShowInInspector] public float Stamina{ get; private set; } = 1;

    void SetFirstPersonController()
    {
        if (firstPersonController == null){
            return;
        }
        firstPersonController.onMove.AddListener(onMove.Invoke);
        firstPersonController.getMoveData = GetMoveData;
    }

    MoveData GetMoveData(bool sprint)
    {
        var moveData = new MoveData();
        moveData.speed = moveSpeed * speedMod;
        moveData.fov = normalFov;
        if (!sprint){
            AddStamina(staminaRegenRate * Time.deltaTime);
            return moveData;
        }
        var canSprint = Stamina > 0;
        if (canSprint){
            moveData.speed *= sprintSpeedMod;
            moveData.fov = sprintFov;
            Stamina -= staminaUseRate * Time.deltaTime;
        }
        return moveData;
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public void Spawn(Vector3 position, Quaternion rotation)
    {
        // Debug.Log("SetPosLocal pos " + position);
        characterController.enabled = false;
        transform.position = position;
        transform.rotation = rotation;
        AudioListener.pause = false;
        characterController.enabled = true;
    }

    public void ModSpeed(float mod)
    {
        speedMod *= mod;
    }

    public LevelNode GetContainingNode()
    {
        var colliders = new Collider[1];
        Physics.OverlapBoxNonAlloc(transform.position, 0.1f * Vector3.one, colliders, Quaternion.identity,
            LayerMask.GetMask("Level Node"));
        var node = General.GetComponentFromCollider<LevelNode>(colliders[0]);
        if (!node){
            Debug.LogWarning("Player node is null", this);
        }
        return node;
    }

    public void AddStamina(float effectStaminaPerSecond)
    {
        Stamina += effectStaminaPerSecond;
        Stamina = Mathf.Min(1, Stamina);
    }
}