using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallGenerator : MonoBehaviour 
{
	[SerializeField] HyperCuboid wallPrefab;
	[SerializeField] float longEdge;
	[SerializeField] float shortEdge;
	[SerializeField] float perpendicularEdge;

	private List<HyperCuboid> walls;

	public void PlaceWall(Vector4 position, Vector4 size)
    {
		HyperCuboid wall = Instantiate(wallPrefab);
		wall.SetSize(size);
		wall.GetComponent<Transform4>().Position = position;
		walls.Add(wall);
    }

	

	public void ClearWalls()
    {
		for(int i = walls.Count-1; i>=0; i--)
        {
			HyperCuboid wallToRemove = walls[i];
			walls.RemoveAt(i);
			Destroy(wallToRemove.gameObject);
        }
    }

	private Vector4 CalculateWallSize(Vector4 faceDirection)
    {
		// find most perpendicular axis
		int perpendicularAxisIndex = 0;
		float maxDist = Mathf.Abs(faceDirection.x);
		for (int i = 1; i < 4; i++)
        {
			float dist = Mathf.Abs(faceDirection[i]);
            if (dist > maxDist)
            {
				maxDist = dist;
				perpendicularAxisIndex = i;
            }
		}

		// choose short axis
		int shortAxisIndex = (perpendicularAxisIndex + Random.Range(0,3)) % 4;

		// assign
		Vector4 result = Vector4.zero;
		for (int i = 0; i < 4; i++)
		{
			if (i == perpendicularAxisIndex)
			{
				result[i] = perpendicularEdge;
			}
			else if (i == shortAxisIndex)
			{
				result[i] = shortEdge;
			}
			else
			{
				result[i] = longEdge;
			}
		}
		return result;
    }

	private void SpawnWallsInDirection(Vector4 playerPos, Vector4 facePos, int nLayers)
    {
		float spawnRadius = Vector4.Distance(playerPos, facePos) / (nLayers + 1);
		for (int i = 0; i < nLayers; i++) {
			Vector4 wallPosition = Vector4.Lerp(playerPos, facePos, (i+1)*spawnRadius/ Vector4.Distance(playerPos, facePos));
			PlaceWall(wallPosition, CalculateWallSize(facePos - playerPos));
		}
	}

	public void SpawnWalls(Vector4 playerPos, Vector4 targetPos, int nLayers)
    {
		SpawnWallsInDirection(playerPos, targetPos, nLayers);
		SpawnWallsInDirection(playerPos, new Vector4(targetPos.x, targetPos.y, -targetPos.z, targetPos.w), nLayers);
	}

	private void Awake()
    {
		walls = new List<HyperCuboid>();
    }

	// Update is called once per frame
	void Update () 
	{
   //     if (Input.GetKeyDown(KeyCode.G))
   //     {
			//PlaceWall(new Vector4(0, 0, test_NextWallZ, 0), new Vector4(4, 1, 1, 1));
			//test_NextWallZ += 2;
   //     }
	}
}
