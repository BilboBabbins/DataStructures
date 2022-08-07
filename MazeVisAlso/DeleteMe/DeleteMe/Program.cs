using System;
using System.Collections.Generic;

public class TeaHolder<T>
{
    public T Tea;

    public TeaHolder(T tea)
    {
        Tea = tea;
    }
}

public class TeaShipment<T>
{
    public TeaHolder<T>[] Teas;


    public TeaShipment(List<T> teas)
    {
        Teas = new TeaHolder<T>[teas.Count];
        for (int i = 0; i < teas.Count; i++)
        {
            Teas[i] = new TeaHolder<T>(teas[i]);
        }
    }

    public void PrintTea()
    {
        for (int a = 0; a < Teas.Length; a++)
        {
            Console.WriteLine($"{Teas[a].Tea}");
        }
    }

}



public class C
{

    public static void Main()
    {
        List<Char> Tea = new List<char>() { 'a', 'b', 'c', 'd', 'e' };

        TeaShipment<char> teaShipment = new TeaShipment<char>(Tea);
        teaShipment.PrintTea();
    }

}
