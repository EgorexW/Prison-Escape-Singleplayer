using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public partial class Character
{   
    [SerializeField] float maxInteractDis = 3;

    [FoldoutGroup("Events")] public UnityEvent<float, float> onHoldInteraction;
    [FoldoutGroup("Events")] public UnityEvent onFinishInteraction;
    
    Coroutine holdCoroutine;

    public IInteractive GetInteractive(){
        RaycastHit[] raycasts = new RaycastHit[10];
        Physics.RaycastNonAlloc(new Ray(aim.position, aim.forward), raycasts, maxInteractDis);
        IInteractive interactive = new DummyInteractive();
        foreach(RaycastHit raycast in raycasts){
            if (raycast.collider == null){
                continue;
            }
            GameObject gameObject = raycast.collider.gameObject;
            if (raycast.collider.attachedRigidbody != null){
                gameObject = raycast.collider.attachedRigidbody.gameObject;
            }
            gameObject.TryGetComponent<IInteractive>(out IInteractive interactiveTmp);
            if (interactiveTmp != null){
                interactive = interactiveTmp;
                break;
            }
        }
        return interactive;
    }

    public void Interact(float duration = 0){
        IInteractive interactive = GetInteractive();
        if (interactive.HoldDuration > duration){
            if (holdCoroutine != null){
                return;
            }
            holdCoroutine = StartCoroutine(HoldCoroutine(interactive));
            return;
        }
        onFinishInteraction.Invoke();
        interactive.Interact(this);
    }
    public void CancelInteract(){
        onFinishInteraction.Invoke();
        if (holdCoroutine == null){
            return;
        }
        StopCoroutine(holdCoroutine);
        holdCoroutine = null;
    }
    public IEnumerator HoldCoroutine(IInteractive interactive){
        float time = 0;
        while (time <= interactive.HoldDuration){
            onHoldInteraction.Invoke(time, interactive.HoldDuration);
            time += Time.deltaTime;
            yield return null;
        }
        Interact(time);
        CancelInteract();
    }
}
