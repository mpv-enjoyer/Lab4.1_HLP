using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public class ArrayElement
{
    int CurrentElement = 0;
    ArrayElement? NextElement = null;
    public ArrayElement(int element = 0)
    {
        CurrentElement = element;
    }
    public int Get(int index)
    {
        if (index == 0) return CurrentElement;
        if (NextElement == null) throw new Exception("'get' out of bounds");
        return NextElement.Get(index - 1);
    }
    public void Set(int index, int value)
    {
        if (index == 0)
        {
            CurrentElement = value;
            return;
        }
        if (NextElement == null) throw new Exception("'set' out of bounds");
        NextElement.Set(index - 1, value);
    }
    private bool MoveBack()
    {
        if (NextElement == null) return true;
        CurrentElement = NextElement.Get(0);
        if (NextElement.MoveBack())
        {
            NextElement = null;
        }
        return false;
    }
    public void RemoveAt(int index)
    {
        if (index != 0)
        {
            if (NextElement == null) throw new Exception("'removeat' out of bounds");
            NextElement.RemoveAt(index - 1);
            return;
        }
        if (MoveBack()) NextElement = null;
    }
    public void Generate(int size)
    {
        if (size <= 1) return;
        NextElement = new ArrayElement();
        NextElement.Generate(size - 1);
    }
    public int Size()
    {
        if (NextElement == null) return 1;
        return NextElement.Size() + 1;
    }
    public void Expand(int value)
    {
        if (NextElement == null)
        {
            NextElement = new ArrayElement(value);
            return;
        }
        NextElement.Expand(value);
    }
}
public class CustomIntArray : ArrayElement
{
    public CustomIntArray(int size)
    {
        if (size <= 0) throw new Exception("'size' out of bounds");
        Generate(size);
    }
    public void Swap(int index1, int index2)
    {
        int temp = Get(index1);
        Set(index1, Get(index2));
        Set(index2, temp);
    }
    public void InputData(in string input, in char separator = ' ')
    {
        int current_fill_el = 0;
        int prev = 0;
        bool in_num = false;
        int neg = 1;
        for (int i = 0; i < input.Count(); i++)
        {
            if (input[i] == '-' && !in_num)
            {
                neg = -neg;
                continue;
            }
            if (input[i] == separator)
            {
                if (current_fill_el >= Size()) return;
                Set(current_fill_el, prev * neg);
                current_fill_el += 1;
                prev = 0;
                neg = 1;
                in_num = false;
                continue;
            }
            if (char.IsDigit(input[i]))
            {
                in_num = true;
                prev = prev * 10 + (input[i] - '0');
            }
        }
        if (current_fill_el < Size() && in_num) Set(current_fill_el, prev * neg);
    }
    public void InputDataRandom()
    {
        Random random = new();
        for (int i = 0; i < Size(); i++)
        {
            Set(i, random.Next(100));
        }
    }
    public void Print()
    {
        Console.Write("[ ");
        for (int i = 0; i < Size(); i++)
        {
            Console.Write($"{Get(i)} ");
        }
        Console.WriteLine("]");
    }
    public bool FindValue(int value, out CustomIntArray indexes)
    {
        int indexesSize = 0;
        for (int i = 0; i < Size(); i++)
        {
            if (value == Get(i))
            {
                indexesSize += 1;
            }
        }
        if (indexesSize == 0)
        {
            indexes = new CustomIntArray(1);
            return false;
        }
        indexes = new CustomIntArray(indexesSize);
        int current_index = 0;
        for (int i = 0; i < Size(); i++)
        {
            if (value == Get(i))
            {
                indexes.Set(current_index, i);
                current_index += 1;
            }
        }
        return true;
    }
    public bool DelValue(int value)
    {
        CustomIntArray valueIndexes;
        if (!FindValue(value, out valueIndexes)) return false; //no corresponding element found
        if (valueIndexes.Size == Size) return false; //cannot make size = 0
        for (int i = valueIndexes.Size() - 1; i >= 0; i--)
        {
            RemoveAt(valueIndexes.Get(i));
        }
        return true;
    }
    public int FindMax(ref int index)
    {
        int CurrentMax = int.MinValue;
        for (int i = 0; i < Size(); i++)
        {
            if (CurrentMax < Get(i))
            {
                CurrentMax = Get(i);
                index = i;
            }
        }
        return CurrentMax;
    }
    public bool Add(CustomIntArray rhs)
    {
        if (rhs.Size() != Size()) return false;
        for (int i = 0; i < Size(); i++)
        {
            Set(i, Get(i) + rhs.Get(i));
        }
        return true;
    }
    public void Sort()
    {
        for (int i = 0; i < Size(); i++)
        {
            int smallestIndex = i;
            for (int j = i + 1; j < Size(); j++)
            {
                if (Get(smallestIndex) > Get(j)) smallestIndex = j;
            }
            Swap(i, smallestIndex);
        }
    }
    public void Insert(int index, int value)
    {
        if (index > Size()) throw new Exception("'insert' out of bounds");
        Expand(Get(Size() - 1));
        for (int i = Size() - 1; i > index; i--)
        {
            Set(i, Get(i - 1));
        }
        Set(index, value);
    }
}