using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] Transform _player;
    [SerializeField] float _smoothing; 

    // Update is called once per frame
    void LateUpdate()
    {
        Move();
    }

    private void Move()
    {
        transform.position = _player.transform.position + new Vector3(0f, 0f, -10f);
    }
}
