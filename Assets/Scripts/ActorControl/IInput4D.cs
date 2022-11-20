using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

interface IInput4D
{
    float GetXAxis();
    float GetYAxis();
    float GetZAxis();
    float GetWAxis();

    float GetXYRotation();
    float GetXZRotation();
    float GetXWRotation();
    float GetYZRotation();
    float GetYWRotation();
    float GetZWRotation();
}
