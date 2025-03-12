using Sirenix.OdinInspector;
using UnityEngine;

public class Door : MonoBehaviour, IDoor
{
    Vector3 rotationAxis = new Vector3(0, 1, 0);
    float rotationAngle = -90;
    float moveTime = 0.75f;
    bool colliderWhenOpen = false;
    
    [Range(0, 1)][SerializeField] float beginOpenChance = 0;
    [SerializeField] Optional<float> autoCloseTime;
    [SerializeField] AccessLevel accessLevel;
    
    float lastMoveStartedTime = 0;
    Quaternion startRotation;
    Collider[] colliders;
    bool lockState;
    float CurrentRotToStartRot => 0;
    bool Opened => startRotation != transform.rotation;

    void Awake(){
        colliders = GetComponentsInChildren<Collider>();
    }
    private void Start()
    {
        startRotation = transform.rotation;
        if (Random.value < beginOpenChance){
            LocalOpen();
        }
    }
    void Update()
    {
        ResolveColliders();
        ResolveAutoClose();
    }

    private void ResolveAutoClose()
    {
        if (!autoCloseTime)
        {
            return;
        }
        if (!CanChangeState())
        {
            return;
        }
        if (Time.time - autoCloseTime - moveTime < lastMoveStartedTime)
        {
            return;
        }
        LocalClose();
    }

    public void LockState(bool lockState){
        this.lockState = lockState;
    }
    private void ResolveColliders()
    {
        if (colliderWhenOpen)
        {
            return;
        }
        foreach (Collider collider in colliders)
        {
            collider.isTrigger = Opened;
        }
    }
    public bool CanChangeState(){
        if (Time.time - lastMoveStartedTime < moveTime)
        {
            return false;
        }
        if (lockState){
            return false;
        }
        return true;
    }
    
    [Button]
    private void ChangeState()
    {
        if (!CanChangeState()){
            return;
        }
        if (Opened)
        {
            LocalClose();
            return;
        }
        LocalOpen();
    }

    public void Open(){
        if (!CanChangeState()){
            return;
        }
        LocalOpen();
    }

    public void Close(){
        if (!CanChangeState()){
            return;
        }
        LocalClose();
    }
    private void LocalOpen()
    {
        if (Opened){
            return;
        }
        Rotate(rotationAngle);
    }
    private void LocalClose()
    {
        if (!Opened)
        {
            return;
        }
        Rotate(-rotationAngle);
    }

    private void Rotate(float angle)
    {
        LeanTween.cancel(gameObject);
        lastMoveStartedTime = Time.time;
        transform.LeanRotateAroundLocal(rotationAxis.normalized, angle, moveTime);
    }

    public virtual void Interact(Character character){
        if (!CanCharacterUse(character, true)){
            return;
        }
        ChangeState();
    }
    public float GetHoldDuration(){
        return 0;
    }

    public bool CanCharacterUse(Character character, bool onInteract)
    {
        if (accessLevel == null){
            return true;
        }
        Item item = character.GetHeldItem();
        if (item is not IKeycard){
            return false;
        }
        IKeycard keycard = (IKeycard)item;
        if (onInteract)
        {
            if (!keycard.CanOpenWhenHeld()){
                return false;
            }
        }
        return keycard.HasAccess(accessLevel);
    }
}
