using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Authentication.ExtendedProtection;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// [Author : Seoksii]
/// 인스턴스마다 부여된 고유한 코드로 해당 인스턴스를 찾을 수 있는 자료구조 입니다.
/// </summary>
public class CodeInstanceContainer<T>
{
    private int _capacity = 1000;
    /// <summary>
    /// ItemData를 담고 있는 배열의 크기입니다.
    /// capacity보다 큰 개수의 데이터가 들어올 경우 자동으로 늘어납니다.
    /// </summary>
    public int Capacity
    {
        get { return _capacity; }
        private set { _capacity = value; }
    }

    private T[] _data = new T[1000];
    public T this[int i]
    {
        get
        {
            if (i < 0) throw new InvalidOperationException();
            return _data[i];
        }
        set
        {
            while (i >= _data.Length) { increaseCapacity(Capacity << 1); }
            _data[i] = value;
        }
    }

    /// <summary>
    /// 현재 할당 가능한 아이템 코드 중 가장 낮은 값을 반환하는 스택입니다.
    /// </summary>
    private Stack<int> AvailableCodes = new Stack<int>();

    public CodeInstanceContainer()
    {
        for (int i = 1000 - 1; i >= 0; i--)
            AvailableCodes.Push(i);
    }

    /// <summary>
    /// CodeInstanceContainer에 인스턴스를 추가하고 코드를 돌려받습니다.
    /// </summary>
    public int Add(in T instance)
    {
        int code;

        if (AvailableCodes.Count <= 0)
        {
            code = Capacity;
            this[code] = instance;
            return code;
        }
        // AvailableCodes.Count > 0
        code = AvailableCodes.Pop();
        this[code] = instance;

        return code;
    }

    public void Remove(int code)
    {
        if (code < 0) throw new InvalidOperationException();
        if (code >= Capacity) throw new InvalidOperationException();

        if (AvailableCodes.Count <= 0) AvailableCodes.Push(code);
        else
        {
            Stack<int> tmpStack = new Stack<int>();
            while (AvailableCodes.Count > 0 && AvailableCodes.Peek() > code)
                tmpStack.Push(AvailableCodes.Pop());
            AvailableCodes.Push(code);
            while (tmpStack.Count > 0)
                AvailableCodes.Push(tmpStack.Pop());
        }
    }

    public void increaseCapacity(int capacity)
    {
        if (capacity < Capacity) throw new InvalidOperationException();

        Array.Resize<T>(ref _data, (int)capacity);

        Stack<int> tmpStack = new Stack<int>();
        while (AvailableCodes.Count > 0)
            tmpStack.Push(AvailableCodes.Pop());
       
        for (int i = capacity - 1; i >= Capacity; --i)
            AvailableCodes.Push(i);
        
        while (tmpStack.Count > 0)
            AvailableCodes.Push(tmpStack.Pop());

        Capacity = capacity;
    }
}