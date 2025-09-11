using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public partial class Player
{
    [SerializeField] bool log;
    [SerializeField] float maxInteractDis = 3;

    [FoldoutGroup("Events")] public UnityEvent<float, float> onHoldInteraction;
    [FoldoutGroup("Events")] public UnityEvent onFinishInteraction;

    Coroutine holdCoroutine;

    public IInteractive GetInteractive()
    {
        var raycasts = new RaycastHit[10];
        Physics.RaycastNonAlloc(new Ray(aim.position, aim.forward), raycasts, maxInteractDis);
        if (log){
            Debug.DrawRay(aim.position, aim.forward * maxInteractDis, Color.red, 3);
        }
        IInteractive interactive = new DummyInteractive();
        var closestDis = float.MaxValue;
        foreach (var raycast in raycasts){
            if (raycast.collider == null){
                continue;
            }
            var distance = Vector3.Distance(aim.position, raycast.point);
            if (distance > closestDis){
                continue;
            }
            var gameObject = raycast.collider.gameObject;
            if (raycast.collider.attachedRigidbody != null){
                gameObject = raycast.collider.attachedRigidbody.gameObject;
            }
            gameObject.TryGetComponent<IInteractive>(out var interactiveTmp);
            if (interactiveTmp != null){
                closestDis = distance;
                interactive = interactiveTmp;
            }
        }
        return interactive;
    }

    public void Interact(float duration = 0)
    {
        var interactive = GetInteractive();
        if (log){
            Debug.Log("Interactive hit: " + interactive);
        }
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

    public void CancelInteract()
    {
        onFinishInteraction.Invoke();
        if (holdCoroutine == null){
            return;
        }
        StopCoroutine(holdCoroutine);
        holdCoroutine = null;
    }

    public IEnumerator HoldCoroutine(IInteractive interactive)
    {
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