using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawSpawnRange : MonoBehaviour
{
    public float agentDensity = 30f;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, agentDensity);
    }
}
