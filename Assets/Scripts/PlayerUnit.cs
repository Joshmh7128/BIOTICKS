using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// the player unit is a unit object
public class PlayerUnit : UnitObject
{
    public GameObject hoverNotif, engagedNotif; // our floating notifications for hovering and engaged

    public int moveRange; // how far we can move

    PlayerCamera player; 

    private void Start()
    {
        player = PlayerCamera.instance;
    }

    public void Update()
    {
        // are we selected?
        SelectionCheck();
    }

    void SelectionCheck()
    {
        // are we the selected object?
        selected = (player.selectedObject == gameObject);

        // if we are selected, engage icon
        if (selected)
            IsSelected();
    }

    // when the mouse is over the player
    public void OnMouseOver()
    {
        if (!selected)
            IsHover();

        // if the mouse is over and we left click, select this unit
        if (Input.GetMouseButtonDown(0))
        {
            player.SetNewSelection(gameObject);
        }
    }

    public void OnMouseExit()
    {
        if (!selected)
        {
            hoverNotif.SetActive(false);
            engagedNotif.SetActive(false);
        }
    }

    // our function for being hovered on, triggered by the player camera mouse handler
    public void IsHover()
    {
        engagedNotif.SetActive(false);
        hoverNotif.SetActive(true);
    }

    // our function for being selected
    public void IsSelected()
    {
        DisplayMovement();
        engagedNotif.SetActive(true);
        hoverNotif.SetActive(false);

    }
    
    // if we are engaged, show our movement
    public void DisplayMovement()
    {
        // go through the tiles and check the movement
        foreach (TileClass tile in GridManager.instance.tiles)
        {
            if (Vector3.Distance(transform.position,  tile.transform.position) < moveRange * GridManager.instance.gridSpacing)
            tile.moveable = true;
        }
    }
}
