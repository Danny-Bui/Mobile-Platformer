using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] Transform _player;
    [SerializeField] float _smoothing; 

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, _player.position, _smoothing) + new Vector3(0f,0f,-10f);
    }
}
