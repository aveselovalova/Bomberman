using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : RotateBase
{

    private float _speed = 1.5f;
    private int _rotateAngle = 0;

    void Start()
    {
    }
   
    void FixedUpdate()
    {
        Moving();
    }
    void Moving()
    {
        if (Input.GetKey(KeyCode.UpArrow)) MoveInDirection(Vector3.forward, _rotateAngle - 90);
        if (Input.GetKey(KeyCode.RightArrow)) MoveInDirection(Vector3.right, _rotateAngle);
        if (Input.GetKey(KeyCode.LeftArrow)) MoveInDirection(Vector3.left, _rotateAngle + 180);
        if (Input.GetKey(KeyCode.DownArrow)) MoveInDirection(Vector3.back, _rotateAngle+90);
    }
    public void MoveInDirection(Vector3 to, int angle)
    {
        Rotate(angle);
        transform.position += to * _speed * Time.fixedDeltaTime;
    }
  
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
            Destroy(gameObject);
        
    }

}
