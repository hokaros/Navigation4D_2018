using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transform4 : MonoBehaviour
{
    [SerializeField] Vector4 position;
    [SerializeField] Vector6 rotation = new Vector6();

    public Vector4 Position
    {
        get => position;
        set => position = value;
    }
    public Vector4 GlobalPosition => PointToWorld(Vector4.zero);
    public Vector6 Rotation
    {
        get => rotation;

        set => rotation = value;
    }
    //public Vector4 Forward { get { return Rotation4d.GetRotatedPoint(Vectors4.Forward, rotation).normalized; } }
    public Vector4 Forward => (PointToWorld(Vectors4.Forward) - GlobalPosition);
    public Vector4 Backward => (PointToWorld(Vectors4.Backward) - GlobalPosition);
    public Vector4 Left => (PointToWorld(Vectors4.Left) - GlobalPosition);
    public Vector4 Right => (PointToWorld(Vectors4.Right) - GlobalPosition);
    public Vector4 Up => (PointToWorld(Vectors4.Up) - GlobalPosition);
    public Vector4 Down => (PointToWorld(Vectors4.Down) - GlobalPosition);
    public Vector4 WPositive => (PointToWorld(Vectors4.WPositive) - GlobalPosition);
    public Vector4 WNegative => (PointToWorld(Vectors4.WNegative) - GlobalPosition);

    /// <summary>
    /// Transforms position from local space to world space
    /// </summary>
    public Vector4 PointToWorld(Vector4 point)
    {
        point = Rotation4d.GetRotatedPoint(point, Rotation);

        Vector4 outerSpacePoint = Position + point;

        if (transform.parent == null)
            return outerSpacePoint;
        return transform.GetParent4().PointToWorld(outerSpacePoint);
    }

    /// <summary>
    /// Transforms position from world space to local space
    /// </summary>
    public Vector4 PointToLocal(Vector4 point)
    {
        // point = Position + toPointLocal
        // toPointLocal = Rotate(toPoint, Rotation)
        // toPoint = pointWorld - Position

        // pointWorld = toPoint + Position
        // toPoint = Rotate(toPointLocal, -Rotation)

        if (transform.parent != null)
            point = transform.GetParent4().Position;

        Vector4 toPointWorld = point - Position;
        return Rotation4d.GetRotatedPoint(toPointWorld, -Rotation, reverse:true);
    }

    public void NormalizeRotation()
    {
        const float fullCycle = 2 * Mathf.PI;

        for(int i = 0; i < 6; i++)
        {
            rotation[i] = rotation[i] % fullCycle;
        }
    }

    public Hyperplane4 GetNativeHyperplane()
    {
        Vector4 localWToWorld = PointToWorld(new Vector4(0, 0, 0, 1)) - GlobalPosition;
        return new Hyperplane4(
            GlobalPosition,
            localWToWorld
        );
    }

    public void Update()
    {

    }
}
