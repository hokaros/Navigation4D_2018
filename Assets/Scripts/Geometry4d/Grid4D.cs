using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Transform4))]
public class Grid4D : MonoBehaviour 
{
    [SerializeField] private GameObject gridCell;
    [SerializeField] private Vector4 dimensions;
    [SerializeField] private float _offset;

    public float Offset { get { return _offset; } }

    private Transform4 transform4;

    void Start()
    {
        transform4 = GetComponent<Transform4>();
        Vector4 pos = transform4.Position;
        for (int x = 0; x < dimensions[0]; x++)
        {
            for (int y = 0; y < dimensions[1]; y++)
            {
                for (int z = 0; z < dimensions[2]; z++)
                {
                    for (int w = 0; w < dimensions[3]; w++)
                    {
                        GameObject newCell = Instantiate(gridCell);
                        Transform4 t4 = newCell.GetComponent<Transform4>();
                        t4.Position = new Vector4(x * _offset + pos.x, y * _offset + pos.y, z * _offset + pos.z, w * _offset+pos.w);
                    }
                }
            }
        }
    }
}