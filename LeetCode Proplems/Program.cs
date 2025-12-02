using LeetCode_Proplems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;

namespace LeetCode_Problems
{
    public class Program
    {
        private static void Main(string[] args)
        {


        //    var date = new DateTime(2020, 10, 12);
        //    bool isWeekend = date.IsWeekend();
           var solution = new Solution();

            //string s = "jar", t = "jam";
            //solution.IsAnagram(s, t);


            //string s = "No lemon, no melon";
            //solution.IsPalindrome(s);

            solution.Trap(new int[] { 0, 2, 0, 3, 1, 0, 1, 3, 2, 1 });
        }
    }

    public class ListNode
    {
        public int val;
        public ListNode? next;
        public ListNode(int val = 0, ListNode? next = null)
        {
            this.val = val;
            this.next = next;
        }
    }

    public class Solution
    {
        // ---------------- Day 1 ----------------
        //  TwoSum
        public int[] TwoSum(int[] nums, int target)
        {

            var seen = new Dictionary<int, int>();
            for (int i = 0; i < nums.Length; i++)
            {
                int need = target - nums[i];
                if (seen.TryGetValue(need, out int j)) return new[] { j, i };
                if (!seen.ContainsKey(nums[i])) seen[nums[i]] = i;
            }
            return Array.Empty<int>();
        }
        // Two Integer Sum I
        public int[] TwoSum2(int[] numbers, int target)
        {

            int left = 0, right = numbers.Length - 1;
            while (left < right)
            {
                if (numbers[left] + numbers[right] > target)
                {
                    right--;
                }
                else if (numbers[left] + numbers[right] < target)
                {
                    left++;
                }
                else
                {
                    return new int[] { left + 1, right + 1 };
                }
            }
            return new int[0];
        }

        public bool IsPalindrome(string s)
        {
            int left = 0, right = s.Length - 1;
            while (left < right)
            {
                while (left < right && !char.IsLetterOrDigit(s[left]))
                { left++; }

                while (left < right && !char.IsLetterOrDigit(s[right]))
                { right--; }

                if (char.ToLower(s[left]) != char.ToLower(s[right]))

                {
                    return false;
                }
                left++;
                right--;
            }
            return true;
        }
        public int RomanToInt(string s)
        {
            var map = new Dictionary<char, int>
            {
                ['I'] = 1,
                ['V'] = 5,
                ['X'] = 10,
                ['L'] = 50,
                ['C'] = 100,
                ['D'] = 500,
                ['M'] = 1000
            };
            int total = 0;
            for (int i = 0; i < s.Length; i++)
            {
                int val = map[s[i]];
                if (i + 1 < s.Length && map[s[i + 1]] > val) total -= val;
                else total += val;
            }
            return total;
        }

        // ---------------- Day 2 ----------------
        public string LongestCommonPrefix(string[] strs)
        {
            if (strs == null || strs.Length == 0) return "";
            string prefix = strs[0];
            for (int i = 1; i < strs.Length && prefix.Length > 0; i++)
            {
                while (!strs[i].StartsWith(prefix, StringComparison.Ordinal))
                    prefix = prefix[..^1];
            }
            return prefix;
        }

        public bool IsValid(string s)
        {
            var stack = new Stack<char>();
            var pairs = new Dictionary<char, char> { [')'] = '(', [']'] = '[', ['}'] = '{' };
            foreach (char c in s)
            {
                if (pairs.ContainsValue(c)) stack.Push(c);
                else if (pairs.TryGetValue(c, out char want))
                {
                    if (stack.Count == 0 || stack.Pop() != want) return false;
                }
                else return false;
            }
            return stack.Count == 0;
        }

        public ListNode? MergeTwoLists(ListNode? list1, ListNode? list2)
        {
            var dummy = new ListNode();
            var cur = dummy;
            while (list1 != null && list2 != null)
            {
                if (list1.val <= list2.val) { cur.next = list1; list1 = list1.next; }
                else { cur.next = list2; list2 = list2.next; }
                cur = cur.next!;
            }
            cur.next = list1 ?? list2;
            return dummy.next;
        }

