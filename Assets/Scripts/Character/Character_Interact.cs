using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Character
{   
    [SerializeField] float maxInteractDis = 3;
    Coroutine holdCoroutine;

    public IInteractive GetInteractive(){
        RaycastHit[] raycasts = new RaycastHit[10];
        Debug.Log(aim);
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
    public void Interact(){
        Interact(0);
    }
    public void Interact(float duration = 0){
        IInteractive interactive = GetInteractive();
        if (interactive.GetHoldDuration() > duration){
            if (holdCoroutine != null){
                return;
            }
            holdCoroutine = StartCoroutine(HoldCoroutine(interactive));
            return;
        }
        interactive.Interact(this);
    }
    public void CancelInteract(){
        if (holdCoroutine == null){
            return;
        }
        StopCoroutine(holdCoroutine);
        holdCoroutine = null;
    }
    public IEnumerator HoldCoroutine(IInteractive interactive){
        float time = 0;
        while (time <= interactive.GetHoldDuration()){
            time += Time.deltaTime;
            yield return null;
        }
        Interact(time);
        CancelInteract();
    }
}
