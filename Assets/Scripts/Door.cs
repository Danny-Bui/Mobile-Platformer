using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    GameManager _gameManager;
    [SerializeField] List<Door> _adjacentDoors = new List<Door>();
    [SerializeField] Door _connectedDoor;
    public bool _locked = false;
    SpriteRenderer _spriteRenderer;


    // Start is called before the first frame update
    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        ChangeColor();
    }

    // Changes the door color to indicate whether it's locked or unlocked
    private void ChangeColor()
    {
        if (_locked)
        {
            _spriteRenderer.color = new Color(0.4f, 0.4f, 0.4f);
            return;
        }

        _spriteRenderer.color = Color.white;
    }

    // Locks or unlocks depending on its previous state
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

    // This is used when the player interacts with a door and will lock/unlock the adjacent doors if opened
    public void Open(Rigidbody2D player)
    {
        if (!_locked && !_connectedDoor._locked)
        {
            foreach(Door door in _adjacentDoors)
            {
                door.ChangeState();
            }

            ChangeState();
            player.position = _connectedDoor.transform.position;
            _gameManager.CheckWin();
        }
    }
}
