
using System;
using System.IO;
using System.Collections.Generic;

namespace GradeBook
{
    public delegate void GradeAddedDelegate(object sender, EventArgs args);

    public class NamedObject
    {
        public NamedObject(string name)
        {
            Name = name;
        }
        public string Name
        {
            get;
            set;
        }
    }

    public interface IBook
    {
        void AddGrade(double grade);
        Statistics GetStatistics();
        string Name { get; }
        event GradeAddedDelegate GradeAdded;
    }

    public abstract class Book : NamedObject, IBook
    {
        public Book(string name) : base(name)
        {
        }

        public abstract event GradeAddedDelegate GradeAdded;

        public abstract void AddGrade(double grade);

        public abstract Statistics GetStatistics();
    }
    public class DiskBook : Book
    {
        public DiskBook(string name) : base(name)
        {

        }

        public override event GradeAddedDelegate GradeAdded;

        public override void AddGrade(double grade)
        {
            using (var writer = File.AppendText($"{Name}.txt"))
            {
                writer.WriteLine(grade);
                if(GradeAdded != null)
                {
                    GradeAdded(this, new EventArgs());
                }
            }
            
        }

        public override Statistics GetStatistics()
        {
            throw new NotImplementedException();
        }
    }
    public class InMemoryBook : Book
    {
        public InMemoryBook(string name) : base(name)
        {
            grades = new List<double>();
            Name = name;
        }
        public override void AddGrade(double grade)
        {
            if (grade <= 100 && grade >= 0)
            {
                ValidGrade = true;
                grades.Add(grade);
                if (GradeAdded != null)
                {
                    GradeAdded(this, new EventArgs());
                }

            }
            else
            {
                ValidGrade = false;
                //throw new ArgumentException($"Invalid Grade {nameof(grade)}");
            }
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
        
        public override event GradeAddedDelegate GradeAdded;

        public override Statistics GetStatistics()
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
        public const string CATEGORY = "Science";
        public bool ValidGrade;
        
    }
}