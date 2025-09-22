using Nrjwolf.Tools.AttachAttributes;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class PlayerAim : MonoBehaviour
{
    [BoxGroup("References")] [GetComponent] [SerializeField] Image image;

    [BoxGroup("References")] [Required] [SerializeField] Player player;

    [BoxGroup("References")] [Required] [SerializeField] Sprite notInteractableCursor;
    [BoxGroup("References")] [Required] [SerializeField] Sprite interactableCursor;

    void Update()
    {
        image.sprite = player.GetInteractive().IsDummy ? notInteractableCursor : interactableCursor;
    }
}