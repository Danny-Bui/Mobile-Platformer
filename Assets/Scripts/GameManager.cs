using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Door _finalDoor;
    [SerializeField] Door _finalDoor2;

    List<Door> _doors = new List<Door>();
    
    private void Start()
    {
        GameObject[] allDoors = GameObject.FindGameObjectsWithTag("Door");

        foreach (GameObject door in allDoors)
        {
            _doors.Add(door.GetComponent<Door>());
        }
    }

    // Unlocks the final door to win if all the doors are locked
    public void CheckWin()
    {
        bool unlocked = false;

        foreach (Door door in _doors)
        {
            if (!door._locked)
                unlocked = true;
        }

        if (!unlocked)
        {
            Debug.Log("Win");
            _finalDoor.ChangeState();
            _finalDoor2.ChangeState();
        }
    }
}
