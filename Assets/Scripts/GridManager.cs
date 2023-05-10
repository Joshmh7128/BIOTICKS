using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    // our grid variables
    [Header("Grid Settings")]
    [SerializeField] int gridSize; // the grid size on the X and Y
    [SerializeField] GameObject tilePrefab; // our tile prefab
    public TileClass[,] tiles; // the array in which we will keep our tiles
    public float gridSpacing; // how spaced out is the grid?

    [Header("Unit Settings")]
    [SerializeField] int playerCount; 
    [SerializeField] int enemyCount;
    [SerializeField] GameObject playerUnitPrefab, enemyUnitPrefab;


    // instance
    public static GridManager instance;
    private void Awake() => instance = this;


    // start runs at the start of the game
    private void Start()
    {
        tiles = new TileClass[gridSize, gridSize];

        // build our grid
        BuildGrid();

        // place our player units
        PlacePlayerUnits();

        // once players are placed, run all of the TileStart() functions
        foreach (TileClass tile in tiles)
            tile.TileStart();
    }

    // build our grid
    void BuildGrid()
    {
        // starting from the bottom right, build out a grid
        // double loop, move column by column building out the level
        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                // create a new tile
                Transform t = Instantiate(tilePrefab, new Vector3(x * gridSpacing, 0, y * gridSpacing), Quaternion.identity).transform;
                // set our world position
                t.parent = transform;
                // set our array position
                t.GetComponent<TileClass>().x = x;
                t.GetComponent<TileClass>().y = y;
                // add our tile to the array
                tiles[x, y] = t.gameObject.GetComponent<TileClass>();
            }
        }
    }

    // place our player units on the top
    void PlacePlayerUnits()
    {
        for (int i = 0; i < playerCount; i++)
        {
            // choose a random tile
            int x = (int)Random.Range(0f, gridSize);
            int y = (int)Random.Range(gridSize, gridSize - (float)gridSize / 3);

            Debug.Log("checking: " + x + "," + y);

            // check to make sure that tile has nothing on it
            if (tiles[x, y].heldGameObject == null)
            {
                // spawn and set this as the held object
                tiles[x, y].heldGameObject = Instantiate(playerUnitPrefab, tiles[x, y].holdPoint);
            }
            else
            {
                // we want this to run again
                i--;
            }
        }
    }

}
