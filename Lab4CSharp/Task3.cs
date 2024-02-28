using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Runtime.Intrinsics;

namespace Lab4CSharp
{
    class Matrix
    {
        protected decimal[,] DecimalArray;
        protected int n, m;
        protected int codeError;

        public int Rows => n;
        public int Columns => m;
        public int CodeError
        {
            get { return codeError; }
            set { codeError = value; }
        }

        public Matrix(int n = 1, int m = 1, decimal initializationValue = 0)
        {
            DecimalArray = new decimal[n, m];
            this.n = n;
            this.m = m;
            codeError = 0;
            InitializeArray(initializationValue);
        }

        ~Matrix()
        {
            Console.WriteLine("Destructor is called.");
        }

        private void InitializeArray(decimal value = 0)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    DecimalArray[i, j] = value;
                }
            }
        }

        public void InputValues()
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    Console.Write($"Enter element [{i},{j}]: ");
                    DecimalArray[i, j] = Convert.ToDecimal(Console.ReadLine());
                }
            }
        }

        public void Display()
        {
            Console.WriteLine("Matrix elements:");
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    Console.Write($"{DecimalArray[i, j]} ");
                }
                Console.WriteLine();
            }
        }

        public void AssignValue(decimal value)
        {
            InitializeArray(value);
        }

        public decimal this[int i, int j]
        {
            get
            {
                if (i < 0 || i >= n || j < 0 || j >= m)
                {
                    codeError = -1;
                    return 0;
                }
                else
                {
                    codeError = 0;
                    return DecimalArray[i, j];
                }
            }
            set
            {
                if (i >= 0 && i < n && j >= 0 && j < m)
                {
                    DecimalArray[i, j] = value;
                }
                else
                {
                    codeError = -1;
                }
            }
        }

        public decimal this[int k]
        {
            get
            {
                int i = k / m;
                int j = k % m;
                if (i < 0 || i >= n || j < 0 || j >= m)
                {
                    codeError = -1;
                    return 0;
                }
                else
                {
                    codeError = 0;
                    return DecimalArray[i, j];
                }
            }
            set
            {
                int i = k / m;
                int j = k % m;
                if (i >= 0 && i < n && j >= 0 && j < m)
                {
                    DecimalArray[i, j] = value;
                }
                else
                {
                    codeError = -1;
                }
            }
        }

        public override bool Equals([AllowNull] object obj)
        {
            if (obj == null || !(obj is Matrix))
                return false;

            Matrix other = (Matrix)obj;

            if (this.Rows != other.Rows || this.Columns != other.Columns)
                return false;

            for (int i = 0; i < this.Rows; i++)
            {
                for (int j = 0; j < this.Columns; j++)
                {
                    if (this.DecimalArray[i, j] != other.DecimalArray[i, j])
                        return false;
                }
            }

            return true;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + this.Rows.GetHashCode();
                hash = hash * 23 + this.Columns.GetHashCode();
                for (int i = 0; i < this.Rows; i++)
                {
                    for (int j = 0; j < this.Columns; j++)
                    {
                        hash = hash * 23 + this.DecimalArray[i, j].GetHashCode();
                    }
                }
                return hash;
            }
        }

        public static bool operator ==(Matrix matrix1, Matrix matrix2)
        {
            if (matrix1.Rows != matrix2.Rows || matrix1.Columns != matrix2.Columns)
                return false;

            for (int i = 0; i < matrix1.Rows; i++)
            {
                for (int j = 0; j < matrix1.Columns; j++)
                {
                    if (matrix1[i, j] != matrix2[i, j])
                        return false;
                }
            }

            return true;
        }

        public static bool operator !=(Matrix matrix1, Matrix matrix2)
        {
            return !(matrix1 == matrix2);
        }

        public static Matrix operator ++(Matrix matrix)
        {
            Matrix result = new Matrix(matrix.n, matrix.m);
            for (int i = 0; i < matrix.n; i++)
            {
                for (int j = 0; j < matrix.m; j++)
                {
                    result.DecimalArray[i, j] = matrix.DecimalArray[i, j] + 1;
                }
            }
            return result;
        }

        public static Matrix operator --(Matrix matrix)
        {
            Matrix result = new Matrix(matrix.n, matrix.m);
            for (int i = 0; i < matrix.n; i++)
            {
                for (int j = 0; j < matrix.m; j++)
                {
                    result.DecimalArray[i, j] = matrix.DecimalArray[i, j] - 1;
                }
            }
            return result;
        }

        public static explicit operator bool(Matrix matrix)
        {
            foreach (decimal element in matrix.DecimalArray)
            {
                if (element == 0)
                {
                    return false;
                }
            }
            return true;
        }

        public static Matrix operator ~(Matrix matrix)
        {
            Matrix result = new Matrix(matrix.n, matrix.m);
            for (int i = 0; i < matrix.n; i++)
            {
                for (int j = 0; j < matrix.m; j++)
                {
                    //result.DecimalArray[i, j] = ~matrix.DecimalArray[i, j];
                    result.DecimalArray[i, j] = BitwiseNot(matrix.DecimalArray[i, j]);
                }
            }
            return result;
        }

        private static decimal BitwiseNot(decimal value)
        {
            // Перетворення десяткового числа на ціле, застосування побітового доповнення,
            // а потім знову перетворення на десяткове число
            return (decimal)(~(ulong)value);
        }

        public static Matrix operator +(Matrix matrix1, Matrix matrix2)
        {
            if (matrix1.n != matrix2.n || matrix1.m != matrix2.m)
            {
                throw new ArgumentException("Matrices must have the same dimensions for addition.");
            }

            Matrix result = new Matrix(matrix1.n, matrix1.m);
            for (int i = 0; i < matrix1.n; i++)
            {
                for (int j = 0; j < matrix1.m; j++)
                {
                    result.DecimalArray[i, j] = matrix1.DecimalArray[i, j] + matrix2.DecimalArray[i, j];
                }
            }
            return result;
        }

        public static Matrix operator +(Matrix matrix, decimal scalar)
        {
            Matrix result = new Matrix(matrix.n, matrix.m);
            for (int i = 0; i < matrix.n; i++)
            {
                for (int j = 0; j < matrix.m; j++)
                {
                    result.DecimalArray[i, j] = matrix.DecimalArray[i, j] + scalar;
                }
            }
            return result;
        }

        public static Matrix operator -(Matrix matrix1, Matrix matrix2)
        {
            if (matrix1.n != matrix2.n || matrix1.m != matrix2.m)
            {
                throw new ArgumentException("Matrices must have the same dimensions for subtraction.");
            }

            Matrix result = new Matrix(matrix1.n, matrix1.m);
            for (int i = 0; i < matrix1.n; i++)
            {
                for (int j = 0; j < matrix1.m; j++)
                {
                    result.DecimalArray[i, j] = matrix1.DecimalArray[i, j] - matrix2.DecimalArray[i, j];
                }
            }
            return result;
        }

        public static Matrix operator -(Matrix matrix, decimal scalar)
        {
            Matrix result = new Matrix(matrix.n, matrix.m);
            for (int i = 0; i < matrix.n; i++)
            {
                for (int j = 0; j < matrix.m; j++)
                {
                    result.DecimalArray[i, j] = matrix.DecimalArray[i, j] - scalar;
                }
            }
            return result;
        }

        public static Matrix operator *(Matrix matrix1, Matrix matrix2)
        {
            if (matrix1.n != matrix2.n || matrix1.m != matrix2.m)
            {
                throw new ArgumentException("Matrices must have the same dimensions for multiplication.");
            }

            Matrix result = new Matrix(matrix1.n, matrix1.m);
            for (int i = 0; i < matrix1.n; i++)
            {
                for (int j = 0; j < matrix1.m; j++)
                {
                    result.DecimalArray[i, j] = matrix1.DecimalArray[i, j] * matrix2.DecimalArray[i, j];
                }
            }
            return result;
        }

        public static Matrix operator *(Matrix matrix, decimal scalar)
        {
            Matrix result = new Matrix(matrix.n, matrix.m);
            for (int i = 0; i < matrix.n; i++)
            {
                for (int j = 0; j < matrix.m; j++)
                {
                    result.DecimalArray[i, j] = matrix.DecimalArray[i, j] * scalar;
                }
            }
            return result;
        }

        public static Matrix operator /(Matrix matrix1, Matrix matrix2)
        {
            if (matrix1.n != matrix2.n || matrix1.m != matrix2.m)
            {
                throw new ArgumentException("Matrices must have the same dimensions for division.");
            }

            Matrix result = new Matrix(matrix1.n, matrix1.m);
            for (int i = 0; i < matrix1.n; i++)
            {
                for (int j = 0; j < matrix1.m; j++)
                {
                    if (matrix2.DecimalArray[i, j] == 0)
                    {
                        throw new DivideByZeroException("Division by zero encountered.");
                    }
                    result.DecimalArray[i, j] = matrix1.DecimalArray[i, j] / matrix2.DecimalArray[i, j];
                }
            }
            return result;
        }

        public static Matrix operator /(Matrix matrix, decimal scalar)
        {
            if (scalar == 0)
            {
                throw new DivideByZeroException("Division by zero encountered.");
            }

            Matrix result = new Matrix(matrix.n, matrix.m);
            for (int i = 0; i < matrix.n; i++)
            {
                for (int j = 0; j < matrix.m; j++)
                {
                    result.DecimalArray[i, j] = matrix.DecimalArray[i, j] / scalar;
                }
            }
            return result;
        }

        public static Matrix operator %(Matrix matrix1, Matrix matrix2)
        {
            if (matrix1.n != matrix2.n || matrix1.m != matrix2.m)
            {
                throw new ArgumentException("Matrices must have the same dimensions for integer division.");
            }

            Matrix result = new Matrix(matrix1.n, matrix1.m);
            for (int i = 0; i < matrix1.n; i++)
            {
                for (int j = 0; j < matrix1.m; j++)
                {
                    result.DecimalArray[i, j] = matrix1.DecimalArray[i, j] % matrix2.DecimalArray[i, j];
                }
            }
            return result;
        }

        public static Matrix operator %(Matrix matrix, decimal scalar)
        {
            Matrix result = new Matrix(matrix.n, matrix.m);
            for (int i = 0; i < matrix.n; i++)
            {
                for (int j = 0; j < matrix.m; j++)
                {
                    result.DecimalArray[i, j] = matrix.DecimalArray[i, j] % scalar;
                }
            }
            return result;
        }

        public static Matrix operator |(Matrix matrix1, Matrix matrix2)
        {
            if (matrix1.n != matrix2.n || matrix1.m != matrix2.m)
            {
                throw new ArgumentException("Matrices must have the same dimensions for binary OR operator.");
            }

            Matrix result = new Matrix(matrix1.n, matrix1.m);
            for (int i = 0; i < matrix1.n; i++)
            {
                for (int j = 0; j < matrix1.m; j++)
                {
                    result.DecimalArray[i, j] = BitwiseOr(matrix1.DecimalArray[i, j], matrix2.DecimalArray[i, j]);
                }
            }
            return result;
        }

        public static Matrix operator |(Matrix matrix, decimal scalar)
        {
            Matrix result = new Matrix(matrix.n, matrix.m);
            for (int i = 0; i < matrix.n; i++)
            {
                for (int j = 0; j < matrix.m; j++)
                {
                    result.DecimalArray[i, j] = BitwiseOr(matrix.DecimalArray[i, j], scalar);
                }
            }
            return result;
        }

        public static Matrix operator ^(Matrix matrix1, Matrix matrix2)
        {
            if (matrix1.n != matrix2.n || matrix1.m != matrix2.m)
            {
                throw new ArgumentException("Matrices must have the same dimensions for XOR.");
            }

            Matrix result = new Matrix(matrix1.n, matrix1.m);
            for (int i = 0; i < matrix1.n; i++)
            {
                for (int j = 0; j < matrix1.m; j++)
                {
                    result.DecimalArray[i, j] = BitwiseXor(matrix1.DecimalArray[i, j], matrix2.DecimalArray[i, j]);
                }
            }
            return result;
        }

        public static Matrix operator ^(Matrix matrix, decimal scalar)
        {
            Matrix result = new Matrix(matrix.n, matrix.m);
            for (int i = 0; i < matrix.n; i++)
            {
                for (int j = 0; j < matrix.m; j++)
                {
                    result.DecimalArray[i, j] = BitwiseXor(matrix.DecimalArray[i, j], scalar);
                }
            }
            return result;
        }

        public static Matrix operator <<(Matrix matrix, int scalar)
        {
            Matrix result = new Matrix(matrix.n, matrix.m);
            for (int i = 0; i < matrix.n; i++)
            {
                for (int j = 0; j < matrix.m; j++)
                {
                    result.DecimalArray[i, j] = BitwiseLeftShift(matrix.DecimalArray[i, j], scalar);
                }
            }
            return result;
        }        

        public static Matrix operator >>(Matrix matrix, int scalar)
        {
            Matrix result = new Matrix(matrix.n, matrix.m);
            for (int i = 0; i < matrix.n; i++)
            {
                for (int j = 0; j < matrix.m; j++)
                {
                    result.DecimalArray[i, j] = BitwiseRightShift(matrix.DecimalArray[i, j], scalar);
                }
            }
            return result;
        }

        public static bool operator >(Matrix matrix1, Matrix matrix2)
        {
            return matrix1.Rows * matrix1.Columns > matrix2.Rows * matrix2.Columns;
        }

        public static bool operator <(Matrix matrix1, Matrix matrix2)
        {
            return matrix1.Rows * matrix1.Columns < matrix2.Rows * matrix2.Columns;
        }

        public static bool operator >=(Matrix matrix1, Matrix matrix2)
        {
            return matrix1.Rows * matrix1.Columns >= matrix2.Rows * matrix2.Columns;
        }

        public static bool operator <=(Matrix matrix1, Matrix matrix2)
        {
            return matrix1.Rows * matrix1.Columns <= matrix2.Rows * matrix2.Columns;
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
