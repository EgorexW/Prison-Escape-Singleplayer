using UnityEngine;

public class Door : MonoBehaviour, IDoor
{
    [SerializeField] Vector3 rotationAxis = new Vector3(0, 1, 0);
    [SerializeField] float rotationAngle = -90;
    [SerializeField] float moveTime = 0.75f;
    [SerializeField] bool beginOpen = false;
    [SerializeField] Optional<float> autoCloseTime;
    [SerializeField] bool colliderWhenOpen = false;
    
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
        if (beginOpen){
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

    public virtual bool CanCharacterUse(Character character, bool onInteract){
        return true;
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
}
