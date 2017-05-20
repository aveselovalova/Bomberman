using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : BombEffectsBase {

    private List<Vector3> _directionsOfExplosion = new List<Vector3> { Vector3.up, Vector3.forward, Vector3.right, Vector3.back, Vector3.left };
    private List<RaycastHit> _listOfHits = new List<RaycastHit>();
    private Vector3 _explosionPos;
    GameObject boom;
    public Explosion()
    {
       
    }
    protected virtual GameObject DinamicChildObject(GameObject explos,  float xPosition, float zPosition)
    {
        var objLocation = new Vector3(Mathf.RoundToInt(xPosition), 0.55f, Mathf.RoundToInt(zPosition));
        return Instantiate(explos, objLocation, new Quaternion(0, 0, 0, 0));
    }
    public void MakeExplosion(GameObject bomb, GameObject explosion, float destroyedRadius, Action<List<RaycastHit>> actionDestroyObj = null)
    {
        _explosionPos = bomb.transform.position;
        DestroyRaius(explosion, destroyedRadius);
        boom = DinamicChildObject(explosion, _explosionPos.x, _explosionPos.z);
        CheckHits(destroyedRadius);
        if (actionDestroyObj != null)
                actionDestroyObj(_listOfHits);
    }
    private void DestroyRaius(GameObject explosion, float radius)
    {
        foreach (var r in explosion.GetComponentsInChildren<ParticleSystem>())
            r.startLifetime = radius;
    }
    private void CheckHits(float radius)
    {
        Debug.Log(radius);
        foreach(var direction in _directionsOfExplosion)
        {
            RaycastHit hit = Physics.RaycastAll(_explosionPos, direction, radius).FirstOrDefault();
            if (hit.collider != null)
                _listOfHits.Add(hit);
        }
    }
}
