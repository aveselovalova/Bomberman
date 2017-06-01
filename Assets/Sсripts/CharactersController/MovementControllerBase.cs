using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovementControllerBase : MonoBehaviour
{

    protected float horizontalMovement;
    protected float verticalMovement;
    protected int _rotationAngle = 0;
    public AudioClip stepSound;
    protected AudioSource soundSource;

    protected abstract void GetObjectRotation(int angle);
    protected abstract void Move(float speed);
    protected abstract void ChoseRotation();
    protected abstract void CharactersStepPlay();
}
