using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class Door : MonoBehaviour, IDoor
{
    [FoldoutGroup("BasicConfig")][SerializeField] Vector3 rotationAxis = new Vector3(0, 1, 0);
    [FoldoutGroup("BasicConfig")][SerializeField] float rotationAngle = -90;
    [FoldoutGroup("BasicConfig")] public bool colliderWhileMoving = false;
    [FoldoutGroup("BasicConfig")] public float moveSpeed = 90;
    
    [Range(0, 1)][SerializeField] float beginOpenChance = 0;
    
    [FoldoutGroup("Debug")][ShowInInspector] Quaternion closedRotation;
    [FoldoutGroup("Debug")][ShowInInspector] Quaternion openRotation;
    [FoldoutGroup("Debug")][ShowInInspector] Collider[] colliders;
    [FoldoutGroup("Debug")][ShowInInspector] Quaternion targetRotation;

    [FoldoutGroup("Events")] public UnityEvent onOpen;
    [FoldoutGroup("Events")] public UnityEvent onClose;

    void Awake(){
        colliders = GetComponentsInChildren<Collider>();
    }
    private void Start()
    {
        closedRotation = transform.rotation;
        openRotation = closedRotation * Quaternion.AngleAxis(rotationAngle, rotationAxis);
        Close();
        if (Random.value < beginOpenChance){
            Open();
        }
    }
    
    DoorState GetState() {
        if (transform.rotation == targetRotation){
            return transform.rotation == closedRotation ? DoorState.Closed : DoorState.Open;
        }
        return DoorState.Moving;
    }
    void Update()
    {
        Move();
    }

    void Move()
    {
        if (transform.rotation == targetRotation){
            return;
        }
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, moveSpeed * Time.deltaTime);
        foreach (var collider in colliders)
        {
            collider.enabled = GetState() != DoorState.Moving || colliderWhileMoving;
        }
    }

    [FoldoutGroup("Debug")][Button]
    public void Open()
    {
        targetRotation = openRotation;
        onOpen.Invoke();
    }
    [FoldoutGroup("Debug")][Button]
    public void Close()
    {
        targetRotation = closedRotation;
        onClose.Invoke();
    }

    public void ChangeState()
    {
        if (GetState() == DoorState.Closed){
            Open();
        } else if (GetState() == DoorState.Open){
            Close();
        }
    }
}

enum DoorState
{
    Open,
    Closed,
    Moving
}
