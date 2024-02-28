using System;
using System.Drawing;

namespace Lab4CSharp
{
    internal class IsoscelesTriangle
    {
        private double side;
        private double basis;
        private Color color;

        public IsoscelesTriangle(double side, double basis, Color color)
        {
            this.side = side;
            this.basis = basis;
            this.color = color;
        }

        public IsoscelesTriangle(int side, int b) : this(side, b, Color.White) { }

        public double A { get { return side; } set { side = value; } }

        public double B { get { return basis; } set { basis = value; } }

        public Color Color { get { return color; } }

        public double Perimeter()
        {
            return 2 * side + basis;
        }

        public double Area()
        {
            return 0.5 * basis * Math.Sqrt((side * side) - ((basis * basis) / 4));
        }

        public bool RightTriangle()
        {
            return side == basis;
        }

        public void DisplayFields()
        {
            Console.WriteLine($"Side: {side}");
            if (RightTriangle() == false)
                Console.WriteLine($"Basis: {basis}");
            Console.WriteLine($"Color: {color.Name}");
        }

        // Індексатор
        public object this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0:
                        return side;
                    case 1:
                        return basis;
                    case 2:
                        return color.Name;
                    default:
                        throw new ArgumentOutOfRangeException("index", "Index out of range");
                }
            }
            set
            {
                switch (index)
                {
                    case 0:
                        side = Convert.ToDouble(value);
                        break;
                    case 1:
                        basis = Convert.ToDouble(value);
                        break;
                    case 2:
                        color = (Color)value;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("index", "Index out of range");
                }
            }
        }

        // Перевантаження операторів
        public static IsoscelesTriangle operator ++(IsoscelesTriangle triangle)
        {
            triangle.side++;
            triangle.basis++;
            return triangle;
        }

        public static IsoscelesTriangle operator --(IsoscelesTriangle triangle)
        {
            triangle.side--;
            triangle.basis--;
            return triangle;
        }

        public static implicit operator bool(IsoscelesTriangle triangle)
        {
            return triangle.side > 0 && triangle.basis > 0 && (triangle.side + triangle.side) > triangle.basis;
        }

        public static implicit operator string(IsoscelesTriangle triangle)
        {
            return $"Side: {triangle.side}, Basis: {triangle.basis}, Color: {triangle.color.Name}";
        }

        public static implicit operator IsoscelesTriangle(string triangleString)
        {
            string[] parts = triangleString.Split(',');
            double side = double.Parse(parts[0].Split(':')[1]);
            double basis = double.Parse(parts[1].Split(':')[1]);
            Color color = Color.FromName(parts[2].Split(':')[1].Trim());
            return new IsoscelesTriangle(side, basis, color);
        }

        public static IsoscelesTriangle operator +(IsoscelesTriangle triangle, double scalar)
        {
            triangle.side += scalar;
            triangle.basis += scalar;
            return triangle;
        }
    }
}
