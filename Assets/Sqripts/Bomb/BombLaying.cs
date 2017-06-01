using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class BombLaying : Explosion
{

    public int radius;
    public int maxBombCount;
    public AudioClip setBombSound;
    public AudioClip explosionSound;

    private float _bombLifetime;
    private int _avaliableBombCount;
    private Score _scoreCounter;
    private Animator _animator;
    private AudioSource _soundSource;

    private void Start()
    {
        radius = 1;
        maxBombCount = 1;
        _bombLifetime = 1.5f;
        _avaliableBombCount = 1;
        _scoreCounter = GetComponent<Score>();
        _animator = GetComponent<Animator>();
        _soundSource = GetComponentInChildren<AudioSource>();

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
        var bombPosition = transform.position.RoundXZCoordinate();
        var yOffset = 0.3f;
        var newBombPosition = new Vector3(bombPosition.x, bombPosition.y + yOffset, bombPosition.z);
        _soundSource.PlayOneShot(setBombSound);
        var bomb = new DynamicObjectsCreator().CreateDynamicGameObject("Bomb/Bomb", newBombPosition);
        _animator.SetTrigger("SetBomb");
        _avaliableBombCount++;
        FillBarriersForAllIntelEnemies(newBombPosition);
        yield return new WaitForSeconds(bombLifetime);

        bomb.SetActive(false);
        _soundSource.PlayOneShot(explosionSound);
        ClearBarriersForAllIntelEnemies(newBombPosition);
        MakeExplosion(bomb, "Bomb/ExplosionAll", radius, DestroyGameObj);

        DestroyCharacters(bomb);

        _avaliableBombCount--;
    }

    public bool IsCharacterAtBomb(GameObject bomb, GameObject character)
    {
        return (bomb.transform.position.x == Mathf.Round(character.transform.position.x) &&
                bomb.transform.position.z == Mathf.Round(character.transform.position.z));
    }
    private void DestroyCharacterAtBomb(GameObject bomb, string characterName, int timeOfDestroy = 3)
    {

        var characters = GameObject.FindGameObjectsWithTag(characterName);
        foreach (var character in characters)
            if (IsCharacterAtBomb(bomb, character))
            {

                character.GetComponent<Animator>().SetTrigger("Killed");
                Destroy(character, timeOfDestroy);
                switch (characterName)
                {
                    case "Enemy":
                        _scoreCounter.UpdateScoreForEnemy(Characters.Enemy);
                        break;
                    case "IntelligentEnemy":
                        _scoreCounter.UpdateScoreForEnemy(Characters.IntelligentEnemy);
                        break;
                    case "Hero":
                        enabled = false;
                        GetComponent<Score>().OutputFailText();
                        break;
                }
            }

    }
    private void DestroyCharacters(GameObject bomb)
    {
        DestroyCharacterAtBomb(bomb,"Hero");
        DestroyCharacterAtBomb(bomb,"Enemy");
        DestroyCharacterAtBomb(bomb, "IntelligentEnemy");
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
                ClearBarriersForAllIntelEnemies(hit.transform.position);
                hit.transform.gameObject.SetActive(false);
                break;
            case "Hero":
                hit.transform.gameObject.SetActive(false);
                _scoreCounter.OutputFailText();
                break;
            case "Enemy":
                _scoreCounter.UpdateScoreForEnemy(Characters.Enemy);
                hit.transform.GetComponent<Animator>().SetTrigger("Killed");
                Destroy(hit.transform.gameObject, 3);
                break;
            case "IntelligentEnemy":
                _scoreCounter.UpdateScoreForEnemy(Characters.IntelligentEnemy);
                hit.transform.gameObject.SetActive(false);
                break;
        }
    }
    private void ClearBarriersForAllIntelEnemies(Vector3 wallPosition)
    {
        foreach (var intelEnemy in GameObject.FindGameObjectsWithTag("IntelligentEnemy"))
        {
            var roundWallPos = GetRoundPosition.GetPoint(wallPosition);
            intelEnemy.GetComponent<IntelligentEnemiesController>().ClearPositionOnField(roundWallPos);
        }
    }
    private void FillBarriersForAllIntelEnemies(Vector3 bombPosition)
    {
        foreach (var intelEnemy in GameObject.FindGameObjectsWithTag("IntelligentEnemy"))
        {
            var roundWallPos = GetRoundPosition.GetPoint(bombPosition);
            intelEnemy.GetComponent<IntelligentEnemiesController>().FillPositionOnField(roundWallPos);
        }
    }

}
