using System.Diagnostics.CodeAnalysis;

namespace Lab4CSharp
{
    class Vector
    {
        protected decimal[] ArrayDecimal;
        protected uint size;
        protected int codeError;
        protected static uint num_vec;

        public uint Size => size;
        public int CodeError
        {
            get { return codeError; }
            set { codeError = value; }
        }

        public Vector()
        {
            ArrayDecimal = new decimal[1];
            size = 1;
            codeError = 0;
            num_vec++;
        }

        public Vector(uint size)
        {
            ArrayDecimal = new decimal[size];
            this.size = size;
            for (int i = 0; i < size; i++)
            {
                ArrayDecimal[i] = 0;
            }
            codeError = 0;
            num_vec++;
        }

        public Vector(uint size, decimal initializationValue)
        {
            ArrayDecimal = new decimal[size];
            this.size = size;
            for (int i = 0; i < size; i++)
            {
                ArrayDecimal[i] = initializationValue;
            }
            codeError = 0;
            num_vec++;
        }

        ~Vector()
        {
            Console.WriteLine("Destructor is called.");
        }

        public void InputValues()
        {
            for (int i = 0; i < size; i++)
            {
                Console.Write($"Enter element {i}: ");
                ArrayDecimal[i] = Convert.ToDecimal(Console.ReadLine());
            }
        }

        public void Display()
        {
            Console.WriteLine("Vector elements:");
            for (int i = 0; i < size; i++)
            {
                Console.WriteLine($"Element {i}: {ArrayDecimal[i]}");
            }
        }

        public void AssignValue(decimal value)
        {
            for (int i = 0; i < size; i++)
            {
                ArrayDecimal[i] = value;
            }
        }

        public decimal this[int index]
        {
            get
            {
                if (index < 0 || index >= size)
                {
                    codeError = -1;
                    return 0;
                }
                else
                {
                    codeError = 0;
                    return ArrayDecimal[index];
                }
            }
            set
            {
                if (index >= 0 && index < size)
                {
                    ArrayDecimal[index] = value;
                }
                else
                {
                    codeError = -1;
                }
            }
        }

        public static uint NumVec
        {
            get { return num_vec; }
        }

        // Overloading unary operators
        public static Vector operator ++(Vector vector)
        {
            for (int i = 0; i < vector.size; i++)
            {
                vector.ArrayDecimal[i]++;
            }
            return vector;
        }

        public static Vector operator --(Vector vector)
        {
            for (int i = 0; i < vector.size; i++)
            {
                vector.ArrayDecimal[i]--;
            }
            return vector;
        }

        public static explicit operator bool(Vector vector)
        {
            if (vector.size == 0)
            {
                return false;
            }
            foreach (decimal element in vector.ArrayDecimal)
            {
                if (element == 0)
                {
                    return false;
                }
            }
            return true;
        }

        public static Vector operator ~(Vector vector)
        {
            Vector result = new Vector(vector.Size);
            for (int i = 0; i < vector.ArrayDecimal.Length; i++)
            {
                result.ArrayDecimal[i] = BitwiseNot(vector.ArrayDecimal[i]);
            }
            return result;
        }

        private static decimal BitwiseNot(decimal value)
        {
            // Перетворення десяткового числа на ціле, застосування побітового доповнення,
            // а потім знову перетворення на десяткове число
            return (decimal)(~(ulong)value);
        }

        public static Vector operator +(Vector vector1, Vector vector2)
        {
            if (vector1.Size != vector2.Size)
            {
                throw new ArgumentException("Vectors must be of the same size for addition.");
            }

            Vector result = new Vector(vector1.Size);
            for (int i = 0; i < vector1.ArrayDecimal.Length; i++)
            {
                result.ArrayDecimal[i] = vector1.ArrayDecimal[i] + vector2.ArrayDecimal[i];
            }
            return result;
        }

        public static Vector operator +(Vector vector, decimal scalar)
        {
            Vector result = new Vector(vector.Size);
            for (int i = 0; i < vector.ArrayDecimal.Length; i++)
            {
                result.ArrayDecimal[i] = vector.ArrayDecimal[i] + scalar;
            }
            return result;
        }

