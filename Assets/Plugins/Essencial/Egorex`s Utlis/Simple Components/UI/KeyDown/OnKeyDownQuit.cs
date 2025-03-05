using UnityEngine;

public class OnKeyDownQuit : OnKeyDown
{
    void Awake()
    {
        onKeyDown.AddListener(CloseGame);
    }

    public static void CloseGame(){
        Application.Quit();
    }
}