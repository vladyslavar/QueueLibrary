using NUnit.Framework;
using QueueLibrary;
using System;
using System.Collections.Generic;
using System.Reflection;


namespace QueueTests
{
    public class Tests
    {
        //constructors
        [Test]
        public void Constructor_to_add_collection_as_paramether()
        {
            List<int> list = new List<int>() { 12, 13, 16, 20 };
            MyQueue<int> list_queue = new MyQueue<int>(list);
            Assert.AreEqual(list.Count, list_queue.Count);
        }
        [Test]
        public void Enqueue_few_values_to_zero_capacity_constructor()
        {
            MyQueue<int> few_value_mq = new MyQueue<int>(0);
            few_value_mq.Enqueue(5);
            few_value_mq.Enqueue(17);
            Assert.AreEqual(2, few_value_mq.Count);
        }

        //enqueue
        [Test]
        public void Enqueue_on_count_zero_and_bigger_capacity()
        {
            MyQueue<int> mq1 = new MyQueue<int>();
            mq1.Enqueue(15);
            Assert.AreEqual(1, mq1.Count);
        }
        [Test]
        public void Enqueue_on_count_more_than_zero_and_bigger_capacity()
        {
            MyQueue<int> mq2 = new MyQueue<int>();
            mq2.Enqueue(15);
            mq2.Enqueue(16);
            mq2.Enqueue(17);
            Assert.AreEqual(3, mq2.Count);
        }
        [Test]
        public void Enqueue_on_count_zero_and_same_capacity()
        {
            MyQueue<int> mq3 = new MyQueue<int>(0);
            mq3.Enqueue(5);
            Assert.AreEqual(1, mq3.Count);
        }
        [Test]
        public void On_enqueue_event()
        {
            MyQueue<int> mq4 = new MyQueue<int>();
            mq4.OnEnqueue += AddHandler;
            mq4.Enqueue(7);
            void AddHandler(int i)
            {
                Assert.Pass();
            }
        }

        //dequeue

        [Test]
        public void Dequeue_from_zero_queue()
        {
            MyQueue<int> mq5 = new MyQueue<int>();
            Assert.Throws<InvalidOperationException>(() => mq5.Dequeue());
        }
        [Test]
        public void Dequeue_from_one_element_queue()
        {
            MyQueue<int> mq6 = new MyQueue<int>();
            mq6.Enqueue(10);
            mq6.Dequeue();
            Assert.AreEqual(0, mq6.Count);
        }
        List<int> list2 = new List<int>() { 10, 11, 15, 30 };
        [Test]
        public void Dequeue_from_few_element_queue_correct_count()
        {
            MyQueue<int> mq7 = new MyQueue<int>(list2);
            var val = mq7.Dequeue();
            Assert.AreEqual(list2.Count-1, mq7.Count);
            Assert.AreEqual(list2[0], val);
        }
        [Test]
        public void On_dequeue_event()
        {
            MyQueue<int> mq9 = new MyQueue<int>(list2);
            mq9.OnDequeue += GetHandler;
            mq9.Dequeue();
            void GetHandler()
            {
                Assert.Pass();
            }
        }

        //Peek
        [Test]
        public void Peek_from_zero_queue()
        {
            MyQueue<int> mq10 = new MyQueue<int>();
            Assert.Throws<InvalidOperationException>(() => mq10.Peek());
        }
        [Test]
        public void Peek_from_not_zero_queue_correct_count()
        {
            MyQueue<int> mq11 = new MyQueue<int>(list2);
            int val = mq11.Peek();
            Assert.AreEqual(list2.Count, mq11.Count);
            Assert.AreEqual(list2[0], val);
        }

        //Clear
        [Test]
        public void Clear_to_empty_queue()
        {
            MyQueue<int> mq13 = new MyQueue<int>(list2);
            mq13.Clear();
            Assert.AreEqual(0, mq13.Count);
        }
        [Test]
        public void On_clear_event()
        {
            MyQueue<int> mq14 = new MyQueue<int>(list2);
            mq14.OnClear += ClearHandler;
            mq14.Clear();
            void ClearHandler()
            {
                Assert.Pass();
            }
        }
        //Contains
        [Test]
        public void Contains_on_empty_queue()
        {
            MyQueue<int> mq15 = new MyQueue<int>();
            Assert.Throws<InvalidOperationException>(() => mq15.Contains(2));
        }
        [Test]
        public void Contains_check_if_contains()
        {
            MyQueue<int> mq16 = new MyQueue<int>(list2);
            bool res = mq16.Contains(15);
            Assert.IsTrue(res);
        }
        [Test]
        public void Contains_check_if_not_contains()
        {
            MyQueue<int> mq17 = new MyQueue<int>(list2);
            bool res = mq17.Contains(20);
            Assert.IsFalse(res);
        }

