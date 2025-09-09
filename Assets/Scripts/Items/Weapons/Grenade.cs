using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class Grenade : ItemEffect
{
    [SerializeField] [Required] GameObject effectPrefab;

    [SerializeField] float timeToExplode = 3f;
    [SerializeField] float radius = 3f;
    [SerializeField] Damage damage = new(30f, 50f);

    [FoldoutGroup("Events")] public UnityEvent onExplode;

    public override void Use(Player playerTmp, bool alternative = false)
    {
        playerTmp.ThrowItem(Item);
        Item.pickupable = false;
        General.CallAfterSeconds(Explode, timeToExplode);
    }

    void Explode()
    {
        var hits = Physics.OverlapSphere(transform.position, radius);

        var effect = Instantiate(effectPrefab, transform.position, Quaternion.identity);
        effect.transform.localScale = Vector3.one * radius;

        var transforms = General.GetComponentsFromCollider<Transform>(hits);

        foreach (var hitTransform in transforms) ResolveHit(hitTransform);

        onExplode.Invoke();

        gameObject.SetActive(false);
    }

    protected virtual void ResolveHit(Transform hitTransform)
    {
        var damageable = General.GetRootComponent<IDamagable>(hitTransform, false);
        damageable?.Damage(damage);
    }
}