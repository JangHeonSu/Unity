using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Transactions;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private Transform target;

    public float speed = 70f;
    public float explosionRadius = 0f;
    public GameObject ImpactEffect;
    public float damage;

    public void Seek(Transform _target)
    {
        target = _target;
    }

    void Update()
    {
        if(target == null) 
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;
        
        if(dir.magnitude <=distanceThisFrame) 
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);
    }

    void HitTarget()
    {
        GameObject effectIns = (GameObject)Instantiate(ImpactEffect, transform.position, transform.rotation);
        Destroy(effectIns, 2f);

        if(explosionRadius >0f)
        {
            Explode();
        }
        else
        {
            Damage(target);
        }

        Destroy(gameObject);
    }

    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius); 
        foreach(Collider collider in colliders) 
        {
            if(collider.tag == "Enemy")
            {
                Damage(collider.transform);
            }
        }
    }
    void Damage(Transform enemy)
    {
        enemy aboutEnemy = enemy.GetComponent<enemy>();

        if(aboutEnemy != null) 
        {
            aboutEnemy.TakeDamage(damage);
        }
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
