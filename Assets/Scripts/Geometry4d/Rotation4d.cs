using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public enum Axis
{
	x,
	y,
	z,
	w
}

public class Rotation4d
{

	private Matrix4x4 rotationMatrix;

	public Matrix4x4 RotationMatrix { get => rotationMatrix; }


	public Rotation4d(Vector6 individualRotations)
    {
		rotationMatrix = GetRotationMatrix(individualRotations);
    }

	public Rotation4d()
    {
		rotationMatrix = Matrix4x4.identity;
    }

	/// <summary>
	/// Rotate in plane spanned by local axes: a1 and a2
	/// </summary>
	/// <param name="a1"></param>
	/// <param name="a2"></param>
	public void RotateInLocal(Axis a1, Axis a2, float angle)
    {
		rotationMatrix = rotationMatrix * GetRotationMatrix(a1, a2, angle);
    }

	public Vector4 GetRotatedPoint(Vector4 p)
    {
		return rotationMatrix * p;
    }

	public Vector4 GetUnrotatedPoint(Vector4 p)
    {
		return rotationMatrix.inverse * p;
    }


	private static Matrix4x4 GetRotationMatrix(Vector6 individualRotations, bool reverse = false)
	{
		List<AxisPlane> axes = new List<AxisPlane>
			{new AxisPlane(Axis.x, Axis.y), new AxisPlane(Axis.x, Axis.z), new AxisPlane(Axis.x, Axis.w),
			new AxisPlane(Axis.y, Axis.z), new AxisPlane(Axis.y, Axis.w), new AxisPlane(Axis.z, Axis.w), };


		Matrix4x4 rotation = Matrix4x4.identity;
		for (int i = 0; i < axes.Count; i++)
		{
			int index = reverse ? axes.Count - i - 1 : i;
			rotation = rotation * GetRotationMatrix(axes[index].axis1, axes[index].axis2, individualRotations[index]);
		}
		return rotation;
	}

	private static Matrix4x4 GetRotationMatrix(Axis a1, Axis a2, float angle)
	{
		if (a1 == a2)
		{
			Debug.LogError("Axes of a rotation should not be the same");
			return Matrix4x4.identity;
		}

		// Make a1 < a2
		if ((int)a2 < (int)a1)
		{
			Axis mem = a1;
			a1 = a2;
			a2 = mem;
		}

		float s = Mathf.Sin(angle);
		float c = Mathf.Cos(angle);

		Vector4[] columns = new Vector4[4];
		// init with identity matrix
		for(int i = 0; i < 4; i++)
        {
			for(int j = 0; j < 4; j++)
            {
				columns[i][j] = 0;

				if (i == j)
					columns[i][j] = 1;
            }
        }

		columns[(int)a1][(int)a1] = c;
		columns[(int)a1][(int)a2] = s;
		columns[(int)a2][(int)a1] = -s;
		columns[(int)a2][(int)a2] = c;

		return new Matrix4x4(
			columns[0], columns[1], columns[2], columns[3]
			);
    }


	/// <summary>
	///
	/// </summary>
	/// <param name="point"></param>
	/// <param name="angle">angle in rad</param>
	/// <param name="a1"></param>
	/// <param name="a2">Axis different than a1</param>
	/// <returns></returns>
	public static Vector4 GetRotatedPoint(Vector4 point, float angle, Axis a1, Axis a2)
	{
		return GetRotationMatrix(a1, a2, angle) * point;
	}

	public static Vector4 GetRotatedPoint(Vector4 point, Vector6 rotation, bool reverse = false)
	{
		return GetRotationMatrix(rotation, reverse) * point;
	}

	private struct AxisPlane
	{
		public Axis axis1;
		public Axis axis2;

		public AxisPlane(Axis axis1, Axis axis2)
		{
			this.axis1 = axis1;
			this.axis2 = axis2;
		}
	}
}
