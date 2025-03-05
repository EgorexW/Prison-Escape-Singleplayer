using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public enum Direction{
    Left,
    Right,
    Up,
    Down
}

public class General : MonoBehaviour
{
    static General instance;

    static General GetInstance(){
        if (instance == null){
            instance = new GameObject("General").AddComponent<General>();
        }
        return instance;
    }
    public static void EnableAfterSeconds(GameObject gameObject, float seconds){
        GetInstance().StartCoroutine(EnableAfterSecondsCoroutine(gameObject, seconds));
    }
    public static void StartAfterSeconds(MonoBehaviour monoBehaviour, IEnumerator coroutine, float seconds){
        GetInstance().StartCoroutine(StartAfterSecondsCoroutine(monoBehaviour, coroutine, seconds));
    }
    public static void CallAfterSeconds(UnityAction action, float seconds = 0){
        GetInstance().StartCoroutine(CallAfterSecondsCoroutine(action, seconds));
    }
    public static Vector2 DirToVector(Direction direction){
        switch (direction){
            case Direction.Left:
                return Vector2.left;
            case Direction.Right:
                return Vector2.right;
            case Direction.Up:
                return Vector2.up;
            case Direction.Down:
                return Vector2.down;
        }
        return Vector2.zero;
    }
    public static Vector2[] GetVectorsList(){
        return new[]{
            Vector2.right,
            Vector2.left,
            Vector2.up,
            Vector2.down
        };
    }
    public static float GetAngleFromVector(Vector2 dir){
        dir = dir.normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (angle < 0){
            angle += 360;
        }
        return angle;
    }
    
    public static Vector3 GetMousePos()
    {
        return GetMouseWorldPos(Input.mousePosition);
    }

    public static Vector3 GetMouseWorldPos(Vector3 mousePos)
    {
        Debug.Assert(Camera.main != null, "Camera.main == null");
        Vector3 pos = Camera.main.ScreenToWorldPoint(mousePos);
        pos.z = 0;
        return pos;
    }

    public static Vector2 RandomPointOnCircle(){
        var angle = Random.value * 2 * Mathf.PI;
        return new Vector2(Mathf.Sin(angle), Mathf.Cos(angle));
    }
    static IEnumerator StartAfterSecondsCoroutine(MonoBehaviour monoBehaviour, IEnumerator coroutine, float seconds){
        yield return new WaitForSeconds(seconds);
        if (monoBehaviour == null || !monoBehaviour.gameObject.activeInHierarchy){
            yield break;
        }
        monoBehaviour.StartCoroutine(coroutine);
    }
    static IEnumerator EnableAfterSecondsCoroutine(GameObject gameObject, float seconds){
        yield return new WaitForSeconds(seconds);
        if (gameObject == null){
            yield break;
        }
        gameObject.SetActive(true);
    }
    static IEnumerator CallAfterSecondsCoroutine(UnityAction action, float seconds){
        yield return new WaitForSeconds(seconds);
        action.Invoke();
    }
    public static string TimeToString(float nr){
        int minutes = Mathf.FloorToInt(nr/60);
        float seconds = nr - minutes * 60;
        seconds = Mathf.Round(seconds);
        string text = minutes + ":" + seconds;
        if (seconds < 10){
            text = minutes + ":0" + seconds; 
        }
        return text;
    }
    public static Vector2Int RoundVector(Vector2 pos)
    {
        return new Vector2Int(Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.y));
    }
    public static TComponent GetComponentFromRaycast<TComponent>(RaycastHit2D raycast){
        return GetComponentFromCollider<TComponent>(raycast.collider);
    }
    public static TComponent GetComponentFromCollider<TComponent>(Collider2D collider){
        if (collider == null){
            return default;
        }
        return !collider.TryGetComponent(out TComponent component) ? default : component;
    }
    public static GameObject GetGameObjectFromRaycast(RaycastHit2D raycast){
        return GetGameObjectFromCollider(raycast.collider);
    }

    static GameObject GetGameObjectFromCollider(Collider2D collider)
    {
        if (collider == null){
            return null;
        }
        return collider.gameObject;
    }

    public static void WorldText(string text, Vector2 pos, float size, float time = 0.01f){
        WorldText(text, pos, size, time, Color.white);
    }
    public static void WorldText(string text, Vector2 pos, float size, float time, Color color)
    {
        GameObject gameObject = new GameObject("WorldText"){
            transform ={
                position = pos
            },
            hideFlags = HideFlags.HideInHierarchy
        };
        TextMeshPro textMeshPro = gameObject.AddComponent<TextMeshPro>();
        textMeshPro.color = color;
        textMeshPro.text = text;
        textMeshPro.fontSize = size;
        textMeshPro.alignment = TextAlignmentOptions.Center;
        Destroy(gameObject, time);
    }
    public static TComponent GetRootComponent<TComponent>(GameObject gameObject, bool mustBeFound = true)
    {
        return GetRootComponent<TComponent>(gameObject.transform, mustBeFound);
    }
    public static TComponent GetRootComponent<TComponent>(Transform transform, bool mustBeFound = true)
    {
        var objectRoot = GetObjectRoot(transform, mustBeFound);
        if (objectRoot == null) return default;
        TComponent component = objectRoot.GetRootComponent<TComponent>();
        Debug.Assert(!mustBeFound || component != null, typeof(TComponent) + " is null", transform);
        return component;
    }

    public static ObjectRoot GetObjectRoot(Transform transform, bool mustBeFound = true)
    {
        Transform checkedTransform = transform;
        ObjectRoot objectRoot;
        while (true){
            if (checkedTransform.TryGetComponent(out objectRoot)){
                break;
            }
            checkedTransform = checkedTransform.parent;
            if (checkedTransform != null) continue;
            if (mustBeFound){
                Debug.LogError("Object Root not found", transform);
            }
            break;
        }
        return objectRoot;
    }

    public static Vector2 GetClosestPos(Vector2 centerPos, List<Vector2> poses)
    {
        var closestPos = Vector2.zero;
        var smallestDis = Mathf.Infinity;
        foreach (var pos in poses){
            var dis = Vector2.Distance(centerPos, pos);
            if (dis < smallestDis){
                smallestDis = dis;
                closestPos = pos;
            }
        }
        return closestPos;
    }

    public static Vector2 GetClosestPos(Vector3 transformPosition, List<GameObject> enemies)
    {
        var poses = enemies.ConvertAll(input => (Vector2)input.transform.position);
        return GetClosestPos(transformPosition, poses);
    }

    public static bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

    public static float RandomRange(Vector2 vector)
    {
        return UnityEngine.Random.Range(vector.x, vector.y);
    }
    
    public static int RandomRange(Vector2Int vector)
    {
        return UnityEngine.Random.Range(vector.x, vector.y);
    }
}

public interface INamed
{
    public string GetName();
}