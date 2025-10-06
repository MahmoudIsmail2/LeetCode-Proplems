using LeetCode_Proplems;

public class Program
{
    private static void Main(string[] args)
    {
        Solution solution = new Solution();

        solution.MergeTwoLists(new ListNode(1, new ListNode(2, new ListNode(4))), new ListNode(1, new ListNode(3, new ListNode(4))));
    }

}
public class Solution
{
    #region DAY 1
    public int[] TwoSum(int[] nums, int target)
    {

        for (int i = 0; i < nums.Length; i++)
        {

            int diffrence = target - nums[i];
            int result = Array.IndexOf(nums, diffrence);
            if (result != -1 && result != i)
            {
                return new int[] { result, i };
            }

        }
        return new int[] { };
    }

    public bool IsPalindrome(int x)
    {
        var strNum = x.ToString();
        var charArray = strNum.ToCharArray();
        var reversedCharArray = charArray.Reverse().ToArray();
        return charArray.SequenceEqual(reversedCharArray);
    }

    public int RomanToInt(string s)
    {
        Dictionary<char, int> romanMap = new Dictionary<char, int>()
        {
            {'I', 1},
            {'V', 5},
            {'X', 10},
            {'L', 50},
            {'C', 100},
            {'D', 500},
            {'M', 1000}
        };
        int total = 0;
        for (int i = 0; i < s.Length; i++)
        {
            switch (s[i])
            {
                case 'I':
                    if (i != s.Length - 1)
                    {
                        if (s[i + 1] != 'V' && s[i + 1] != 'X')
                        {
                            total += romanMap[s[i]];
                        }
                        else
                        {
                            total += romanMap[s[i + 1]] - romanMap[s[i]];
                            i++;
                        }

                    }
                    else
                    {
                        total += romanMap[s[i]];
                    }

                    break;

                case 'X':
                    if (i != s.Length - 1)
                    {
                        if (s[i + 1] != 'L' && s[i + 1] != 'C')
                        {
                            total += romanMap[s[i]];
                        }
                        else
                        {
                            total += romanMap[s[i + 1]] - romanMap[s[i]];
                            i++;
                        }

                    }
                    else
                    {
                        total += romanMap[s[i]];
                    }

                    break;
                case 'C':
                    if (i != s.Length - 1)
                    {
                        if (s[i + 1] != 'D' && s[i + 1] != 'M')
                        {
                            total += romanMap[s[i]];
                        }
                        else
                        {
                            total += romanMap[s[i + 1]] - romanMap[s[i]];
                            i++;
                        }

                    }
                    else
                    {
                        total += romanMap[s[i]];
                    }

                    break;
                default:
                    total += romanMap[s[i]];
                    break;

            }

        }
        return total;

    }
    #endregion

    #region Day 2 

    public string LongestCommonPrefix(string[] strs)
    {
        var prefix = "";
        for (int i = 0; i < strs[0].Length; i++)
        {
            char currentChar = strs[0][i];
            for (int j = 1; j < strs.Length; j++)
            {
                if (i == strs[j].Length || strs[j][i] != currentChar)
                {
                    return prefix;
                }
            }
            prefix += currentChar;
        }
        return prefix;
    }

    public bool IsValid(string s)
    {
        string openingBrackets = "";             // (       
        if (s[0] != ')' && s[0] != ']' && s[0] != '}')
        {
            char lastOpeningBracket;
            int openingBracketsCount = 0;
            for (int i = 0; i < s.Length; i++)
            {

                switch (s[i])
                {
                    case ')':
                        if (openingBrackets.Length != 0)
                        {
                            lastOpeningBracket = openingBrackets[openingBrackets.Length - 1];
                            if (lastOpeningBracket == '(')
                            {
                                openingBrackets = openingBrackets.Remove(openingBrackets.Length - 1);
                                if (openingBrackets.Length == 0 && i == s.Length - 1)
                                    return true;

                                break;
                            }
                        }
                        return false;
                    case ']':
                        if (openingBrackets.Length != 0)
                        {
                            lastOpeningBracket = openingBrackets[openingBrackets.Length - 1];
                            if (lastOpeningBracket == '[')
                            {
                                openingBrackets = openingBrackets.Remove(openingBrackets.Length - 1);
                                if (openingBrackets.Length == 0 && i == s.Length - 1)
                                    return true;

                                break;
                            }
                        }
                        return false;
                    case '}':
                        if (openingBrackets.Length != 0)
                        {
                            lastOpeningBracket = openingBrackets[openingBrackets.Length - 1];
                            if (lastOpeningBracket == '{')
                            {
                                openingBrackets = openingBrackets.Remove(openingBrackets.Length - 1);
                                if (openingBrackets.Length == 0 && i == s.Length - 1)
                                    return true;

                                break;
                            }
                        }
                        return false;

                    default:
                        openingBrackets += s[i];
                        break;
                }
            }

        }
        return false;

    }

    #region Merge Sorted Lists
    //public ListNode MergeTwoLists(ListNode list1, ListNode list2)
    //{
    //    if(list1==null && list2==null)
    //    {
    //        return null;
    //    }      
    //    else
    //    {
    //        if(list1 is null  || list2 is null)
    //        {
    //            return list1 is not null ? list1 : list2!; 
    //        }           
    //        var mergedListNode = new ListNode();
    //        var currentNode = list1;
    //        while (currentNode.next != null)
    //        {
    //            currentNode = currentNode.next;
    //        }
    //        currentNode.next = list2;              // Last Node in List1 now pointer to Head  of List2
    //        mergedListNode = list1;               // List1 + List 2                    

    //        return SortList(mergedListNode); ;
    //    }

    //}
    // this without Bubble Sort
    public ListNode MergeTwoLists(ListNode list1, ListNode list2)
    {
        if (list1 == null) return list2;
        if (list2 == null) return list1;

        ListNode dummy = new ListNode(0);
        var currentNode = dummy;


        while (list1 != null && list2 != null)
        {
            if (list1.val < list2.val)
            {
                currentNode.next = list1;
                list1 = list1.next;
            }
            else
            {
                currentNode.next = list2;
                list2 = list2.next;
            }
            currentNode = currentNode.next;
        }
        if (list1 == null)
        {
            currentNode.next = list2;
        }
        else
        {
            currentNode.next = list1;
        }
        dummy = dummy.next;
        return dummy;

    }
    private ListNode SortList(ListNode merged, bool isSorted = false)
    {
        if (isSorted)
        {
            return merged;
        }
        else
        {
            var currentNode = merged;                   // Head of List
            while (currentNode.next != null)
            {
                if (currentNode.val > currentNode.next.val)       // if true Swap 2 values
                {
                    var temp = currentNode.val;
                    currentNode.val = currentNode.next.val;
                    currentNode.next.val = temp;
                }
                currentNode = currentNode.next;
            }


            return SortList(merged, IsLinkedListSorted(merged));

        }
    }
    private bool IsLinkedListSorted(ListNode mergedList)
    {
        var head = mergedList;
        while (head.next != null)
        {
            if (head.val > head.next.val)
            {
                return false;
            }
            head = head.next;
        }
        return true;
    }
    public void PrintList(ListNode current)
    {
        while (current != null)
        {
            Console.WriteLine(current.val);
            current = current.next;
        }
    }
    #endregion
    #endregion

    #region Day 3 

    #endregion 



}
public class ListNode
{
    public int val;
    public ListNode next;
    public ListNode(int val = 0, ListNode next = null)
    {
        this.val = val;
        this.next = next;
    }
}
