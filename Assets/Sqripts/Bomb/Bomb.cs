using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : BombEffectsBase
{

    int _radius = 1;
    float _bombLifetime = 1.5f;
    int _maxBombCount = 1;
    int _avaliableBombCount = 1;

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsCountOK())
            StartCoroutine("Explosion", _bombLifetime);
    }
    private bool IsCountOK()
    {
        return _avaliableBombCount <= _maxBombCount && _avaliableBombCount > 0;
    }
    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("Bomb"))
            col.isTrigger = false;
    }
    private IEnumerator Explosion(float bombLifetime)
    {
        var bomb=DinamicChildObject("Bomb/Bomb", transform.position.x, transform.position.z);
        _avaliableBombCount++;
        yield return new WaitForSeconds(bombLifetime);

        bomb.SetActive(false);
        new Explosion().MakeExplosion(bomb, new ResourcesLoading().LoadItem("Bomb/ExplosionAll"), _radius, DestroyGameObj);
        if (IsPlayerAtBomb(bomb))
            gameObject.SetActive(false);

        _avaliableBombCount--;
    }
    private bool IsPlayerAtBomb(GameObject bomb)
    {
        return (bomb.transform.position.x == Mathf.Round(transform.position.x) &&
                bomb.transform.position.z == Mathf.Round(transform.position.z));
    }
    public void DestroyGameObj(List<RaycastHit> listOfHits)
    {
        listOfHits.ForEach(SelectForDestroying);
    }
    private void SelectForDestroying(RaycastHit hit)
    {
        switch (hit.collider.tag)
        {
            case "BreakWall":
                hit.transform.gameObject.SetActive(false);
                break;
            case "Hero":
                hit.transform.gameObject.SetActive(false);
                break;
            case "Enemy":
                hit.transform.gameObject.SetActive(false);
                break;
        }
    }

}
