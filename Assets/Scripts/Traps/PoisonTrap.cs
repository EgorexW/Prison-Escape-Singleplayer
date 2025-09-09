using UnityEngine;

class PoisonTrap : MonoBehaviour, ITrap
{
    [SerializeField] Damage damagePerSecond = new(2f);

    void Awake()
    {
        gameObject.SetActive(false);
    }

    void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent<Player>(out var player)){
            player.Damage(damagePerSecond * Time.deltaTime);
        }
    }

    public void Activate()
    {
        gameObject.SetActive(true);
    }
}