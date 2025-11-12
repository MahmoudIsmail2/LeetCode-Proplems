using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace LeetCode_Problems
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var solution = new Solution();

            //// sanity checks
            //var prod = solution.ProductExceptSelf(new[] { 1, 2, 3, 4 });
            //Console.WriteLine($"ProductExceptSelf: [{string.Join(", ", prod)}]");

            //var topK = solution.TopKFrequent(new[] { 1, 1, 1, 2, 2, 3, 3, 3, 3 }, 2);
            //Console.WriteLine($"TopKFrequent: [{string.Join(", ", topK)}]");

            solution.IsValidSudoku(new char[][]
            {
                new char[] {'1','2','.','.','3','.','.','.','.'},
                new char[] {'4','.','.','5','.','.','.','.','.'},
                new char[] {'.','9','8','.','.','.','.','.','3'},
                new char[] {'5','.','.','.','6','.','.','.','4'},
                new char[] {'.','.','.','8','.','3','.','.','5'},
                new char[] {'7','.','.','.','2','.','.','.','6'},
                new char[] {'.','.','.','.','.','.','2','.','.'},
                new char[] {'.','.','.','4','1','9','.','.','8'},
                new char[] {'.','.','.','.','8','.','.','7','9'}
            });

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

        public bool IsPalindrome(int x)
        {
            if (x < 0) return false;
            var s = x.ToString();
            for (int i = 0, j = s.Length - 1; i < j; i++, j--)
                if (s[i] != s[j]) return false;
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
        public int[] TwoSum2(int[] nums, int target) => TwoSum(nums, target);

        public List<List<string>> GroupAnagrams(string[] strs)
        {
            var dict = new Dictionary<string, List<string>>();
            foreach (var s in strs)
            {
                var key = new string(s.OrderBy(c => c).ToArray());
                if (!dict.ContainsKey(key)) dict[key] = new List<string>();
                dict[key].Add(s);
            }
            return dict.Values.ToList();
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

        public bool IsValidSudoku(char[][] board)
        {

            for (int i = 0; i < board.Length; i++)
            {

                var Row = board[i];
                var Col = GetColumn(i, board);
                if (!ValidateArray(Row) || !ValidateArray(Col) ) return false;
                for (int j = 0; j < board[i].Length; j++)
                {
                  var isSubBoxValid =  validateSubBoxs(board, i, j);
                  return isSubBoxValid;
                }
            }
            return true;
        }
        private bool ValidateArray(char[] row)
        {
            foreach (char c in row)
            {
                if (c == '.') continue;
                if (row.Count(x => x == c) > 1) return false;
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

        private bool validateSubBoxs(char[][] board, int row, int col)
        {
            for (int i = row; i < board.Length; i++)
            {
                for (int j = col; j <3; j++)
                {
                    var Row = board[i];
                    var Col = GetColumn(i, board);
                    if (!ValidateArray(Row) || !ValidateArray(Col)) return false;
                }
            }
            return true;
        }

        // helper
        public void PrintList(ListNode? node)
        {
            while (node != null) { Console.Write(node.val + (node.next != null ? " -> " : "")); node = node.next; }
            Console.WriteLine();
        }
    }
}
