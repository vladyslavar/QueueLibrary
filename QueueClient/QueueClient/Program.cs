using System;
using QueueLibrary;

namespace QueueClient
{
    class Program
    {
        static void Main(string[] args)
        {
            MyQueue<int> que = new MyQueue<int>();
            
            que.OnEnqueue += AddHandler;
            que.OnDequeue += RemoveHandler;
            que.OnClear += ClearHandler;

            que.Enqueue(1);
            que.Enqueue(2);
            que.Enqueue(3);
            que.Enqueue(4);
            que.Enqueue(5);
            que.Enqueue(6);
            que.Enqueue(7);
            ShowQueue(que);

            Console.WriteLine();
            que.Dequeue();
            ShowQueue(que);
            Console.WriteLine();
            que.Dequeue();
            ShowQueue(que);

            Console.WriteLine();
            que.Clear();
            ShowQueue(que);

            que.Enqueue(11);
            que.Enqueue(22);
            que.Enqueue(33);
            que.Enqueue(44);
            Console.WriteLine();
            ShowQueue(que);

            int[] arr = new int[6];
            arr[0] = 9; arr[1] = 8; arr[2] = 7; arr[3] = 6; arr[4] = 5; arr[5] = 4;
            que.CopyTo(arr, 2);

            Console.WriteLine();
            for(int i = 0; i < arr.Length; i++)
            {
                Console.WriteLine(arr[i]);
            }

            int[] taArrayMethod = que.ToArray();
            Console.WriteLine();
            Console.WriteLine("To Array:");
            for (int i = 0; i < taArrayMethod.Length; i++)
            {
                Console.WriteLine(taArrayMethod[i]);
            }

            bool ifconttrue = que.Contains(22);
            bool ifcontfalse = que.Contains(66);

            Console.WriteLine("true: " + ifconttrue + " ||| false: " + ifcontfalse);

            Console.WriteLine("Enumerator: ");
            foreach(var item in que)
            {
                Console.WriteLine(item);
            }


            void AddHandler(int item)
            {
                Console.WriteLine(item + " added");
            }
            void RemoveHandler()
            {
                Console.WriteLine("item dequeued");
            }
            void ClearHandler()
            {
                Console.WriteLine("Queue is cleared");
            }
        }

        static public void ShowQueue(MyQueue<int> q)
        {
            if (q.Count != 0)
            {
                foreach(var val in q)
                {
                    Console.Write(val + "\t");
                }
            }
            else
            {
                Console.WriteLine("Queue is empty");
            }
        }
    }
}