        public static Vector operator -(Vector vector1, Vector vector2)
        {
            if (vector1.Size != vector2.Size)
            {
                throw new ArgumentException("Vectors must be of the same size for subtraction.");
            }

            Vector result = new Vector(vector1.Size);
            for (int i = 0; i < vector1.ArrayDecimal.Length; i++)
            {
                result.ArrayDecimal[i] = vector1.ArrayDecimal[i] - vector2.ArrayDecimal[i];
            }
            return result;
        }

        public static Vector operator -(Vector vector, decimal scalar)
        {
            Vector result = new Vector(vector.Size);
            for (int i = 0; i < vector.ArrayDecimal.Length; i++)
            {
                result.ArrayDecimal[i] = vector.ArrayDecimal[i] - scalar;
            }
            return result;
        }

        public static Vector operator *(Vector vector1, Vector vector2)
        {
            if (vector1.Size != vector2.Size)
            {
                throw new ArgumentException("Vectors must be of the same size for multiplication.");
            }

            Vector result = new Vector(vector1.Size);
            for (int i = 0; i < vector1.ArrayDecimal.Length; i++)
            {
                result.ArrayDecimal[i] = vector1.ArrayDecimal[i] * vector2.ArrayDecimal[i];
            }
            return result;
        }

        public static Vector operator *(Vector vector, decimal scalar)
        {
            Vector result = new Vector(vector.Size);
            for (int i = 0; i < vector.ArrayDecimal.Length; i++)
            {
                result.ArrayDecimal[i] = vector.ArrayDecimal[i] * scalar;
            }
            return result;
        }

        public static Vector operator /(Vector vector1, Vector vector2)
        {
            if (vector1.Size != vector2.Size)
            {
                throw new ArgumentException("Vectors must be of the same size for division.");
            }

            Vector result = new Vector(vector1.Size);
            for (int i = 0; i < vector1.ArrayDecimal.Length; i++)
            {
                result.ArrayDecimal[i] = vector1.ArrayDecimal[i] / vector2.ArrayDecimal[i];
            }
            return result;
        }

        public static Vector operator /(Vector vector, decimal scalar)
        {
            Vector result = new Vector(vector.Size);
            for (int i = 0; i < vector.ArrayDecimal.Length; i++)
            {
                result.ArrayDecimal[i] = vector.ArrayDecimal[i] / scalar;
            }
            return result;
        }

        public static Vector operator %(Vector vector1, Vector vector2)
        {
            if (vector1.Size != vector2.Size)
            {
                throw new ArgumentException("Vectors must be of the same size for integer division.");
            }

            Vector result = new Vector(vector1.Size);
            for (int i = 0; i < vector1.ArrayDecimal.Length; i++)
            {
                result.ArrayDecimal[i] = vector1.ArrayDecimal[i] % vector2.ArrayDecimal[i];
            }
            return result;
        }

        public static Vector operator %(Vector vector, decimal scalar)
        {
            Vector result = new Vector(vector.Size);
            for (int i = 0; i < vector.ArrayDecimal.Length; i++)
            {
                result.ArrayDecimal[i] = vector.ArrayDecimal[i] % scalar;
            }
            return result;
        }

        public static Vector operator |(Vector vector1, Vector vector2)
        {
            if (vector1.Size != vector2.Size)
            {
                throw new ArgumentException("Vectors must be of the same size for integer binary sum.");
            }

            Vector result = new Vector(vector1.Size);
            for (int i = 0; i < vector1.ArrayDecimal.Length; i++)
            {
                // Використовуємо метод BitwiseOr для десяткових чисел
                result.ArrayDecimal[i] = BitwiseOr(vector1.ArrayDecimal[i], vector2.ArrayDecimal[i]);
            }
            return result;
        }

        public static Vector operator |(Vector vector, decimal scalar)
        {
            Vector result = new Vector(vector.Size);
            for (int i = 0; i < vector.ArrayDecimal.Length; i++)
            {
                // Використовуємо метод BitwiseOr для десяткових чисел
                result.ArrayDecimal[i] = BitwiseOr(vector.ArrayDecimal[i], scalar);
            }
            return result;
        }

