
using System;
using System.Collections.Generic;

namespace GradeBook
{
    public delegate void GradeAddedDelegate()

    public class Book
    {
        public Book(string name)
        {
            grades = new List<double>();
            Name = name;
        }
        public void AddGrade(char letter)
        {
            switch (letter)
            {
                case 'A':
                    AddGrade(100);
                    break;
                case 'B':
                    AddGrade(90);
                    break;
                case 'C':
                    AddGrade(80);
                    break;
                case 'D':
                    AddGrade(70);
                    break;
                default:
                    AddGrade(0);
                    break;
            }
        }
        public void AddGrade(double grade)
        {
            if (grade <= 100 && grade >= 0)
            {
                grades.Add(grade);
                ValidGrade = true;
            }
            else
            {
                ValidGrade = false;
                //throw new ArgumentException($"Invalid Grade {nameof(grade)}");
            }
        }
        public Statistics GetStatistics()
        {
            var results = new Statistics();
            results.Average = 0.0;
            results.High = double.MinValue;
            results.Low = double.MaxValue;

            for (var index = 0; index < grades.Count; index++)
            {
                results.Low = Math.Min(grades[index], results.Low);
                results.High = Math.Max(grades[index], results.High);
                results.Average += grades[index];
            }
            
            results.Average /= grades.Count;

            switch (results.Average)
            {
                case var d when d >= 90.0:
                    results.Letter = 'A';
                    break;
                case var d when d >= 80.0:
                    results.Letter = 'B';
                    break;
                case var d when d >= 70.0:
                    results.Letter = 'C';
                    break;
                case var d when d >= 60.0:
                    results.Letter = 'D';
                    break;
                default:
                    results.Letter = 'F';
                    break;
            }
            return results;
        }

        private List<double> grades;
        
        public string Name
        {
            get; set;
        }


        public const string CATEGORY = "Science";
        public bool ValidGrade;
        
    }
}