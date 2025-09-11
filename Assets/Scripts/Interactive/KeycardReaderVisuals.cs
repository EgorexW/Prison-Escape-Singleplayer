using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

class KeycardReaderVisuals : MonoBehaviour
{
    public KeycardReader keycardReader;

    [BoxGroup("Text")] [Required] [SerializeField] TextMeshPro text;

    [BoxGroup("Text")] [SerializeField] float textDisplayTime = 3;

    // [BoxGroup("Text")] [SerializeField] string defaultText = "<color=yellow>----</color>";
    [BoxGroup("Text")] [SerializeField] string accessGrantedText = "<color=green>✓</color>";
    [BoxGroup("Text")] [SerializeField] string accessDeniedText = "<color=red>X</color>";

    [BoxGroup("Audio")] [SerializeField] PlayAudio accessGrantedSound;
    [BoxGroup("Audio")] [SerializeField] PlayAudio accessDeniedSound;
    [BoxGroup("Audio")] [SerializeField] PlayAudio electrocuteSound;

    [BoxGroup("Sparks")] [SerializeField] ParticleSystem sparks;

    string defaultText = "<color=yellow>----</color>";
    float lastTextChangeTime;

    void Start()
    {
        keycardReader.onPowerChanged.AddListener(OnPowerChanged);
        OnPowerChanged(keycardReader.IsPowered());
        defaultText = keycardReader.accessLevel.name;
        text.color = keycardReader.accessLevel.color;
    }

    void Update()
    {
        if (Time.time - lastTextChangeTime > textDisplayTime){
            text.text = defaultText;
        }
    }

    void OnPowerChanged(bool working)
    {
        text.enabled = working;
    }

    public void AccessDenied()
    {
        text.text = accessDeniedText;
        lastTextChangeTime = Time.time;
        accessDeniedSound?.Play();
    }

    public void AccessGranted(bool original)
    {
        text.text = accessGrantedText;
        defaultText = accessGrantedText;
        if (!original){
            return;
        }
        accessGrantedSound?.Play();
    }

    public void Electrocute()
    {
        sparks?.Play();
    }
}