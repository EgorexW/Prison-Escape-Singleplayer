using System;
using Nrjwolf.Tools.AttachAttributes;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

public class KeycardDetectorOverlay : UIElement
{
    [BoxGroup("References")][Required][SerializeField] ObjectsPool objectsUI;
    [BoxGroup("References")][Required][SerializeField] RectTransform container;

    [SerializeField] float innerRange = 2f;
    [SerializeField] float range = 5f;

    float size;
    Player player;

    float Scale => Size / innerRange / 2;
    float Size => Mathf.Min(container.rect.height, container.rect.width);

    public void Init(Player playerTmp)
    {
        player = playerTmp;
        Show();
    }

    void Update()
    {
        var playerPos = player.transform.position;
        var colliders = Physics.OverlapSphere(playerPos, range);
        var keycards = General.GetComponentsFromCollider<Keycard>(colliders);
        // Debug.Log("keycards found: " + keycards.Count);
        objectsUI.Clear();
        foreach (var keycard in keycards){
            if (keycard.ignoreDetectorOverlay){
                continue;
            }
            var item = keycard.GetComponent<Item>();
            if (item != null && item.isHeld){
                continue;
            }
            
            var dis = keycard.transform.position - playerPos;
            Debug.DrawRay(playerPos, dis, Color.red, 1f);
            var dir = dis.normalized;
            var keycardObj = objectsUI.AddObject();
            var rect = keycardObj.GetComponent<RectTransform>();
            
            // AI-generated code: map world direction to UI position without rotating the marker
            Vector3 forwardFlat = new Vector3(player.transform.forward.x, 0f, player.transform.forward.z).normalized;
            Vector3 rightFlat   = new Vector3(player.transform.right.x, 0f, player.transform.right.z).normalized;
            var clampedDistance = Mathf.Min(dis.magnitude, innerRange);
            float x = Vector3.Dot(dir, rightFlat) * clampedDistance * Scale;
            float y = Vector3.Dot(dir, forwardFlat) * clampedDistance * Scale;
            var vector2 = new Vector2(x, y);
            
            rect.anchoredPosition = vector2;
            
            // Debug.LogFormat("[KeycardOverlay] keycardWorldPos={0} playerPos={1} dir={2} dotRight={3:F2} dotForward={4:F2} clampedDistance={5:F2} scale={6:F2} uiPos=({7:F2},{8:F2}) screenSize=({9:F0},{10:F0})",
            //     keycard.transform.position, playerPos, dir, Vector3.Dot(dir, new Vector3(player.transform.right.x,0f,player.transform.right.z).normalized), Vector3.Dot(dir, new Vector3(player.transform.forward.x,0f,player.transform.forward.z).normalized), clampedDistance, Scale, vector2.x, vector2.y, container.rect.width, container.rect.height);
            
            var keycardVisuals = keycardObj.GetComponent<KeycardVisuals>();
            keycardVisuals.keycard = keycard;
            keycardVisuals.Apply();
        }
    }
}
