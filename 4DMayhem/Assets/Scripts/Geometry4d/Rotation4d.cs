using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Rotation4d
{
	public enum Axis
	{
		x,
		y,
		z,
		w
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
		if (a1 == a2)
		{
			Debug.LogError("Axes of a rotation should not be the same");
			return point;
		}

		if ((int)a2 < (int)a1)
		{
			Axis mem = a1;
			a1 = a2;
			a2 = mem;
		}

		Vector4 res = new Vector4(point.x, point.y, point.z, point.w);

		float s = Mathf.Sin(angle);
		float c = Mathf.Cos(angle);

		res[(int)a1] = point[(int)a1] * c + point[(int)a2] * (-s);
		res[(int)a2] = point[(int)a1] * s + point[(int)a2] * c;

		return res;
	}

	public static Vector4 GetRotatedPoint(Vector4 point, Vector6 rotation, bool reverse = false)
	{
		List<AxisPlane> axes = new List<AxisPlane> 
			{new AxisPlane(Axis.x, Axis.y), new AxisPlane(Axis.x, Axis.z), new AxisPlane(Axis.x, Axis.w),
			new AxisPlane(Axis.y, Axis.z), new AxisPlane(Axis.y, Axis.w), new AxisPlane(Axis.z, Axis.w), };
		
		
		for (int i = 0; i < axes.Count; i++)
		{
			int index = reverse ? axes.Count - i -1: i;
			point = GetRotatedPoint(point, rotation[index], axes[index].axis1, axes[index].axis2);
		}
		return point;
	}
}
