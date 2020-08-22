using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

/// <summary>
/// 一般对象的池子
/// </summary>
public class ObjectPool<T>
{
    //池子中的所有对象
    private List<ObjectPoolContainer<T>> list;
    //已被使用的对象（用于查询）
    private Dictionary<T, ObjectPoolContainer<T>> lookup;
    private Func<T> factoryFunc;
    private int lastIndex;

    public int Count
    {
        get
        {
            return list.Count;
        }
    }

    public int CountUsedItems
    {
        get
        {
            return lookup.Count;
        }
    }

    public ObjectPool(Func<T> factoryFunc, int initialSize)
    {
        //该委托用于返回一个新创建的对象实例
        this.factoryFunc = factoryFunc;
        list = new List<ObjectPoolContainer<T>>(initialSize);
        lookup = new Dictionary<T, ObjectPoolContainer<T>>(initialSize);

        Warm(initialSize);
    }

    //初始化时候即生成capacity个对象
    private void Warm(int capacity)
    {
        for(int i=0; i<capacity; i++)
        {
            CreateContainer();
        }
    }
    private ObjectPoolContainer<T> CreateContainer()
    {
        var container = new ObjectPoolContainer<T>();
        container.Item = factoryFunc();
        list.Add(container);
        return container;
    }

    public T GetItem()
    {
        ObjectPoolContainer<T> container = null;

        // 若在list中找得到还未被使用的对象，则使用该对象作为返回值
        for (int i = 0; i < list.Count; i++)
        {
            lastIndex++;
            // 最可能没使用的就是上次使用了的下一个。若上次使用的是最后1个，则这次只要从第0个开始查
            if (lastIndex > list.Count - 1)
                lastIndex = 0;

            if (list[lastIndex].Used)
                continue;
            else
            {
                container = list[lastIndex];
                break;
            }
        }

        // list中所有对象都被使用了，新创建一个对象
        if (container == null)
        {
            container = CreateContainer();

        }

        // 标记为已使用
        container.Consume();
        lookup.Add(container.Item, container);

        return container.Item;
    }

    public void ReleaseItem(object item)
    {
        ReleaseItem((T)item);
    }

    public void ReleaseItem(T item)
    {
        if (lookup.ContainsKey(item))
        {
            // 标记为未使用
            var container = lookup[item];
            container.Release();
            lookup.Remove(item);
        }
    }

    //private ObjectPool<T> myObjPool = new ObjectPool<T>(() => new T(), 10);

    //var T = myObjPool.GetItem();

    //myObjPool.ReleaseItem(myObj); 
}
 

   

