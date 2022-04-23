using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{


    public GameObject TilePrefabs;
    public int NumberofRows;
    public int NumberOfColumns;
    public Vector2 offset;

    public GameObject[,] tiles;
    // Start is called before the first frame update
    void Start()
    {
        tiles = new GameObject[NumberofRows, NumberOfColumns];
        GenerateGrid();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateGrid()
	{
		for (int i = 0; i < NumberofRows; i++)
		{
			for (int j = 0; j < NumberOfColumns; j++)
			{
                tiles[i, j] = Instantiate(TilePrefabs, this.transform);
                tiles[i, j].transform.position = new Vector3(i * offset.x, 0, j * offset.y);
			}
		}
	}
}
