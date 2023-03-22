using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] List<Door> _adjacentDoors = new List<Door>();
    [SerializeField] Door _connectedDoor;

    SpriteRenderer _spriteRenderer;
    bool _locked = false;

    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void ChangeColor()
    {
        if (_locked)
        {
            _spriteRenderer.color = Color.red;
            return;
        }

        _spriteRenderer.color = Color.green;
    }

    public void ChangeState()
    {
        if (_locked)
        {
            _locked = false;
            ChangeColor();
            return;
        }

        _locked = true;
        ChangeColor();
    }

    public void Open(Rigidbody2D player)
    {
        if (!_locked)
        {
            foreach(Door door in _adjacentDoors)
            {
                door.ChangeState();
            }
        }
    }
}