        // ---------------- Day 3 ----------------
        public int MaxProfit(int[] prices)
        {
            int minP = int.MaxValue, best = 0;
            foreach (var p in prices) { minP = Math.Min(minP, p); best = Math.Max(best, p - minP); }
            return best;
        }

        public ListNode? SortList(ListNode? head)
        {
            if (head == null || head.next == null) return head;
            // split
            var slow = head; var fast = head.next;
            while (fast != null && fast.next != null) { slow = slow.next!; fast = fast.next.next; }
            var second = slow.next; slow.next = null;
            // sort halves
            var l1 = SortList(head);
            var l2 = SortList(second);
            // merge
            return Merge(l1, l2);

            static ListNode? Merge(ListNode? a, ListNode? b)
            {
                var dummy = new ListNode(); var t = dummy;
                while (a != null && b != null)
                {
                    if (a.val <= b.val) { t.next = a; a = a.next; }
                    else { t.next = b; b = b.next; }
                    t = t.next!;
                }
                t.next = a ?? b;
                return dummy.next;
            }
        }

        // ---------------- Day 4 ----------------
        // Remove all duplicates such that only distinct numbers remain (LeetCode 82).
        public ListNode? DeleteDuplicates(ListNode? head)
        {
            var dummy = new ListNode(0, head);
            var prev = dummy;
            while (head != null)
            {
                if (head.next != null && head.val == head.next.val)
                {
                    int v = head.val;
                    while (head != null && head.val == v) head = head.next;
                    prev.next = head;
                }
                else
                {
                    prev = head;
                    head = head.next;
                }
            }
            return dummy.next;
        }

        // ---------------- Day 5 ----------------
        public void SortColors(int[] nums) // Dutch National Flag
        {
            int l = 0, i = 0, r = nums.Length - 1;
            while (i <= r)
            {
                if (nums[i] == 0) { (nums[l], nums[i]) = (nums[i], nums[l]); l++; i++; }
                else if (nums[i] == 2) { (nums[r], nums[i]) = (nums[i], nums[r]); r--; }
                else i++;
            }
        }

        // ---------------- Day 6 ----------------
        public int MaxArea(int[] height)
        {
            int l = 0, r = height.Length - 1, best = 0;
            while (l < r)
            {
                int h = Math.Min(height[l], height[r]);
                best = Math.Max(best, h * (r - l));
                if (height[l] < height[r]) l++; else r--;
            }
            return best;
        }

        public int[] MergetwoSortedArrayes(int[] nums1, int[] nums2)
        {
            int i = 0, j = 0, k = 0;
            var merged = new int[nums1.Length + nums2.Length];
            while (i < nums1.Length && j < nums2.Length)
                merged[k++] = (nums1[i] <= nums2[j]) ? nums1[i++] : nums2[j++];
            while (i < nums1.Length) merged[k++] = nums1[i++];
            while (j < nums2.Length) merged[k++] = nums2[j++];
            return merged;
        }

        public double FindMedianSortedArrays(int[] nums1, int[] nums2)
        {
            var m = MergetwoSortedArrayes(nums1, nums2);
            int n = m.Length;
            return (n % 2 == 1) ? m[n / 2] : (m[n / 2 - 1] + m[n / 2]) / 2.0;
        }

        // ---------------- Day 7 ----------------
        public ListNode? AddTwoNumbers(ListNode? l1, ListNode? l2)
        {
            int carry = 0;
            var dummy = new ListNode();
            var cur = dummy;
            while (l1 != null || l2 != null || carry > 0)
            {
                int sum = (l1?.val ?? 0) + (l2?.val ?? 0) + carry;
                carry = sum / 10;
                cur.next = new ListNode(sum % 10);
                cur = cur.next;
                l1 = l1?.next;
                l2 = l2?.next;
            }
            return dummy.next;
        }

        public string ReverseVowels(string s)
        {
            var set = new HashSet<char>("aeiouAEIOU");
            var arr = s.ToCharArray();
            int l = 0, r = arr.Length - 1;
            while (l < r)
            {
                if (!set.Contains(arr[l])) { l++; continue; }
                if (!set.Contains(arr[r])) { r--; continue; }
                (arr[l], arr[r]) = (arr[r], arr[l]);
                l++; r--;
            }
            return new string(arr);
        }

