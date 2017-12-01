using System;
using System.Collections.Generic;
using System.Diagnostics;
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

            Node head = CreateSinglyLinkedList(1000000);
            long ticks = RemoveDuplicates_NoBuffer(head);
            Console.WriteLine("De-Duplication without buffer (" + ticks + " ticks)");
            //PrintNodes(head);
            Console.WriteLine();

            head = CreateSinglyLinkedList(1000000);
            ticks = RemoveDuplicates_Buffer(head);
            Console.WriteLine("De-Duplication with buffer (" + ticks + " ticks)");
            //PrintNodes(head);
            Console.WriteLine();

            head = CreateSinglyLinkedList(1000000);
            ticks = RemoveDuplicates_Recreate(head);
            Console.WriteLine("De-Duplication with hashset and re-creation (" + ticks + " ticks)");
            //PrintNodes(head);
            Console.WriteLine();

            Console.ReadLine();
        }
        
        //////////////////////////////////////////////////////////////
        //        
        // For each node in the list, check all subsequent nodes for
        // duplicate values.  If duplicate is found, remove it from 
        // the list.        
        // 
        // Complexity: Algorithm runs in O(N^2) time
        //             For every element, every subsequent element must be checked.
        //             This is technically less than N^2, but constants are dropped.
        //
        //             Algorithm requires O(1) space
        //             Memory requirements are constant regardless of input.
        //
        private static long RemoveDuplicates_NoBuffer(Node passed_n)
        {            
            Stopwatch sw = Stopwatch.StartNew();

            if (passed_n == null)
                return 0;

            Node current_n = passed_n;
            Node runner_n = passed_n;

            while (current_n != null)
            {
                runner_n = current_n;

                // check every subsequent node value for duplicate of current node value
                while (runner_n.next != null)
                {
                    if (runner_n.next.Data == current_n.Data)
                    {
                        runner_n.next = runner_n.next.next; // remove node from list
                    }
                    else
                        runner_n = runner_n.next;
                }

                // increment to the next node and check again
                current_n = current_n.next;
            }

            sw.Stop();

            return sw.ElapsedTicks;
        }


        //////////////////////////////////////////////////////////////
        //        
        // For each node in the list, check if its value exists in
        // the hashset.  If exists, remove that node from the list.
        // if not, add the value to the hashset.
        //
        // This algo prevents the O(N^2) time requirement of the above.
        // 
        // Complexity: Algorithm runs in O(N) time
        //             Every element is checked once.
        //
        //             Algorithm requires O(N) space
        //             Every node value (int) is copied into a hashset.        
        //
        private static long RemoveDuplicates_Buffer(Node passed_n)
        {
            Stopwatch sw = Stopwatch.StartNew();

            if (passed_n == null)
                return 0;

            HashSet<int> values = new HashSet<int>();

            values.Add(passed_n.Data);

            // (note that passed_n will never be a duplicate because it's first)

            while (passed_n.next != null)
            {
                if (values.Contains(passed_n.next.Data))
                {
                    passed_n.next = passed_n.next.next; // remove duplicate from list
                }
                else
                {
                    values.Add(passed_n.next.Data); // HashSet<> handles duplicates for us         
                    passed_n = passed_n.next;
                }                
            }

            sw.Stop();

            return sw.ElapsedTicks;
        }

        //////////////////////////////////////////////////////////////
        //        
        // For each node in the list, add the value to a hashset.
        //
        // For each item after the head, create a node and link it
        // to a new list.
        //
        // The hashset cannot contain duplicates, and is extremely 
        // fast.  This algo avoids checking values and removing nodes,
        // which are much slower operations than adding values to 
        // a hashset.        
        // 
        // Complexity: Algorithm runs in O(N) time
        //             Every element is checked once. For non-head,
        //             non-duplicate values, every value is created
        //             as a new node.
        //
        //             Algorithm requires O(N) space
        //             Every non-duplicate node value (int) is copied 
        //             into a hashset.        
        //             Every non-duplicate value is created as a new 
        //             node.
        //             Worst-cast: O(2N), which is O(N) (drop the 
        //             constant.
        //
        private static long RemoveDuplicates_Recreate(Node passed_n)
        {
            Stopwatch sw = Stopwatch.StartNew();

            if (passed_n == null)
                return 0;

            HashSet<int> values = new HashSet<int>();

            values.Add(passed_n.Data);

            Node runner = passed_n;

            while (runner.next != null)
            {
                values.Add(runner.next.Data);

                runner = runner.next;                
            }

            values.Remove(passed_n.Data);

            Node last = passed_n;
            foreach(int i in values)
            {
                Node n = new Node(i);
                last.next = n;
                last = n;
            }

            sw.Stop();
            return sw.ElapsedTicks;
        }

        private static Node CreateSinglyLinkedList(int count)
        {
            if (count < 1)
                return null;

            Random rnd = new Random();

            Node head = new Node(rnd.Next(0, 1000));

            Node n = head;

            for (int i = 0; i < count - 1; ++i)
            {
                n.next = new Node(rnd.Next(0, 1000));
                n = n.next;
            }

            return head;
        }

        private static void PrintNodes(Node passed_n)
        {
            while (passed_n != null)
            {
                Console.Write(passed_n.Data + ", ");

                passed_n = passed_n.next;
            }
            Console.WriteLine();
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

        public int Data;

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
