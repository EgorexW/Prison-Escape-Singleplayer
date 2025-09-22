using UnityEngine;
using UnityEngine.SceneManagement;

public class WinGame : MonoBehaviour
{
    [SerializeField] string sceneToLoad = "Win Screen";

    [SerializeField] int delay = 2;

    public void Win()
    {
        General.CallAfterSeconds(() => SceneManager.LoadSceneAsync(sceneToLoad), delay);
    }
}