using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class CustomIntStack
{
    CustomIntArray? Array = null;
    public CustomIntStack()
    {

    }
    public int Size()
    {
        return Array == null ? 0 : Array.Size();
    }
    public void Push(int value)
    {
        if (Size() == 0)
        {
            Array = new(1);
            Array.Set(0, value);
            return;
        }
        Array.Insert(0, value);
    }
    public int Pop()
    {
        if (Size() == 0) throw new Exception("Pull empty");
        int value = Array.Get(0);
        if (Size() == 1)
        {
            Array = null;
            return value;
        }
        Array.RemoveAt(0);
        return value;
    }
    public int Back()
    {
        if (Size() == 0) throw new Exception("Back empty");
        return Array.Get(Size() - 1);
    }
    public void Clear()
    {
        Array = null;
    }
}