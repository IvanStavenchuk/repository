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
 * Ther is an array of integers length N and number M <= N.
 * Write a code that checks that there is no zero among from 0 to M elements.
 * Just output the Yes/No.
 * Limits
 * 1. Don't use logical operators and operators of comparison (==, <, >, !=, <=, >=) 
 * 2. Don't use loops
 * 3. Don't use conditional operators switch, if, ?:, assembler injections
 * 4. Don't use libraries functions, except to output the result.
 * 5. Don't use goto, longjmp, exceptions (if language has it)
 * Sujest that the task is solving on C, but it could be solved by another one. For example on C#, JS, or others.
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
