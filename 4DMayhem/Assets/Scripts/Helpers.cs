using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helpers
{
    private static void AddPermutationSafe(List<Vector4> permutations, List<float> p)
    {
        Vector4 newPermutation = new Vector4(p[0], p[1], p[2], p[3]);
        if(!permutations.Contains(newPermutation))
        {
            permutations.Add(newPermutation);
        }
    }
    private static void GetPermutations(List<Vector4> permutations, List<float> currPermutation, List<float> values)
    {
        if(values.Count > 0)
        {
            for (int i = 0; i < values.Count; i++)
            {
                List<float> nextPermutation = new List<float>(currPermutation);
                nextPermutation.Add(values[i]);
                List<float> nextValues = new List<float>(values);
                nextValues.RemoveAt(i);
                GetPermutations(permutations, nextPermutation, nextValues);
            }
        }
        else
        {
            AddPermutationSafe(permutations, currPermutation);
        } 
    }

    private static void GetPermutationsSigned(List<Vector4> permutations, List<float> currPermutation, List<float> values, bool isEven, bool evenOnly = false)
    {
        if (values.Count > 0)
        {
            for (int i = 0; i < values.Count; i++)
            {
                List<float> nextPermutationPositive = new List<float>(currPermutation);
                List<float> nextPermutationNegative = new List<float>(currPermutation);
                nextPermutationPositive.Add(values[i]);
                nextPermutationNegative.Add(values[i] * -1);
                List<float> nextValuesPositive = new List<float>(values);
                List<float> nextValuesNegative = new List<float>(values);
                nextValuesPositive.RemoveAt(i);
                nextValuesNegative.RemoveAt(i);
                GetPermutationsSigned(permutations, nextPermutationPositive, nextValuesPositive, i % 2 == 0 ? isEven : !isEven, evenOnly);
                GetPermutationsSigned(permutations, nextPermutationNegative, nextValuesNegative, i % 2 == 0 ? isEven : !isEven, evenOnly);
            }
        }
        else if(!evenOnly || isEven)
        {
            AddPermutationSafe(permutations, currPermutation);
        }
    }

    public static void CollectSignedPermutations(List<Vector4> target, Vector4 allowedValues, bool evenOnly = false)
    {
        List<float> currPermutation = new List<float>();
        List<float> values = new List<float>();
        values.Add(allowedValues[0]);
        values.Add(allowedValues[1]);
        values.Add(allowedValues[2]);
        values.Add(allowedValues[3]);
        GetPermutationsSigned(target, currPermutation, values, true, evenOnly);
    }
}
