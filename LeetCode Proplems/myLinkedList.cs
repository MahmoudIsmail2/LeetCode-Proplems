using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode_Proplems
{
    public class myLinkedList
    {
        public Node head;
        public Node tail;
        public int length;
       
       public void InsertsatFirst (int value)
       {
            if (length==0 || head==null)
            {
                Node newNode = new Node(value);
                head = newNode;
                tail = newNode; 
                length = 1;
            }
            else
            {
                Node newNode=new Node(value);
                newNode.next = head;
                head = newNode;
                length++;
            }
       }
        public void InsertatLast(int value)
        {
            if (length == 0 || head == null)
            {
                Node newNode = new Node(value);
                head = newNode;
                tail = newNode;
                length = 1;
            }
            else{
                Node newNode = new Node(value);
                tail.next = newNode;
                tail = newNode;
                length++;
            }
        }

        public void InsertatIndex(int index,int value)
        {
            if(index==0)
            {
                InsertsatFirst(value);
            }
            else if(index==length)
            {
                InsertatLast(value);
            }
            else
            {
                Node newNode = new Node(value);
                Node currentNode = head;
                for (int i = 0; i < index - 1; i++)
                {
                    currentNode = currentNode.next;
                }
                newNode.next = currentNode.next;
                currentNode.next = newNode;
                length++;
            }
        }

        public void DeleteFirst()
        {
            if (length == 1)
            {
                head = null;
                tail = null;
                length = 0;
            }
            else
            {
                head = head.next;
                length--;
            }
        }
        public void PrintList()
        {
            Node currentNode = head;
            while (currentNode != null)
            {
                Console.WriteLine(currentNode.value);
                currentNode = currentNode.next;
            }
        }
    }
}
