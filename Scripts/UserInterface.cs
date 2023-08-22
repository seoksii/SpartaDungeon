using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Vector2
{
    private int _x;
    public int X { get; set; }
    private int _y;
    public int Y { get; set; }

    public Vector2() { X = 0; Y = 0; }
    public Vector2(int x, int y)
    {
        X = x;
        Y = y;
    }

    public static Vector2 operator +(in Vector2 p1, in Vector2 p2)
    {
        return new Vector2(p1.X + p2.X, p1.Y + p2.Y);
    }
}

abstract class UserInterface
{
    const char FrameSymbol = '#';

    private Vector2 size;
    public Vector2 Size { get; set; }


    UserInterface() { Size = new Vector2(0, 0); }
    public UserInterface(Vector2 size) { Size = size; }

    public void DrawFrame(Vector2 position)
    {
        Console.SetCursorPosition(position.X, position.Y);
        Console.Write(String.Concat(Enumerable.Repeat(FrameSymbol, Size.X)));
        for (int i = 1; i < Size.Y - 1; i++)
        {
            Console.SetCursorPosition(position.X, position.Y + i);
            Console.Write(FrameSymbol);
            Console.SetCursorPosition(position.X + Size.X - 1, position.Y + i);
            Console.Write(FrameSymbol);
        }
        Console.SetCursorPosition(position.X, position.Y + Size.Y - 1);
        Console.Write(String.Concat(Enumerable.Repeat(FrameSymbol, Size.X)));
    }

    public abstract void DrawContents(Vector2 position);
    
    public void DrawUI(Vector2 position, bool enableFrame = true)
    {
        if (enableFrame) DrawFrame(position);
        DrawContents(position);
    }
}