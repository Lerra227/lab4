using System.Numerics;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CarComparer;

Console.WriteLine("Введите нижнюю границу диапазона значений матрицы: ");
        int low  = Convert.ToInt32(Console.ReadLine());
Console.WriteLine("Введите верхнюю границу диапазона значений матрицы: ");
        int top = Convert.ToInt32(Console.ReadLine());
Console.WriteLine("Введите количество строк матрицы: ");
        int m = Convert.ToInt32(Console.ReadLine());
Console.WriteLine("Введите количество столбцов матрицы: ");
        int n = Convert.ToInt32(Console.ReadLine());


MyMatrix matrix = new MyMatrix(m,n,low, top);
Console.WriteLine("Матрица 1:");
matrix.print();
Console.WriteLine("Элемент [1,2] = " + matrix[0,1]);
Console.WriteLine("Матрица 2:");
MyMatrix matrix1 = new MyMatrix(m, n, low, top);
matrix1.print();
Console.WriteLine("Сложение матриц:");
(matrix + matrix1).print();
Console.WriteLine("Вычитание матриц (Матрица1-Матрица2):");
(matrix - matrix1).print();
Console.WriteLine("Умножение матриц(Матрица1*Матрица2)");
(matrix*matrix1).print();
MyMatrix matrix4 = new MyMatrix(n, m, low, top);
Console.WriteLine("Матрица4:");
matrix4.print();
Console.WriteLine("Умножение матриц(Матрица1*Матрица4)");
(matrix * matrix4).print();
Console.WriteLine("Умножение Матрицы1 на 2:");
(2 * matrix).print();
Console.WriteLine("Деление матрицы 1 на 3:");
(matrix / 3).print();

Console.WriteLine("//////////////////////////////////////////////////////////////////////////////////////");

Car[] cars = new Car[] { new Car("Toyota", 200, 2006), new Car("Lexus", 280, 2001), new Car("Mazda", 260, 2017)};
foreach(var car in cars) Console.WriteLine(car.Name+"\t"+car.MaxSpeed+"\t"+car.Year);
Console.WriteLine("По какому критерию будет проводиться сортировка(Name/Speed/Year)?");
string crit = Console.ReadLine();
switch (crit)
{
    case "Name":
        Array.Sort(cars, new CarComparer(CarComparer.SortCrit.Name));
        Console.WriteLine("Сортировка по названию:");
        foreach (var car in cars) Console.WriteLine(car.Name + "\t" + car.MaxSpeed + "\t" + car.Year);
        break;
    case "Speed":
        Array.Sort(cars, new CarComparer(CarComparer.SortCrit.MaxSpeed));
        Console.WriteLine("Сортировка по скорости:");
        foreach (var car in cars) Console.WriteLine(car.Name + "\t" + car.MaxSpeed + "\t" + car.Year);
        break;

    case "Year":
        Array.Sort(cars, new CarComparer(CarComparer.SortCrit.Year));
        Console.WriteLine("Сортировка по году выпуска:");
        foreach (var car in cars) Console.WriteLine(car.Name + "\t" + car.MaxSpeed + "\t" + car.Year);
        break;
   
        default: throw new Exception("Недопустимое значение сортировки");

        Console.WriteLine("////////////////////////////////////////////////////////////////////////////////");
        
        CarCatalog catalog = new CarCatalog(cars);
        
        Console.WriteLine("Прямой проход с первого элемента до последнего:");
        foreach (var car in catalog)
        {
            Console.WriteLine($"{car.Name} {car.Year} {car.MaxSpeed}");
        }

        Console.WriteLine();

        Console.WriteLine("Обратный проход от последнего к первому:");
        foreach (var car in catalog.GetReverseEnumerator())
        {
            Console.WriteLine($" {car.Name} {car.Year} {car.MaxSpeed}");
        }

        Console.WriteLine();

        Console.WriteLine("Проход по элементам массива с фильтром по году выпуска (2001):");
        foreach (var car in catalog.GetCarsByProductionYear(2001))
        {
            Console.WriteLine($" {car.Name} {car.Year} {car.MaxSpeed}");
        }

        Console.WriteLine();

        Console.WriteLine("Проход по элементам массива с фильтром по максимальной скорости (260):");
        foreach (var car in catalog.GetCarsByMaxSpeed(260))
        {
            Console.WriteLine($" {car.Name} {car.Year} {car.MaxSpeed}");
        }




}



public class MyMatrix
{
    public int m, n, low, top;
    double [,] matrix;
   // int[,] matrix = new int[,];
    public MyMatrix(int m,int n, int low,int top)
    {
        this.m = m;
        this.n = n;
        this.low = low;
        this.top = top;
        matrix = new double[m,n];
        var rand = new Random();
        for(int i = 0; i < m; i++) 
        {
            for(int j = 0; j < n; j++)
            {
                matrix[i,j] = rand.Next(low, top); 
               // Console.Write(matrix[i, j]+"\t");
            }
           // Console.WriteLine("\n");
        }

    }

