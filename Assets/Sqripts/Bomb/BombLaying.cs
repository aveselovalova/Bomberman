using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class BombLaying : Explosion
{

    public int radius;
    public int maxBombCount;

    private float _bombLifetime;
    private int _avaliableBombCount;
    private Score _scoreCounter;
    private void Start()
    {
        radius = 1;
        maxBombCount = 1;
        _bombLifetime = 1.5f;
        _avaliableBombCount = 1;
        _scoreCounter = GetComponent<Score>();
    }
    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsCountOK())
            StartCoroutine("Explosion", _bombLifetime);
    }
   
    private bool IsCountOK()
    {
        return _avaliableBombCount <= maxBombCount && _avaliableBombCount > 0;
    }
  
    private IEnumerator Explosion(float bombLifetime)
    {
        var bombPosition = GetRoundPosition.RoundXZCoordinate(transform.position);
        var yOffset = 0.3f;
        var newBombPosition = new Vector3(bombPosition.x, bombPosition.y- yOffset, bombPosition.z);

        var bomb = new DynamicObjectsCreator().CreateDynamicGameObject("Bomb/Bomb", newBombPosition);

        _avaliableBombCount++;

        yield return new WaitForSeconds(bombLifetime);

        bomb.SetActive(false);
        MakeExplosion(bomb,"Bomb/ExplosionAll", radius, DestroyGameObj);

        if (IsPlayerAtBomb(bomb))
            gameObject.SetActive(false);

        _avaliableBombCount--;
    }

    public bool IsPlayerAtBomb(GameObject bomb)
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
                PowerupsGenerator.GenerateNewPowerup(hit.transform.position);
                hit.transform.gameObject.SetActive(false);
                break;
            case "Hero":
                hit.transform.gameObject.SetActive(false);
                _scoreCounter.OutputFailText();
                break;
            case "Enemy":
                _scoreCounter.UpdateScoreForEnemy(Characters.Enemy);
                hit.transform.gameObject.SetActive(false);
                break;
            case "IntelligentEnemy":
                _scoreCounter.UpdateScoreForEnemy(Characters.IntelligentEnemy);
                hit.transform.gameObject.SetActive(false);
                break;
        }
    }


}
