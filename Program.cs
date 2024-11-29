using System;
using System.Collections.Generic;

public class SimpleUnionFind
{
    public int[] parent;

    public SimpleUnionFind(int size)
    {
        parent = new int[size];
        for (int i = 0; i < size; i++)
        {
            parent[i] = i;  // Инициализируем каждый элемент как родителя самого себя
        }
    }

    // Операция поиска (find)
    public int Find(int x)
    {
        if (parent[x] != x)
        {
            return Find(parent[x]); // Рекурсивно ищем корень
        }
        return x;
    }

    // Операция объединения (union)
    public void Union(int x, int y)
    {
        int rootX = Find(x);
        int rootY = Find(y);

        if (rootX != rootY)
        {
            parent[rootY] = rootX;  // Объединяем два множества
        }
    }
}

public class UnionFindWithPathCompression
{
    private int[] parent;

    public UnionFindWithPathCompression(int size)
    {
        parent = new int[size];
        for (int i = 0; i < size; i++)
        {
            parent[i] = i;  // Каждый элемент является своим родителем
        }
    }

    // Операция поиска с сжатием пути
    public int Find(int x)
    {
        if (parent[x] != x)
        {
            parent[x] = Find(parent[x]); // Сжимаем путь
        }
        return parent[x];
    }

    // Операция объединения
    public void Union(int x, int y)
    {
        int rootX = Find(x);
        int rootY = Find(y);

        if (rootX != rootY)
        {
            parent[rootY] = rootX;  // Объединяем два множества
        }
    }
}

public class UnionFindWithRank
{
    private int[] parent;
    private int[] rank;  // Массив для хранения рангов деревьев

    public UnionFindWithRank(int size)
    {
        parent = new int[size];
        rank = new int[size];
        for (int i = 0; i < size; i++)
        {
            parent[i] = i;  // Каждый элемент является своим родителем
            rank[i] = 0;  // Ранг каждого дерева равен 0
        }
    }

    // Операция поиска с сжатием пути
    public int Find(int x)
    {
        if (parent[x] != x)
        {
            parent[x] = Find(parent[x]);  // Сжимаем путь
        }
        return parent[x];
    }

    // Операция объединения с использованием рангов
    public void Union(int x, int y)
    {
        int rootX = Find(x);
        int rootY = Find(y);

        if (rootX != rootY)
        {
            // Применяем ранговую эвристику
            if (rank[rootX] > rank[rootY])
            {
                parent[rootY] = rootX;  // Присоединяем дерево с меньшим рангом
            }
            else if (rank[rootX] < rank[rootY])
            {
                parent[rootX] = rootY;
            }
            else
            {
                parent[rootY] = rootX;  // Если ранги равны, то можно выбрать произвольно
                rank[rootX]++;  // Увеличиваем ранг
            }
        }
    }
}

// Пример использования:
class Program
{
    static void Main()
    {
        SimpleUnionFind uf = new SimpleUnionFind(10);
        //UnionFindWithPathCompression uf = new UnionFindWithPathCompression(10);
        //UnionFindWithRank uf = new UnionFindWithRank(10);
        uf.Union(1, 2);
        Console.WriteLine("After UNION(1,2)-----------------------------");
        foreach (var item in uf.parent)
        {  
           Console.WriteLine(item.ToString()); 
        }
        Console.WriteLine("-----------------------------------");
        uf.Union(2, 3);
        Console.WriteLine("After UNION(2,3)-----------------------------");
        foreach (var item in uf.parent)
        {
            Console.WriteLine(item.ToString());
        }
        Console.WriteLine("-----------------------------------");
        uf.Union(4, 5);

        Console.WriteLine(uf.Find(3));
        Console.WriteLine(uf.Find(5));
    }
}
