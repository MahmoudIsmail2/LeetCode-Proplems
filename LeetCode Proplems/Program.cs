using LeetCode_Proplems;
using System.Diagnostics.Metrics;
using System.Numerics;
using System.Runtime.CompilerServices;

public class Program
{
    private static void Main(string[] args)
    {
        Solution solution = new Solution();

        // solution.SortList(new ListNode(-1, new ListNode(5, new ListNode(3, new ListNode(4, new ListNode(0))))));
        //var result = solution.DeleteDuplicates(new ListNode(1, new ListNode(1, new ListNode(2, new ListNode(3, new ListNode(3))))));

        //var result = solution.DeleteDuplicates(new ListNode(1, new ListNode(1, new ListNode(2))));
        //var result = solution.DeleteDuplicates(new ListNode(1, new ListNode(2, new ListNode(3, new ListNode(3, new ListNode(4, new ListNode(4, new ListNode(5))))))));
        //solution.PrintList(result);

        // solution.SortColors(new int[] { 2, 0, 2, 1, 1, 0 });
        //var area=solution.MaxArea(new int[] { 1, 8, 6, 2, 5, 4, 8, 3, 7 });
        //Console.WriteLine(  $"area is : {area}");
        //var merged=  solution.MergetwoSortedArrayes([1,3], [2]);
        //  foreach(var item in merged)
        //  {
        //      Console.WriteLine(item);
        //  }
        //double med= solution.FindMedianSortedArrays(new int[] { 1, 2 }, new int[] { 3, 4 });
        //Console.WriteLine(med);




        Console.WriteLine(solution.LengthOfLongestSubstring("ynyo")); 
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
    public ListNode SortMergedList(ListNode merged, bool isSorted = false)
    {
        if (isSorted || merged == null || merged.next == null)
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


            return SortMergedList(merged, IsLinkedListSorted(merged));

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

    public int MaxProfit(int[] prices)
    {
        int minPrice = int.MaxValue;
        int maxProfit = 0;
        for (int i = 0; i < prices.Length; i++)
        {
            minPrice = Math.Min(minPrice, prices[i]);
            maxProfit = Math.Max(maxProfit, prices[i] - minPrice);
        }
        return maxProfit;
    }

    public ListNode SortList(ListNode head)
    {
        // Divide List into 2 halves
        if (head is null || head.next is null) return head;

        var slow = head;
        var fast = head;

        while (fast != null && fast.next != null && fast.next.next != null)
        {
            slow = slow.next;
            fast = fast.next.next;
        }
        // now slow is at middle
        var second = slow.next;
        slow.next = null;
        var first = head;                  // Now i have 2 lists



        // Step 3: Recursively sort both halves
        first = SortList(first);
        second = SortList(second);

        // Step 4: Merge the sorted halves
        return Merge(first, second);


    }
    private ListNode Merge(ListNode l1, ListNode l2)
    {
        var dummy = new ListNode(0);
        var tail = dummy;

        while (l1 != null && l2 != null)
        {
            if (l1.val <= l2.val)
            {
                tail.next = l1;
                l1 = l1.next;
            }
            else
            {
                tail.next = l2;
                l2 = l2.next;
            }
            tail = tail.next;
        }

        // Attach the remaining part of whichever list is not empty
        tail.next = l1 ?? l2;

        return dummy.next;
    }
    #endregion

    #region Day 4
    //public ListNode DeleteDuplicates(ListNode head)
    //{
    //    if (head == null) return head;
    //    var output = head;
    //    while (head.next != null)
    //    {
    //        if (head.val == head.next.val)
    //        {
    //            head.next = head.next.next;
    //        }
    //        else
    //        {
    //            head = head.next;
    //        }
    //    }
    //    return output;

    //}
    public ListNode DeleteDuplicates(ListNode head)
    {
        var dummy = new ListNode(0, head);
        var prev = dummy;
        var current = head;

        while (current != null)
        {
            // Check if this node is the start of duplicates
            if (current.next != null && current.val == current.next.val)
            {
                // Skip all nodes with the same value
                while (current.next != null && current.val == current.next.val)
                {
                    current = current.next;
                }
                // Bypass the duplicates
                prev.next = current.next;
            }
            else
            {
                // No duplicates — move prev forward
                prev = prev.next;
            }
            // Move current forward
            current = current.next;
        }

        return dummy.next;
    }


    #endregion


    #region Day 5
    public void SortColors(int[] nums)
    {
        // Buble Sort

        //var n=nums.Length;
        //for(int i =0; i<n-1; i++)
        //{
        //    for(int j=0; j<n-i-1; j++)
        //    {
        //        if (nums[j] > nums[j+1])
        //        {
        //            var temp = nums[j];
        //            nums[j] = nums[j + 1];
        //            nums[j + 1] = temp;
        //        }
        //    }
        //} 



    }

    #endregion

    #region Day 6

    public int MaxArea(int[] height)
    {
        // Brute Force

        //var n = height.Length;
        //var area = 0;
        //for(int i=0; i < n; i++)
        //{
        //    for (int j=i+1;j<n; j++)
        //    {
        //        var h = Math.Min(height[i], height[j]);
        //        var w = j - i;
        //        area = Math.Max(area, h * w);
        //    }
        //}
        //return area;

        // Optimal Solution
        int left = 0;
        int right = height.Length - 1;
        var area = 0;
        while (left < right)
        {
            if (height[left] < height[right])
            {
                var h = height[left];
                var w = right - left;
                area = Math.Max(area, h * w);
                left++;
            }
            else
            {
                var h = height[right];
                var w = right - left;
                area = Math.Max(area, h * w);
                right--;
            }
        }
        return area;
    }

    public int[] MergetwoSortedArrayes(int[] nums1, int[] nums2)
    {
        int len1 = nums1.Length;
        int len2 = nums2.Length;

        int left = 0;
        int right = 0;
        int i = 0;
        int looplimit = Math.Max(len1, len2);
        int[] merge = new int[len1 + len2];

        while (left < len1 && right < len2)
        {
            if (nums1[left] < nums2[right])
            {
                merge[i] = nums1[left];
                left++;
            }
            else
            {
                merge[i] = nums2[right];
                right++;
            }
            i++;
        }
        // Copy the remaining elements
        if (left < len1)
        {
            for (int j = left; j < len1; j++)
            {
                merge[i] = nums1[j];
                i++;
            }
        }
        else if (right < len2)
        {
            for (int j = right; j < len2; j++)
            {
                merge[i] = nums2[j];
                i++;
            }
        }
        else
        {
            return merge;
        }
        return merge;
    }
    public double FindMedianSortedArrays(int[] nums1, int[] nums2)
    {
        var merged = MergetwoSortedArrayes(nums1, nums2);
        int n = merged.Length;

        if (n % 2 == 0)
        {
            // Even number of elements → average of middle two
            return (merged[n / 2] + merged[(n / 2) - 1]) / 2.0;
        }
        else
        {
            // Odd number of elements → middle element
            return merged[n / 2];
        }
    }

    #endregion

    #region Day 7
    public ListNode AddTwoNumbers(ListNode l1, ListNode l2)
    {
        var num1 = reversestring(GetNumberFromList(l1));
        var num2 = reversestring(GetNumberFromList(l2));
        BigInteger sum = BigInteger.Parse(num1) + BigInteger.Parse(num2);

        string reversedSumString = reversestring(sum.ToString());


        ListNode newList = new ListNode();
        var current = newList;
        foreach (var i in reversedSumString)
        {

            current.next = new ListNode(int.Parse(i.ToString()));
            current = current.next;
        }
        return newList.next;
    }
    private string GetNumberFromList(ListNode listNode)
    {
        string number = "";
        var current = listNode;
        while (current != null)
        {
            number += current.val;
            current = current.next;
        }
        return number;
    }
    private string reversestring(string s)
    {
        char[] charArray = s.ToCharArray();
        Array.Reverse(charArray);
        string reversedString = new string(charArray);
        return reversedString;
    }


    public string ReverseVowels(string s)
    {

        var vowels = new char[] { 'a', 'e', 'i', 'o', 'u', 'A', 'E', 'I', 'O', 'U' };
        int left = 0;
        int right = s.Length - 1;
        char[] charArray = s.ToCharArray();
        while (left < right)
        {
            if (vowels.Contains(s[left]))
            {
                if (vowels.Contains(s[right]))
                {

                    var temp = s[left];
                    charArray[left] = s[right];
                    charArray[right] = temp;

                    left++;
                    right--;

                }
                else { right--; }
            }
            else


            {
                left++;
            }
        }
        return new string(charArray);
    }
    #endregion


    #region Day 8
    public int LengthOfLongestSubstring(string s)   
    {
        if(s.Length==0 || string.IsNullOrEmpty(s)) return 0;
        string subString = "";
        var SubStringsDictionary = new Dictionary<Guid, string>();

        for (int i = 0; i < s.Length; i++)
        {
            var lastSubString = SubStringsDictionary.Values.Max() ?? "";
            for (int j = i; j <s. Length; j++)
            {               
                if (subString.Contains(s[j]))
                {
                    if (lastSubString.Length < subString.Length )
                    {

                        SubStringsDictionary.Add(Guid.NewGuid(), subString);
                    }
                    subString = "";
                    break;
                }
                else
                {
                    subString += s[j];
                }
            }
            if (lastSubString.Length < subString.Length)
            {
                SubStringsDictionary.Add(Guid.NewGuid(), subString);
            }
        }




        var max = SubStringsDictionary.Values.MaxBy(s => s.Length) ?? "";



        return max.Length > subString.Length ? max.Length : subString.Length;

    }
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
