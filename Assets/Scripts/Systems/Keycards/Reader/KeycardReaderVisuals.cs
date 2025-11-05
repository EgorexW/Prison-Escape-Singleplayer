using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class KeycardReaderVisuals : MonoBehaviour
{
    public KeycardReader keycardReader;

    [BoxGroup("Text")] [Required] [SerializeField] TextMeshPro text;

    [BoxGroup("Text")] [SerializeField] float textDisplayTime = 3;

    // [BoxGroup("Text")] [SerializeField] string defaultText = "<color=yellow>----</color>";
    [BoxGroup("Text")] [SerializeField] string accessGrantedText = "<color=green>✓</color>";
    [BoxGroup("Text")] [SerializeField] string accessDeniedText = "<color=red>X</color>";
    [BoxGroup("Text")] [SerializeField] string corruptedText = "<color=red>ERR</color>";

    [BoxGroup("Audio")] [SerializeField] PlayAudio accessGrantedSound;
    [BoxGroup("Audio")] [SerializeField] PlayAudio accessDeniedSound;
    [BoxGroup("Audio")] [SerializeField] PlayAudio electrocuteSound;
    [BoxGroup("Audio")] [SerializeField] PlayAudio corruptedSound;

    [BoxGroup("Sparks")] [SerializeField] ParticleSystem sparks;

    [BoxGroup("Icons")] [SerializeField] GameObject stealCardIcon;

    string defaultText = "<color=yellow>----</color>";
    float lastTextChangeTime = Mathf.NegativeInfinity;

    void Start()
    {
        keycardReader.onPowerChanged.AddListener(OnPowerChanged);
        OnPowerChanged(keycardReader.IsPowered());
        UpdateVisual();
    }

    void Update()
    {
        if (Time.time - lastTextChangeTime > textDisplayTime){
            text.text = defaultText;
        }
    }

    public void UpdateVisual()
    {
        if (keycardReader == null){
            return;
        }
        if (keycardReader.corrupted){
            return;
        }
        defaultText = keycardReader.accessLevel.name;
        text.color = keycardReader.accessLevel.color;
        stealCardIcon.SetActive(keycardReader.stealCard & keycardReader.IsPowered());
    }

    void OnPowerChanged(bool working)
    {
        text.enabled = working;
        stealCardIcon.SetActive(keycardReader.stealCard && working);
    }

    public void AccessDenied()
    {
        text.text = accessDeniedText;
        lastTextChangeTime = Time.time;
        accessDeniedSound?.Play();
    }

    public void AccessGranted(bool original)
    {
        defaultText = accessGrantedText;
        if (!original){
            return;
        }
        accessGrantedSound?.Play();
    }

    public void Electrocute()
    {
        sparks?.Play();
        electrocuteSound?.Play();
    }

    public void Corrupted()
    {
        corruptedSound?.Play();
        defaultText = corruptedText;
    }
}