        public static Vector operator ^(Vector vector1, Vector vector2)
        {
            if (vector1.Size != vector2.Size)
            {
                throw new ArgumentException("Vectors must be of the same size for integer xor.");
            }

            Vector result = new Vector(vector1.Size);
            for (int i = 0; i < vector1.ArrayDecimal.Length; i++)
            {
                // Використовуємо метод BitwiseXor для десяткових чисел
                result.ArrayDecimal[i] = BitwiseXor(vector1.ArrayDecimal[i], vector2.ArrayDecimal[i]);
            }
            return result;
        }

        public static Vector operator ^(Vector vector, decimal scalar)
        {
            Vector result = new Vector(vector.Size);
            for (int i = 0; i < vector.ArrayDecimal.Length; i++)
            {
                // Використовуємо метод BitwiseXor для десяткових чисел
                result.ArrayDecimal[i] = BitwiseXor(vector.ArrayDecimal[i], scalar);
            }
            return result;
        }

        public static Vector operator >>(Vector vector1, int scalar)
        {
            Vector result = new Vector(vector1.Size);
            for (int i = 0; i < vector1.ArrayDecimal.Length; i++)
            {
                // Використовуємо метод BitwiseRightShift для десяткових чисел
                result.ArrayDecimal[i] = BitwiseRightShift(vector1.ArrayDecimal[i], scalar);
            }
            return result;
        }

        public static Vector operator <<(Vector vector1, int scalar)
        {
            Vector result = new Vector(vector1.Size);
            for (int i = 0; i < vector1.ArrayDecimal.Length; i++)
            {
                // Використовуємо метод BitwiseLeftShift для десяткових чисел
                result.ArrayDecimal[i] = BitwiseLeftShift(vector1.ArrayDecimal[i], scalar);
            }
            return result;
        }

        public static bool operator ==(Vector vector1, Vector vector2)
        {
            if (vector1.Size != vector2.Size)
            {
                throw new ArgumentException("Vectors must be of the same size for integer equality check.");
            }

            for (int i = 0; i < vector1.ArrayDecimal.Length; i++)
            {
                if (vector1.ArrayDecimal[i] != vector2.ArrayDecimal[i]) { return false; }
            }
            return true;
        }

        public static bool operator !=(Vector vector1, Vector vector2)
        {
            if (vector1.Size != vector2.Size)
            {
                throw new ArgumentException("Vectors must be of the same size for integer non equality check.");
            }

            for (int i = 0; i < vector1.ArrayDecimal.Length; i++)
            {
                if (vector1.ArrayDecimal[i] == vector2.ArrayDecimal[i]) { return false; }
            }
            return true;
        }

        public override bool Equals([AllowNull] object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            Vector other = (Vector)obj;
            if (size != other.size)
            {
                return false;
            }
            for (int i = 0; i < ArrayDecimal.Length; i++)
            {
                if (ArrayDecimal[i] != other.ArrayDecimal[i])
                {
                    return false;
                }
            }
            return true;
        }

        public override int GetHashCode()
        {
            unchecked // дозволяє використовувати переповнення значень для обчислення хеш-коду
            {
                int hash = 17;
                hash = hash * 23 + size.GetHashCode();
                foreach (var item in ArrayDecimal)
                {
                    hash = hash * 23 + item.GetHashCode();
                }
                return hash;
            }
        }

        public static bool operator >(Vector vector1, Vector vector2)
        {
            return vector1.Size > vector2.Size;
        }

        public static bool operator <(Vector vector1, Vector vector2)
        {
            return vector1.Size < vector2.Size;
        }

        public static bool operator >=(Vector vector1, Vector vector2)
        {
            return vector1.Size >= vector2.Size;
        }

        public static bool operator <=(Vector vector1, Vector vector2)
        {
            return vector1.Size <= vector2.Size;
        }

        private static decimal BitwiseOr(decimal value1, decimal value2)
        {
            return (decimal)((ulong)value1 | (ulong)value2);
        }

        private static decimal BitwiseXor(decimal value1, decimal value2)
        {
            return (decimal)((ulong)value1 ^ (ulong)value2);
        }

        private static decimal BitwiseRightShift(decimal value, int shift)
        {
            return value / (decimal)Math.Pow(2, shift);
        }   

        private static decimal BitwiseLeftShift(decimal value, int shift)
        {
            return value * (decimal)Math.Pow(2, shift);
        }
    }
}