        // ---------------- Day 8 ----------------
        public int LengthOfLongestSubstring(string s)
        {
            var last = new Dictionary<char, int>();
            int best = 0, start = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (last.TryGetValue(s[i], out int prev) && prev >= start) start = prev + 1;
                last[s[i]] = i;
                best = Math.Max(best, i - start + 1);
            }
            return best;
        }

        // ---------------- Day 9 ----------------
       // public int[] TwoSum2(int[] nums, int target) => TwoSum(nums, target);

        public IList<IList<string>> GroupAnagrams(string[] strs)
        {
            var dict = new Dictionary<string, List<string>>();

            foreach (var s in strs)
            {
                var key = new string(s.OrderBy(c => c).ToArray());
                if (!dict.ContainsKey(key))
                    dict[key] = new List<string>();

                dict[key].Add(s);
            }

            return dict.Values
                       .Select(list => (IList<string>)list)
                       .ToList();
        }


        public int[] TopKFrequent(int[] nums, int k)
        {
            var freq = new Dictionary<int, int>();
            foreach (var n in nums) freq[n] = freq.GetValueOrDefault(n) + 1;

            var buckets = new List<int>[nums.Length + 1];
            foreach (var (num, f) in freq)
            {
                buckets[f] ??= new List<int>();
                buckets[f].Add(num);
            }

            var res = new List<int>(k);
            for (int f = buckets.Length - 1; f >= 0 && res.Count < k; f--)
                if (buckets[f] != null)
                    foreach (var n in buckets[f])
                    {
                        res.Add(n);
                        if (res.Count == k) break;
                    }
            return res.ToArray();
        }

        public string Encode(IList<string> strs)
        {
            var sb = new StringBuilder();
            foreach (var s in strs) sb.Append(s.Length).Append('#').Append(s);
            return sb.ToString();
        }

        public IList<string> Decode(string encoded)
        {
            var ans = new List<string>();
            int i = 0;
            while (i < encoded.Length)
            {
                int j = i;
                while (encoded[j] != '#') j++;
                int len = int.Parse(encoded[i..j]);
                string str = encoded.Substring(j + 1, len);
                ans.Add(str);
                i = j + 1 + len;
            }
            return ans;
        }

        // ---------------- Day 10 ----------------
        public int[] ProductExceptSelf(int[] nums)
        {
            int n = nums.Length;
            var left = new int[n];
            var right = new int[n];
            left[0] = 1;
            for (int i = 1; i < n; i++) left[i] = left[i - 1] * nums[i - 1];
            right[n - 1] = 1;
            for (int i = n - 2; i >= 0; i--) right[i] = right[i + 1] * nums[i + 1];
            var ans = new int[n];
            for (int i = 0; i < n; i++) ans[i] = left[i] * right[i];
            return ans;
        }



        // ---------------- Day 11 ----------------

        #region Suduoku
        public bool IsValidSudoku(char[][] board)
        {
            // Check all rows
            for (int i = 0; i < 9; i++)
            {
                if (!ValidateArray(board[i]))
                    return false;
            }

            // Check all columns
            for (int j = 0; j < 9; j++)
            {
                if (!ValidateArray(GetColumn(j, board)))
                    return false;
            }

            // Check all 9 sub-boxes (once!)
            if (!validateSubBoxs(board))
                return false;

            return true;
        }
        private bool ValidateArray(char[] row)
        {
            HashSet<char> values = new HashSet<char>();
            foreach (char c in row)
            {
                if (c == '.') continue;
                if (!values.Add(c)) return false;

            }
            return true;

        }
        private char[] GetColumn(int col, char[][] board)
        {
            var column = new char[board.Length];
            for (int i = 0; i < board.Length; i++)
            {
                column[i] = board[i][col];
            }
            return column;
        }
        private bool validateSubBoxs(char[][] board)
        {
            // Check all 9 sub-boxes
            for (int boxRow = 0; boxRow < 3; boxRow++)      // 3 rows of boxes
            {
                for (int boxCol = 0; boxCol < 3; boxCol++)  // 3 columns of boxes
                {
                    if (!ValidateSubBox(board, boxRow * 3, boxCol * 3))
                        return false;
                }
            }
            return true;
        }
        private bool ValidateSubBox(char[][] board, int startRow, int startCol)
        {
            // Extract the 3x3 box into a 1D array
            var subBox = new char[9];
            int index = 0;

            for (int i = startRow; i < startRow + 3; i++)
            {
                for (int j = startCol; j < startCol + 3; j++)
                {
                    subBox[index++] = board[i][j];
                }
            }

            // Reuse your ValidateArray method!
            return ValidateArray(subBox);
        }
        #endregion



