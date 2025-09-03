using System;
using UnityEngine;

class PoisonTrap : MonoBehaviour, ITrap
{
    [SerializeField] Damage damagePerSecond = new Damage(2f, 0);

    void Awake()
    {
        gameObject.SetActive(false);
    }

    public void Activate()
    {
        gameObject.SetActive(true);
    }

    void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent<Player>(out var player))
        {
            player.Damage(damagePerSecond * Time.deltaTime);
        }
    }
}