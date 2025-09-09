using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

class KeycardReaderVisuals : MonoBehaviour
{
    public KeycardReader keycardReader;

    [BoxGroup("Text")] [Required] [SerializeField] TextMeshPro text;
    [BoxGroup("Text")] [SerializeField] float textDisplayTime = 3;
    [BoxGroup("Text")] [SerializeField] string defaultText = "<color=yellow>----</color>";
    [BoxGroup("Text")] [SerializeField] string accessGrantedText = "<color=green>✓</color>";
    [BoxGroup("Text")] [SerializeField] string accessDeniedText = "<color=red>X</color>";

    [BoxGroup("Audio")] [SerializeField] PlayAudio accessGrantedSound;
    [BoxGroup("Audio")] [SerializeField] PlayAudio accessDeniedSound;

    float lastTextChangeTime;

    void Start()
    {
        keycardReader.onPowerChanged.AddListener(OnPowerChanged);
        OnPowerChanged();
    }

    void Update()
    {
        if (Time.time - lastTextChangeTime > textDisplayTime){
            text.text = defaultText;
        }
    }

    void OnPowerChanged()
    {
        var working = keycardReader.GetPowerLevel() != PowerLevel.NoPower;
        text.enabled = working;
    }

    public void AccessDenied()
    {
        text.text = accessDeniedText;
        lastTextChangeTime = Time.time;
        accessDeniedSound?.Play();
    }

    public void AccessGranted()
    {
        text.text = accessGrantedText;
        defaultText = accessGrantedText;
        accessGrantedSound?.Play();
    }

    public void Electrocute()
    {
        // TODO add sparks or something
    }
}