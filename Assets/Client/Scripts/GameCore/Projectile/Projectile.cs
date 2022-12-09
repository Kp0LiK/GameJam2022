using Client;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public EntityData EntityData { get; set; }
    public Vector3 Direction { get; set; }

    public Rigidbody Rigidbody { get; private set; }
    
    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }
    

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent(out SpiderBehaviour damageable))
        {
            damageable.ApplyDamage(EntityData.Damage);
            Destroy(gameObject);
            return;
        }
        
        if (other.gameObject.TryGetComponent(out BossBehaviour boss))
        {
            boss.ApplyDamage(30f);
            Destroy(gameObject);
            return;
        }
        
        Destroy(gameObject, 3);

    }
}