using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{

    private List<Vector3> _directionsOfExplosion = new List<Vector3> {Vector3.forward, Vector3.right, Vector3.back, Vector3.left };
    private Vector3 _explosionPos;
   
    protected void MakeExplosion(GameObject bomb, string explosionName, float destroyedRadius, Action<List<RaycastHit>> actionDestroyObj = null)
    {
        _explosionPos = bomb.transform.position;
        var explosion = new DynamicObjectsCreator().CreateDynamicGameObject(explosionName, _explosionPos);
        RadiusOfDestroing(explosion, destroyedRadius);

        List<RaycastHit> listOfHits = CheckHits(destroyedRadius);
        if (actionDestroyObj != null)
            actionDestroyObj(listOfHits);
    }
    private void RadiusOfDestroing(GameObject explosion, float radius)
    {
        var raysOfExplosion = explosion.GetComponentsInChildren<ParticleSystem>();
        foreach (var ray in raysOfExplosion)
            ray.startLifetime = radius;
    }
    private List<RaycastHit> CheckHits(float radius)
    {
        List<RaycastHit> listOfHits = new List<RaycastHit>();
        foreach (var direction in _directionsOfExplosion)
        {
            var hit = Physics.RaycastAll(_explosionPos, direction, radius).FirstOrDefault();
            if (hit.collider != null)
                listOfHits.Add(hit);
        }
        return listOfHits;
    }
}
