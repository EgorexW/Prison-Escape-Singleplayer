using UnityEngine;
using UnityEngine.SceneManagement;

public class WinGame : MonoBehaviour
{
    [SerializeField] string sceneToLoad = "Win Screen";

    public void Win()
    {
        SceneManager.LoadSceneAsync(sceneToLoad);
    }
}
