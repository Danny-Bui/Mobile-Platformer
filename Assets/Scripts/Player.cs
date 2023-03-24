using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour, IPointerDownHandler
{
    CharacterController _characterController;
    Rigidbody2D _rigidBody;
    Door _door;
    Restart _goofball;
    Animator _animator;
    PlayerInput _playerInput;
    InputAction _jumpAction, _interactAction, _moveAction;

    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _playerInput = GetComponent<PlayerInput>();

        _moveAction = _playerInput.actions["Move"];

        _jumpAction = _playerInput.actions["Jump"];
        _jumpAction.performed += Jump;
        _jumpAction.Enable();

        _interactAction = _playerInput.actions["Interact"];
        _interactAction.performed += Interact;
        _interactAction.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    // Moves the player and flips the character depending on the direction
    private void Move()
    {
        float xAxis = _moveAction.ReadValue<Vector2>().x;

        if (Input.GetKey(KeyCode.A))
            xAxis--;
        if (Input.GetKey(KeyCode.D))
            xAxis++;

        if (xAxis != 0f)
            _animator.SetBool("Running", true);
        else
            _animator.SetBool("Running", false);

        if (xAxis > 0f)
            transform.localScale = new Vector3(1f, 1f, 1f);
        else if (xAxis < 0f)
            transform.localScale = new Vector3(-1f, 1f, 1f);

        _rigidBody.velocity = new Vector2(xAxis, 0f) * 15f + new Vector2(0, _rigidBody.velocity.y);
    }

    // Jumps
    private void Jump(InputAction.CallbackContext ctx)
    {
        if (CheckGround())
            _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, 35f);
    }

    // Lets the player interact with the door or the lizard
    private void Interact(InputAction.CallbackContext ctx)
    {
        if (_door != null)
            _door.Open(_rigidBody);
        else if (_goofball != null)
            _goofball.RestartScene();
    }

    // Checks if the player is grounded to prevent double jumps
    private bool CheckGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(0f, -2f, 0f), new Vector2(0f, -1f), 0.5f);

        if (hit.collider != null)
            return true;
        else
            return false;
    }

    // Lets the player interact if they're within range of the interactable object
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Door"))
            _door = collision.GetComponent<Door>();

        else if (collision.gameObject.CompareTag("Goofball"))
            _goofball = collision.GetComponent<Restart>();
    }

    // Removes the interactability if the player moves out of the range
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Door"))
            _door = null;

        else if (collision.gameObject.CompareTag("Goofball"))
            _goofball = null;
    }

}
