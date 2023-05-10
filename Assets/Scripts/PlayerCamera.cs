using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{

    public GameObject selectedObject; // which object do we have selected?

    public GameObject selectedTile; // which tile do we have selected

    public bool inSelectMode; // are we in selection mode?

    public static PlayerCamera instance; // this instance

    private void Awake() => instance = this;

    public void SetNewSelection(GameObject g)
    {
        selectedObject = g;
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
    }

    // process out inputs
    void ProcessInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Deselect();
        }
    }

    // deselect
    void Deselect()
    {
        selectedObject = null;
        selectedTile = null;
        // make sure all tiles are no longer displayed moveable
        foreach (TileClass tile in GridManager.instance.tiles)
        {
            // tile.moveable = false;
        }
    }
}
