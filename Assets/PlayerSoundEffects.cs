using Nrjwolf.Tools.AttachAttributes;
using UnityEngine;

[RequireComponent(typeof(PlayAudio))]
public class PlayerSoundEffects : MonoBehaviour
{
    [GetComponent][SerializeField] PlayAudio playAudio;
    
    public void PlaySoundEffect(Sound sound)
    {
        if (sound == null){
            Debug.LogWarning("No sound assigned to sound effect");
            return;
        }
        playAudio.sound = sound;
        playAudio.Play();
    }
}
