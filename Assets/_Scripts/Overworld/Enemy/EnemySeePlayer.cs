using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class EnemySeePlayer : MonoBehaviour
{
    public EnemyPatrol enemyPatrol;

    public float rayDistance = 10f;
    public float sphereRadius = 0.5f;

    void Update()
    {
        if (enemyPatrol.SeePlayer) return;
        RaycastHit hit;

        int layerMask = 1 << 8;

        if (Physics.SphereCast(transform.position, sphereRadius, transform.forward, out hit, rayDistance, layerMask))
        {
            enemyPatrol.SeePlayer = true;
        }

        Debug.DrawRay(transform.position, transform.forward * rayDistance, Color.yellow);
    }
}

