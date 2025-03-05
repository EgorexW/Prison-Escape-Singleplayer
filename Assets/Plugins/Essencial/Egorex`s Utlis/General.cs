using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
    public static void CallAfterSeconds(UnityAction action, float seconds){
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
        return new Vector2[]{
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

    public static Vector3 GetMousePos(){
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
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
}

public static class MyExtentions{
    public static void Shuffle<T>(this List<T> list){
        var count = list.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i) {
            var r = UnityEngine.Random.Range(i, count);
            var tmp = list[i];
            list[i] = list[r];
            list[r] = tmp;
        }
    }
}