using System;
using System.Collections.Generic;
/*
 * Задание
 * Имеется массив целых чисел длины N и число M <= N. 
 * Написать код, который проверяет, что среди первых M элементов нет нуля. 
 * Надо просто вывести Yes/No.  
 * Ограничения. 
 * 1. Нельзя использовать логические операторы и операторы сравнения (==, <, >, !=, <=, >=) 
 * 2. Нельзя использовать циклы.  
 * 3. Нельзя использовать операторы ветвления: switch, if, ?:, ассемблерные вставки. 
 * 4. Нельзя использовать библиотечные функции кроме как для вывода результата. 
 * 5. Нельзя использовать goto, longjmp, исключения (если есть в языке).  
 * Предполагается, что задача решается на C, но можно и на других языках решать. Например, на C#, JS или ещё чем-то.
 * 
 * Task
 * Have an array of integers length N and number M <= N.
 * Need to write code which check that from 0 to M elements have not zero
 * The result is output Yes or No
 * Limits
 * 1. Don't use logical operators of comparison (==, <, >, !=, <=, >=) 
 * 2. Don't use loops
 * 3. Don't use conditional operator switch, if, ?:, assembler injectcts
 * 4. 
 */
namespace UnregularTask
{
    public static class Program
    {
        private static Dictionary<int, string> answers = new Dictionary<int, string>();
        private static Dictionary<int, Func<int, int>> preAnswers = new Dictionary<int, Func<int, int>>();
        private static Dictionary<int, Func<int, int[], int>> conditions = new Dictionary<int, Func<int, int[], int>>();
        public static void Main(string[] args)
        {
            preAnswers.Add(-1, a => -1);
            preAnswers.Add(0, a => (a * -1) >> 31);

            answers.Add(-1, "No");
            answers.Add(0, "Yes");

            conditions.Add(0, foo); // continue
            conditions.Add(-1, (a, c) => 1); // exit

            int[] arr = { 1, 2, 0, 5, 6 };
            int m = 4;

            Console.WriteLine(StartMethod(arr, m));
        }

        public static string StartMethod(int[] arr, int m)
        {
            var result = foo(m - 1, arr); // start recursion

            var preanswer = preAnswers[result >> 31];
            return answers[preanswer(result)];
        }

        static int foo(int index, int[] arr)
        {
            var result = arr[index] * conditions[index -1 >> 31](index - 1,  arr);
            return result;
        }
    }
}
