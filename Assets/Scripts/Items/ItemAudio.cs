using Nrjwolf.Tools.AttachAttributes;
using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(PlayAudio))]
public class ItemAudio : ItemEffect
    {
        [GetComponent][SerializeField] PlayAudio playAudio;

        public override void Use(Player player, bool alternative = false)
        {
            base.Use(player, alternative);
            playAudio.Play();
        }

        public override void HoldUse(Player player, bool alternative = false)
        {
            base.HoldUse(player, alternative);
            playAudio.Play();
        }
    }