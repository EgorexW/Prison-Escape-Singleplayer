using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class DoorObsolete : MonoBehaviour, IDoor, IInteractive
{
    [FoldoutGroup("BasicConfig")][SerializeField] Vector3 rotationAxis = new Vector3(0, 1, 0);
    [FoldoutGroup("BasicConfig")][SerializeField] float rotationAngle = -90;
    [FoldoutGroup("BasicConfig")] public bool colliderWhenOpen = false;
    [FoldoutGroup("BasicConfig")] public float moveTime = 0.75f;
    [FoldoutGroup("BasicConfig")] public float holdDuration = 0;
    
    [Range(0, 1)][SerializeField] float beginOpenChance = 0;
    [SerializeField] Optional<float> autoCloseTime;
    [SerializeField] AccessLevel accessLevel;
    [SerializeField] bool pernamentUnlock = true;
    [SerializeField] Discovery discoveryOnUnlock;
    
    public bool LockState{ get; set; }
    
    float lastMoveStartedTime = -Mathf.Infinity;
    Quaternion startRotation;
    Collider[] colliders;

    [FoldoutGroup("Events")] public UnityEvent onOpen;
    [FoldoutGroup("Events")] public UnityEvent onNoAccess;
    bool Opened => startRotation != transform.rotation;
    public float HoldDuration => holdDuration;

    void Awake(){
        colliders = GetComponentsInChildren<Collider>();
    }
    private void Start()
    {
        startRotation = transform.rotation;
        if (Random.value < beginOpenChance){
            Open();
        }
    }
    void Update()
    {
        ResolveAutoClose();
    }

    void ResolveAutoClose()
    {
        if (!autoCloseTime){
            return;
        }
        if (Time.time - autoCloseTime - moveTime < lastMoveStartedTime){
            return;
        }
        Close();
    }

    bool CanChangeState(){
        if (Time.time - lastMoveStartedTime < moveTime)
        {
            return false;
        }
        return !LockState;
    }
    
    [Button]
    private void ChangeState()
    {
        if (Opened)
        {
            Close();
            return;
        }
        Open();
    }

    public void Open(){
        if (!CanChangeState() || Opened){
            return;
        }
        onOpen.Invoke();
        Rotate(true);
    }

    public void Close(){
        if (!CanChangeState() || !Opened){
            return;
        }
        Rotate(false);
    }

    private void Rotate(bool open)
    {
        LeanTween.cancel(gameObject);
        lastMoveStartedTime = Time.time;
        var angle = rotationAngle;
        if (!open){
            angle = -angle;
        }
        transform.LeanRotateAroundLocal(rotationAxis.normalized, angle, moveTime);
        if (colliderWhenOpen){
            return;
        }
        // ReSharper disable once LocalVariableHidesMember
        foreach (var collider in colliders)
        {
            collider.isTrigger = open;
        }
    }

    public void Interact(Player player){
        if (!CanCharacterUse(player)){
            onNoAccess.Invoke();
            return;
        }
        ChangeState();
    }
    public bool CanCharacterUse(Player player)
    {
        if (accessLevel == null){
            return true;
        }
        var item = player.GetHeldItem();
        if (item is not IKeycard keycard){
            return false;
        }
        if (!keycard.HasAccess(accessLevel)){
            return false;
        }
        if (pernamentUnlock){
            accessLevel = null;
        }
        
        return true;
    }
}
