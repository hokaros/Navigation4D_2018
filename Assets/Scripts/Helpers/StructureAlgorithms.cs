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

    public static bool AreListsOfListsEqual(List<List<int>> l1, List<List<int>> l2, out int firstInvalid, out bool notFoundL1Elem)
    {
        if(!DoesContainAll(l1, l2, out firstInvalid))
        {
            notFoundL1Elem = true;
            return false;
        }
        if (!DoesContainAll(l2, l1, out firstInvalid))
        {
            notFoundL1Elem = false;
            return false;
        }

        // Equal
        firstInvalid = -1;
        notFoundL1Elem = false;
        return true;
    }

    public static bool AreListsEqual(List<int> l1, List<int> l2)
    {
        if (l1.Count != l2.Count)
            return false;

        List<int> sortedL1 = new List<int>(l1);
        sortedL1.Sort();
        List<int> sortedL2 = new List<int>(l2);
        sortedL2.Sort();

        for(int i = 0; i < sortedL1.Count; i++)
        {
            int val1 = sortedL1[i];

            for(int j = 0; j < sortedL2.Count; j++)
            {
                int val2 = sortedL2[j];

                if (val1 == val2)
                    break;  // val1 found in the second list

                if (val2 > val1)
                    return false;
            }
        }

        return true;
    }

    public static bool ContainsDuplicates(List<int> list)
    {
        for(int i = 0; i < list.Count; i++)
        {
            for(int j = i + 1; j < list.Count; j++)
            {
                if (list[i] == list[j])
                    return true;
            }
        }

        return false;
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

    private static bool DoesContainAll(List<List<int>> l1, List<List<int>> l2, out int firstInvalid)
    {
        for (int i = 0; i < l1.Count; i++)
        {
            List<int> sublist1 = l1[i];

            bool found = false;
            for (int j = 0; j < l2.Count; j++)
            {
                List<int> sublist2 = l2[j];

                if (AreListsEqual(sublist1, sublist2))
                {
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                firstInvalid = i;
                return false;
            }
        }

        firstInvalid = -1;
        return true;
    }
}
