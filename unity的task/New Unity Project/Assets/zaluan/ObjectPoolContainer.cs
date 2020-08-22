using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolContainer<T> 
{
    //pool的用途：为了避免频繁的创建和销毁对象
    //原理实现：需要使用对象时候选择一个未被使用的对象使用；在用完后需要销毁的时候将其
    //标记为未使用而不是销毁，以供下次再使用
    // Start is called before the first frame update
    //1.先建立一个类 建立一个容器，写明容器存放的内容 以及标记是否被使用
    //容器内的对象
    public T Item { get; set; }
    //标记容器内对象是否被使用
    public bool Used { get; set; }
    
    public void Consume()//标记已被使用
    {
        Used = true;
    }
    public void Release()//标记未被使用
    {
        Used = false;
    }

    //实现对象池 需要先初始化容量大小
    
   
}
