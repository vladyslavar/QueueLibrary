using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace QueueLibrary
{
    class MyQueueNode<T>
    {
        public T value;
        public MyQueueNode<T> next;
        public MyQueueNode()
        {
            value = default(T);
            next = null;
        }
        public MyQueueNode(T t)
        {
            value = t;
            next = null;
        }
    }
    public class MyQueue<T> : IEnumerable<T>, IEnumerable, IReadOnlyCollection<T>, ICollection
    {
        //T[] Data;
        int capacity;
        int NumOfItems;
        MyQueueNode<T> HeadPoint;
        MyQueueNode<T> TailPoint;

        public delegate void EnqueueHandler(T item);
        public event EnqueueHandler OnEnqueue;

        public delegate void DequeueHandler();
        public event DequeueHandler OnDequeue;

        public delegate void ClearHandler();
        public event ClearHandler OnClear;

        public MyQueue()
        {
            HeadPoint = new MyQueueNode<T>();
            TailPoint = HeadPoint;
            capacity++;
            for(int i = 1; i < 5; i++)
            {
                TailPoint.next = new MyQueueNode<T>();
                TailPoint = TailPoint.next;
                capacity++;
            }
            NumOfItems = 0;
        }
        public MyQueue(IEnumerable<T> inputCollection)
        {
            T[] Data = inputCollection.ToArray();
            HeadPoint = new MyQueueNode<T>(Data[0]);
            TailPoint = HeadPoint;
            capacity++;
            NumOfItems++;
            for (int i = 1; i < Data.Length; i++)
            {
                TailPoint.next = new MyQueueNode<T>(Data[i]);
                TailPoint = TailPoint.next;
                capacity++;
                NumOfItems++;
            }
        }
        public MyQueue(int _capacity)
        {
            HeadPoint = new MyQueueNode<T>();
            TailPoint = HeadPoint;
            //capacity++;
            for (int i = 1; i < _capacity; i++)
            {
                TailPoint.next = new MyQueueNode<T>();
                TailPoint = TailPoint.next;
                capacity++;
            }
            NumOfItems = 0;
        }


        public int Count 
        { 
            get { return NumOfItems;}
        }


        public void Enqueue(T item)
        {
            MyQueueNode<T> CurrentPoint;
            if (Count < capacity)
            {
                if(Count == 0)
                {
                    HeadPoint.value = item;
                }
                else
                {
                    CurrentPoint = HeadPoint;
                    for(int i = 0; i < Count; i++) CurrentPoint = CurrentPoint.next;

                    CurrentPoint.value = item;
                }
                NumOfItems++;
            }
            if(Count == capacity)
            {
                if (Count == 0)
                {
                    HeadPoint.value = item;
                    TailPoint = HeadPoint;
                }
                else
                {
                    TailPoint.next = new MyQueueNode<T>(item);
                    TailPoint = TailPoint.next;
                }
                capacity++;
                NumOfItems++;
            }

            if(OnEnqueue != null)
            {
                OnEnqueue.Invoke(item);
            }
        }
        public T Dequeue()
        {
            if(Count == 0)
            {
                throw new InvalidOperationException();
            }
            else
            {
                T toret = HeadPoint.value;
                if (HeadPoint==TailPoint)
                {
                    HeadPoint = new MyQueueNode<T>();
                }
                else
                {
                    HeadPoint = HeadPoint.next;
                }
                NumOfItems--;
                capacity--;

                if(OnDequeue != null)
                {
                    OnDequeue.Invoke();
                }
                return toret;
            }
        }
        public T Peek()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException();
            }
            return HeadPoint.value;
        }

        public void Clear()
        {
            NumOfItems = 0;
            capacity = 0;
            HeadPoint = new MyQueueNode<T>();
            TailPoint = HeadPoint;
            if(OnClear != null)
            {
                OnClear.Invoke();
            }
        }
        public bool Contains(T item)
        {
            if (NumOfItems != 0)
            {
                MyQueueNode<T> CurrentPoint;
                CurrentPoint = HeadPoint;
                for (int i = 0; i < Count; i++)
                {
                    if (CurrentPoint.value.Equals(item)) return true;
                    else CurrentPoint = CurrentPoint.next;
                }
                return false;
            }
            else
            {
                throw new InvalidOperationException();
            }
        }
        public void CopyTo(T[] array, int arrayIndex)
        {
            //check if array isn't null
            if (array == null) throw new ArgumentNullException();
            //check if index not < -1 or more than array lenght
            if (arrayIndex < 0 || arrayIndex >= array.Length) throw new ArgumentOutOfRangeException();
            //check if enought space
            if (array.Length - arrayIndex < Count) throw new ArgumentException();

            MyQueueNode<T> CurrentPoint = HeadPoint;
            for (int i = 0; i < Count; i++)
            {
                array[arrayIndex + i] = CurrentPoint.value;
                CurrentPoint = CurrentPoint.next;
            }
        }
        
        public T[] ToArray()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException();
            }
            T[] arrToReturn = new T[Count];
            MyQueueNode<T> CurrentPoint = HeadPoint;
            for (int i = 0; i < Count; i++ )
            {
                arrToReturn[i] = CurrentPoint.value;
                CurrentPoint = CurrentPoint.next;
            }
            return arrToReturn;
        }
        public void TrimExcess()
        {
            if(Count/capacity > 0.9)
            {
                MyQueueNode<T> CurrentPoint = HeadPoint;
                for (int i = 0; i < Count; i++)
                    CurrentPoint = CurrentPoint.next;

                TailPoint = CurrentPoint;
                capacity = Count;
            }
        }
        public bool TryDequeue([MaybeNullWhen(false)] out T result)
        {
            if (Count == 0)
            {
                result = default(T);
                return false;
            }
            else
            {
                result = HeadPoint.value;
                if (HeadPoint == TailPoint)
                {
                    HeadPoint = new MyQueueNode<T>();
                }
                else
                {
                    HeadPoint = HeadPoint.next;
                }
                NumOfItems--;
                capacity--;
                return true;
            }
        }
        public bool TryPeek([MaybeNullWhen(false)] out T result)
        {
            if(Count == 0)
            {
                result = default(T);
                return false;
            }
            else
            {
                result = HeadPoint.value;
                return true;
            }
        }
        public bool IsSynchronized { get { return false; } }
        public object SyncRoot { get { return this; } }
        public void CopyTo(Array array, int index)
        {
            //check if array isn't null
            if (array == null) throw new ArgumentNullException();
            //check if index not < -1 or not more than array lenght
            if (index < 0 || index >= array.Length) throw new ArgumentOutOfRangeException();
            //check if enought space
            if (array.Length - index < Count) throw new ArgumentException();

            MyQueueNode<T> CurrentPoint = HeadPoint;
            for (int i = 0; i < Count; i++)
            {
                array.SetValue(CurrentPoint.value, index+i);
                CurrentPoint = CurrentPoint.next;
            }
        }
        public IEnumerator<T> GetEnumerator()
        {
            MyQueueNode<T> CurrentPoint = HeadPoint;
            for (int i = 0; i < Count; i++)
            {
                yield return CurrentPoint.value;
                CurrentPoint = CurrentPoint.next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        
    }
}
