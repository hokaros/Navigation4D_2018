using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallGenerator : MonoBehaviour 
{
	[SerializeField] HyperCuboid wallPrefab;

	public void PlaceWall(Vector4 position, Vector4 size)
    {
		HyperCuboid wall = Instantiate(wallPrefab);
		wall.SetSize(size);
		wall.GetComponent<Transform4>().Position = position;
    }


	private float test_NextWallZ = 0;
	// Update is called once per frame
	void Update () 
	{
        if (Input.GetKeyDown(KeyCode.G))
        {
			PlaceWall(new Vector4(0, 0, test_NextWallZ, 0), new Vector4(4, 1, 1, 1));
			test_NextWallZ += 2;
        }
	}
}
