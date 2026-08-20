using Sirenix.OdinInspector;
using UnityEngine;

public class PrefsCheats : MonoBehaviour
{
    [Button]
    public void UnlockAscensions(int level)
    {
        PlayerPrefs.SetInt(PlayerPrefsKeys.UnlockedAscension, level);
    }
}