        //Copy to
        [Test]
        public void Array_null_as_argument()
        {
            MyQueue<int> mq18 = new MyQueue<int>(list2);
            int[] array = null;
            Assert.Throws<ArgumentNullException>(() => mq18.CopyTo(array, 0));
        }
        [Test]
        public void Wrong_index_as_argument()
        {
            MyQueue<int> mq19 = new MyQueue<int>(list2);
            int[] array = new int[2];
            Assert.Throws<ArgumentOutOfRangeException>(() => mq19.CopyTo(array, 3));
        }
        [Test]
        public void Too_small_array_as_argument()
        {
            MyQueue<int> mq20 = new MyQueue<int>(list2);
            int[] array = new int[2];
            Assert.Throws<ArgumentException>(() => mq20.CopyTo(array, 0));
        }
        [Test]
        public void Copy_to_array()
        {
            MyQueue<int> mq21 = new MyQueue<int>(list2);
            int[] array = new int[7];
            int[] resultArray = new int[] { 0, 10, 11, 15, 30, 0, 0};
            mq21.CopyTo(array, 1);
            Assert.AreEqual(resultArray, array);
        }

        //To array
        [Test]
        public void To_array_on_empty_queue()
        {
            MyQueue<int> mq22 = new MyQueue<int>();
            Assert.Throws<InvalidOperationException>(() => mq22.ToArray());
        }
        [Test]
        public void To_array_returns_array_from_queue()
        {
            MyQueue<int> mq23 = new MyQueue<int>(list2);
            int[] resultArray = new int[] { 10, 11, 15, 30 };
            int[] array = mq23.ToArray();
            Assert.AreEqual(resultArray, array);
        }
        //TrimExcess
        [Test]
        public void Trim_Excess_shorts_queue()
        {
            List<int> longList = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
            MyQueue<int> mq24 = new MyQueue<int>(16);
            foreach (var el in longList)
                mq24.Enqueue(el);
            mq24.TrimExcess();
            int resCapacity = (int)typeof(MyQueue<int>)
                                .GetField("capacity", BindingFlags.Instance | BindingFlags.NonPublic)
                                .GetValue(mq24);
            Assert.IsTrue(mq24.Count == resCapacity);
        }

        //TryDequeue
        [Test]
        public void Try_dequeue_on_empty_queue()
        {
            MyQueue<int> mq25 = new MyQueue<int>();
            int valueres;
            bool res = mq25.TryDequeue(out valueres);
            Assert.IsFalse(res);
        }
        [Test]
        public void Try_dequeue_on_full_queue()
        {
            MyQueue<int> mq26 = new MyQueue<int>(list2);
            int valueres;
            bool res = mq26.TryDequeue(out valueres);
            Assert.IsTrue(res);
        }
        //TryPeek
        [Test]
        public void Try_peek_on_empty_queue()
        {
            MyQueue<int> mq27 = new MyQueue<int>();
            int valueres;
            bool res = mq27.TryPeek(out valueres);
            Assert.IsFalse(res);
        }
        [Test]
        public void Try_peek_on_full_queue()
        {
            MyQueue<int> mq28 = new MyQueue<int>(list2);
            int valueres;
            bool res = mq28.TryPeek(out valueres);
            Assert.IsTrue(res);
        }
        //IsSynchronized
        [Test]
        public void Is_synchronized_has_always_return_falls()
        {
            MyQueue<int> mq = new MyQueue<int>();
            bool res = mq.IsSynchronized;
            Assert.IsFalse(res);
        }
        //SyncRoot
        [Test]
        public void SyncRoot_has_always_return_current_object()
        {
            MyQueue<int> mq = new MyQueue<int>();
            var res = mq.SyncRoot;
            Assert.AreSame(res, mq);
        }
        //CopyTo(Array)
        [Test]
        public void Array_null_as_argument_Array()
        {
            MyQueue<int> mq = new MyQueue<int>(list2);
            Array array = null;
            Assert.Throws<ArgumentNullException>(() => mq.CopyTo(array, 0));
        }
        [Test]
        public void Wrong_index_as_argument_Array()
        {
            MyQueue<int> mq = new MyQueue<int>(list2);
            Array array = new int[2];
            Assert.Throws<ArgumentOutOfRangeException>(() => mq.CopyTo(array, 3));
        }
        [Test]
        public void Too_small_array_as_argument_Array()
        {
            MyQueue<int> mq = new MyQueue<int>(list2);
            Array array = new int[2];
            Assert.Throws<ArgumentException>(() => mq.CopyTo(array, 0));
        }
        [Test]
        public void Copy_to_array_Array()
        {
            MyQueue<int> mq = new MyQueue<int>(list2);
            Array array = new int[7];
            Array resultArray = new int[] { 10, 11, 15, 30, 0, 0, 0 };
            mq.CopyTo(array, 0);
            Assert.AreEqual(resultArray, array);
        }
        
    }
}