    public void print()
    {
        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                 Console.Write(matrix[i, j]+"\t");
            }
            Console.WriteLine("\n");
        }
    } 
    public double this[int n, int m]
    {
        get { return matrix[n, m]; }
        set { matrix[n, m] = value; }
    }

    public static MyMatrix operator +(MyMatrix matrix1, MyMatrix matrix2)
    {   if(matrix1.m != matrix2.m || matrix2.n != matrix1.n) { Console.WriteLine("Размеры матриц не совпадают!!");return matrix1; }
        MyMatrix matrix3 = new MyMatrix(matrix1.m, matrix1.n, matrix1.low, matrix2.top);
        for (int i = 0; i<matrix1.m; ++i)
        {
            for(int j = 0; j<matrix1.n; ++j)
            {
                matrix3[i,j] = matrix1[i, j] + matrix2[i, j];
            }
        } 
        return matrix3;
    }

    public static MyMatrix operator -(MyMatrix matrix1, MyMatrix matrix2)
    {
        if (matrix1.m != matrix2.m || matrix2.n != matrix1.n) { Console.WriteLine("Размеры матриц не совпадают!!"); return matrix1; }
        MyMatrix matrix3 = new MyMatrix(matrix1.m, matrix1.n, matrix2.low, matrix2.top);
        for (int i = 0; i < matrix1.m; ++i)
        {
            for (int j = 0; j < matrix1.n; ++j)
            {
                matrix3[i, j] = matrix1[i, j] - matrix2[i, j];
            }
        }
        return matrix3;
    }

    public static MyMatrix operator *(MyMatrix matrix1, MyMatrix matrix2)
    {
        if (matrix1.n != matrix2.m)
        {
            throw new Exception("Количество столбцов в первой матрице не равно количеству строк во второ!!Error!!");
           // Console.WriteLine("Количество столбцов в первой матрице не равно количеству строк во второ!!Error!!");
           // return matrix1;

        }
        MyMatrix matrix3 = new MyMatrix(matrix1.m, matrix2.n, matrix2.low, matrix2.top);
        for (int i = 0; i < matrix1.m; i++)
            for (int j = 0; j < matrix2.n; j++)
                for (int k = 0; k < matrix2.m; k++) 
                {
                    matrix3[i, j] += matrix1[i, k] * matrix2[k, j];
                }

        return matrix3;
    }

    public static MyMatrix operator *(MyMatrix matrix1, double n)
    {
        MyMatrix matrix3 = new MyMatrix(matrix1.m, matrix1.n, matrix1.low, matrix1.top);
        for (int i = 0; i < matrix1.m; ++i)
        {
            for (int j = 0; j < matrix1.n; ++j)
            {
                matrix3[i, j] = matrix1[i, j] * n;
            }
        }
        return matrix3;
    }

    public static MyMatrix operator *(double n, MyMatrix matrix1)
    {
        MyMatrix matrix3 = new MyMatrix(matrix1.m, matrix1.n, matrix1.low, matrix1.top);
        for (int i = 0; i < matrix1.m; ++i)
        {
            for (int j = 0; j < matrix1.n; ++j)
            {
                matrix3[i, j] = matrix1[i, j] * n;
            }
        }
        return matrix3;
    }

    public static MyMatrix operator /(MyMatrix matrix1, double n)
    {
        MyMatrix matrix3 = new MyMatrix(matrix1.m, matrix1.n, matrix1.low, matrix1.top);
        for (int i = 0; i < matrix1.m; ++i)
        {
            for (int j = 0; j < matrix1.n; ++j)
            {
                matrix3[i, j] = matrix1[i, j] / n;
            }
        }
        return matrix3;
    }


}




public class Car
{
    public double MaxSpeed { get; set; }
    public string Name { get; set; }
    public int Year { get; set; }

    public Car(string name, int speed, int year)
    {
        MaxSpeed = speed;
        Name = name;
        Year = year;
    }


   /* public double maxSpeed
    {
        get { return this.MaxSpeed; }
    }
    public string name
    {
        get { return this.Name; }
    }

    public int year
    {
        get { return this.Year; }
    }*/

}

public class CarComparer : IComparer<Car>
{
    SortCrit crit;
    public enum SortCrit
    {
        Name,
        Year,
        MaxSpeed
    }

    public CarComparer(SortCrit crit)
    {
        this.crit = crit;
    }

    public int Compare(Car x, Car y)
    {
        switch (crit)
        {
            case SortCrit.Name:
                return string.Compare(x.Name, y.Name);
            case SortCrit.Year:
                return x.Year.CompareTo(y.Year);
            case SortCrit.MaxSpeed:
                return x.MaxSpeed.CompareTo(y.MaxSpeed);
            default: throw new ArgumentException("Неизвестный критерий сортировки!");
        }
    }
}

public class CarCatalog : IEnumerable<Car>
{
    private Car[] cars;

    public CarCatalog(Car[] cars)
    {
        this.cars = cars;
    }


    public IEnumerator<Car> GetEnumerator()
    {
        // Прямой проход с первого элемента до последнего
        for (int i = 0; i < cars.Length; i++)
        {
            yield return cars[i];
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public IEnumerable<Car> GetReverseEnumerator()
    {
        // Обратный проход от последнего к первому
        for (int i = cars.Length - 1; i >= 0; i--)
        {
            yield return cars[i];
        }
    }

    public IEnumerable<Car> GetCarsByProductionYear(int Year)
    {
        // Проход по элементам массива с фильтром по году выпуска
        foreach (var car in cars)
        {
            if (car.Year == Year)
            {
                yield return car;
            }
        }
    }

    public IEnumerable<Car> GetCarsByMaxSpeed(int maxSpeed)
    {
        // Проход по элементам массива с фильтром по максимальной скорости
        foreach (var car in cars)
        {
            if (car.MaxSpeed <= maxSpeed)
            {
                yield return car;
            }
        }
    }
}