        //----------------- Day 12 -----------------------

        public int LongestConsecutive(int[] nums)
        {
            HashSet<int> lst = new HashSet<int>();             //  { 9, 1, 4, 7, 3, -1, 0, 5, 8, -1, 6 }

            // sort Array
            int longest = 0;
            Array.Sort(nums);


            for (int i = 0; i < nums.Length; i++)
            {
                if (i == 0 || lst.Count() == 0)
                {
                    lst.Add(nums[i]);
                }
                else
                {
                    var isSequenced = lst.Contains(nums[i] - 1) || lst.Contains(nums[i]);

                    if (isSequenced)
                    {
                        lst.Add(nums[i]);
                    }
                    else
                    {
                        longest = Math.Max(longest, lst.Count());
                        lst.Clear();
                        lst.Add(nums[i]);
                    }
                }
            }
            return Math.Max(longest, lst.Count());
        }
        public bool ContainsDuplicate(int[] nums)
        {
            var set = new HashSet<int>();
            foreach (var num in nums)
            {
                if (set.Contains(num)) return true;

                set.Add(num);
            }
            return false;
        }
        public bool IsAnagram(string s, string t)
        {

            var dic1 = new Dictionary<char, int>();            // char , freq
            var dic2 = new Dictionary<char, int>();

            if (s.Length != t.Length) return false;
            // Fill Dictionaries
            for (int i = 0; i < s.Length; i++)
            {
                if (dic1.ContainsKey(s[i]))
                {
                    dic1[s[i]]++;
                }
                else
                {
                    dic1.Add(s[i], 1);
                }


                if (dic2.ContainsKey(t[i]))
                {
                    dic2[t[i]]++;
                }
                else
                {
                    dic2.Add(t[i], 1);
                }
            }

            foreach (var k in dic1)
            {
                if (dic2.ContainsKey(k.Key))
                {

                    dic1.TryGetValue(k.Key, out int dic1val);
                    dic2.TryGetValue(k.Key, out int dic2val);

                    if (dic1val != dic2val) return false;

                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        // [0,2,0,3,1,0,1,3,2,1]
        public int Trap(int[] height)
        {
            int rainAmount = 0;
            int left = 0, right = 0;
            var lstInBetween = new List<int>();
            for (int i = 0; i < height.Length; i++)
            {
                if (height[i] != 0)
                {
                    if (left == 0) left = i;

                    for (int j = i + 1; height[j] < height[left]; j++)
                    {
                        lstInBetween.Add(height[j]);
                        right = j + 1;
                    }

                    if (left != 0 && right != 0)
                    {
                        // calculate 
                        int h= Math.Min(height[left], height[right]);
                        int w = lstInBetween.Count();

                        int amounttodecrement = 0;
                        foreach(var item in lstInBetween)
                        {
                            amounttodecrement += item;
                        }
                        rainAmount += h * w -amounttodecrement;

                        left = right;
                        right = 0;
                        i = left-1;
                        lstInBetween.Clear();
                    }
                }

            }
            return rainAmount;

        }
        // helper
        public void PrintList(ListNode? node)
        {
            while (node != null) { Console.Write(node.val + (node.next != null ? " -> " : "")); node = node.next; }
            Console.WriteLine();
        }
    }
}
