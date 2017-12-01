using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTCI_2._1_Remove_Duplicates
{
    class Program
    {
        static void Main(string[] args)
        {
            PrintHeaderMsg(2, 1, "Remove Duplicates");

            Node head = CreateSinglyLinkedList(100);

            RemoveDuplicates_Buffer(head);

            RemoveDuplicates_NoBuffer(head);

            Console.ReadLine();
        }

        private static void RemoveDuplicates_Buffer(Node head)
        {
            throw new NotImplementedException();
        }

        private static void RemoveDuplicates_NoBuffer(Node head)
        {
            throw new NotImplementedException();
        }

        private static Node CreateSinglyLinkedList(int count)
        {
            if (count < 1)
                return null;

            Random rnd = new Random();

            Node head = new Node(rnd.Next(10, 100));

            Node n = head;

            for (int i = 0; i < count - 1; ++i)
            {
                n.next = new Node(rnd.Next(10, 100));
                n = n.next;
            }

            return head;
        }

        private static void PrintHeaderMsg(int chapter, int problem, string title)
        {
            Console.WriteLine("Cracking the Coding Interview");
            Console.WriteLine("Chapter " + chapter + ", Problem " + chapter + "." + problem + ": " + title);
            Console.WriteLine();
        }
    }

    class Node
    {
        public Node next = null;

        int Data;

        public Node(int d) => Data = d;        

        public void ApppendToTail(int d)
        {
            Node n = this;

            while (n.next != null)
            {
                n = n.next;
            }

            n.next = new Node(d);
        }


        //////////////////////////////////////////////////////////////
        //        
        // Scans the linked list for the occurance of a value.
        // All nodes found with that value are removed from the list.
        // 
        // If header node is removed, updates header to next node.                
        // 
        // Complexity: Algorithm runs in O(N) time
        //             Every element must be touched once to check for a match.
        //
        //             Algorithm requires O(1) space
        //             Creates 1 Node object no matter what the input is.
        //
        public void DeleteNode(ref Node head, int d)
        {
            Node n = head;

            if (n.Data == d)
            {
                head = n.next.next; // drops the head       
            }

            while (n.next != null)
            {
                if (n.next.Data == d)
                {
                    n.next = n.next.next; // removes next node from list                    
                }
            }

            return;
        }
    }
}
