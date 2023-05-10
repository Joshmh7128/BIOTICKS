using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileClass : MonoBehaviour
{
    public int x, y; // our X and Y positions
    public GameObject heldGameObject; // what are we holding?
    public UnitObject heldUnitObject; // our held unit object
    public Transform holdPoint; // where is the unit held when we hold them?

    public bool moveable; // is this tile moveable? used by the player unit to mov

    [SerializeField] GameObject hoverNotif, moveNotif, attackNotif;

    PlayerCamera cam;

    private void Awake() => cam = PlayerCamera.instance;

    // runs after units are placed
    public void TileStart()
    {
        // check and make sure our held object has the coordinates it needs
        if (heldGameObject.GetComponent<UnitObject>() != null)
        {
            // set the object
            heldUnitObject = heldGameObject.GetComponent<UnitObject>();
            // tell it where it is
            heldUnitObject.x = x;
            heldUnitObject.y = y;
        }
    }

    // check if we are being moused over
    public void OnMouseOver()
    {
        hoverNotif.SetActive(true);

        // if we are in selection mode, and left click happens, we become the selected tile on the camera
        if (cam.inSelectMode && Input.GetMouseButtonDown(0))
        {
            cam.selectedTile = gameObject;
            cam.inSelectMode = false;   
        }
    }

    private void OnMouseExit()
    {
        hoverNotif.SetActive(false);
    }

    private void FixedUpdate()
    {
        // show if we are moveable or not
        moveNotif.SetActive(moveable);
    }
}
