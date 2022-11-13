using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public struct Edge
{
    public int startId;
    public int endId;

    public Edge(int startId, int endId)
    {
        this.startId = startId;
        this.endId = endId;
    }
}

public struct VertexEdge4
{
    public Vector4 start;
    public Vector4 end;

    public VertexEdge4(Vector4 start, Vector4 end)
    {
        this.start = start;
        this.end = end;
    }
}

public struct VertexEdge3
{
    public Vector3 start;
    public Vector3 end;

    public VertexEdge3(Vector4 start, Vector4 end)
    {
        this.start = start;
        this.end = end;
    }
}
