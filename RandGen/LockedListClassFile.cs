﻿
using System.Collections.Generic;

namespace RandGen
{
    public class LockListClass<T>
    {
        private List<T> tList;
        private object tLock = new object();
        public LockListClass(List<T> inList)
        {
            tList = new List<T>(inList);
        }
        public LockListClass()
        {
            tList = new List<T>();
        }
        public void Add(T item)
        {
            lock (tLock)
            {
                tList.Add(item);
            }
        }
        public void AddRange(List<T> items)
        {
            lock (tLock)
            {
                tList.AddRange(items);
            }
        }
        public int Count()
        {
            lock (tLock)
            {
                return tList.Count;
            }
        }
        public T GetItem(int index)
        {
            lock (tLock)
            {
                return tList[index];
            }
        }
        public void RemoveAt(int index)
        {
            lock (tLock)
            {
                tList.RemoveAt(index);
            }
        }
        public T ExtractOne(int index)
        {
            lock (tLock)
            {
                var item = tList[index];
                tList.RemoveAt(index);
                return item;
            }
        }
        public List<T> Copy()
        {
            lock (tLock)
            {
                return new List<T>(tList);
            }
        }
        public void Clear()
        {
            lock (tLock)
            {
                tList.Clear();
            }
        }
        public List<T> SubSet(int startIndex, int count)
        {
            lock (tLock)
            {
                return new List<T>(tList.GetRange(startIndex, count));
            }
        }
    }
}
