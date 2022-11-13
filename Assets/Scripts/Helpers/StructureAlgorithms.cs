using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class StructureAlgorithms
{
    public static bool AreDictionariesEqual(Dictionary<int, List<int>> d1, Dictionary<int, List<int>> d2)
    {
        return DoesContainAll(d1, d2) && DoesContainAll(d2, d1);
    }

    private static bool DoesContainAll(Dictionary<int, List<int>> greater, Dictionary<int, List<int>> smaller)
    {
        foreach (int key in greater.Keys)
        {
            if (!smaller.ContainsKey(key))
                return false;

            List<int> value1 = greater[key];
            List<int> value2 = smaller[key];
            foreach (int value1Elem in value1)
            {
                if (!value2.Contains(value1Elem))
                    return false;
            }
        }

        return true;
    }
}
