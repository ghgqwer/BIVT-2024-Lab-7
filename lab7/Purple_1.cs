using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_7{
    public class Purple_1{
        public class Participant{
            private string _name;
            private string _surname;
            private double[] _coefs;
            private int[,] _marks;
            private int _jumpCount;

            private double _totalScore;
            public string Name => _name;
            public string Surname => _surname;
            public double[] Coefs{
                get{
                    if (_coefs == null) return null;

                    double[] copy = new double[_coefs.Length];
                    for (int i = 0; i < _coefs.Length; i++){
                        copy[i] = _coefs[i];
                    }
                    
                    return copy;
                }
            }
            public int[,] Marks{
                get{
                    if (_marks == null) return null;

                    int[,] copy = new int[_marks.GetLength(0), _marks.GetLength(1)];
                    for (int i = 0; i < _marks.GetLength(0); i++){
                        for (int j = 0; j < _marks.GetLength(1); j++){
                            copy[i, j] = _marks[i, j];
                        }
                    }
                    return copy;
                }
            }
            public double TotalScore{
                get{
                    if (_marks == null || _coefs == null) return 0;

                    double total = 0;
                    for (int jump = 0; jump < _marks.GetLength(0); jump++){
                        double[] grades = new double[_marks.GetLength(1)];
                        for (int judje = 0; judje < _marks.GetLength(1); judje++){
                            grades[judje] = _marks[jump, judje];
                        }

                        double sum = 0;
                        for (int i = 0; i < grades.Length; i++){
                            sum += grades[i];
                        }
                        sum = sum - grades.Max() - grades.Min();
                        total += sum * _coefs[jump];
                    }
                    return total;
                }
            }
        
            public Participant(string name, string surname){
                _name = name;
                _surname = surname;
                _coefs = new double[4]{2.5, 2.5, 2.5, 2.5};
                _marks = new int[4,7];   
            }
            
            public void SetCriterias(double[] coefs){
                if (coefs == null || _coefs == null) return;
                if (coefs.Length != 4) return;
                Array.Copy(coefs, _coefs, coefs.Length);
            }

            public void Jump(int[] marks){
                if (marks == null || _marks == null) return;

                if (_jumpCount >= 4){
                    return;
                }

                if (marks.Length > 7){
                    for (int j = 0; j < 7; j++){
                    _marks[_jumpCount, j] = marks[j];
                }
                }
                else{
                    for (int j = 0; j < marks.Length; j++){
                        _marks[_jumpCount, j] = marks[j];
                    }
                }
                _jumpCount++;

                // if (_jumpCount == 4){
                //     double total = 0;
                //     for (int jump = 0; jump < _marks.GetLength(0); jump++){
                //         double[] grades = new double[_marks.GetLength(1)];
                //         for (int judje = 0; judje < _marks.GetLength(1); judje++){
                //             grades[judje] = _marks[jump, judje];
                //         }
                //         double minGrade = grades.Min();
                //         double maxGrade = grades.Max();
                //         // Array.Sort(grades);
                //         double sum = 0;
                //         for (int i = 0; i < grades.Length; i++){
                //             sum += grades[i];
                //         }
                //         sum = sum - minGrade - maxGrade;
                //         total += sum * _coefs[jump];
                //     }

                //     _totalScore = total;
                // }
            
            }

            public static void Sort(Participant[] array){
                if (array == null) return;
                
                for (int i = 1; i < array.Length; i++){
                    int k = i, j = k - 1;
                    while(j >= 0){
                        if (array[j].TotalScore < array[k].TotalScore){
                            Participant temp = array[k];
                            array[k] = array[j];
                            array[j] = temp;
                        }
                        k = j;
                        j--;
                    } 
            }
            }

        public void Print(){
            Console.WriteLine($"{_name} {_surname} {TotalScore:F2}");
        }
        }
    
        public class Judge{
        private string _name;
        private int[] _marks;
        private int _mark_index;

        public string Name => _name;

        public Judge(string name, int[] marks){
            if (marks == null){
                return;
            }
            _name = name;
            _marks = new int[marks.Length];
            Array.Copy(marks, _marks, marks.Length);
            _mark_index = 0;

        }

        public int CreateMark(){
            if (_marks == null || _marks.Length == 0){
                return 0;
            }
            if (_mark_index == _marks.Length){
                _mark_index = 0;
            }
            return _marks[_mark_index++];
        }

        public void Print(){
            System.Console.WriteLine(_name);
            PrintArray(_marks);
        }


    }
    
        public class Competition{
            private Judge[] _judges;
            private Participant[] _participants;

            public Judge[] Judges{
                get{
                    if (_judges == null || _judges.Length == 0){
                        return null;
                    }
                    Judge[] Copy = new Judge[_judges.Length];
                    Array.Copy(Copy, _judges, _judges.Length);
                    return Copy;
                }
            }

            public Participant[] Participants{
                get{
                    if (_participants == null || _participants.Length == 0){
                        return null;
                    }
                    Participant[] Copy = new Participant[_participants.Length];
                    Array.Copy(Copy, _participants, _participants.Length);
                    return Copy;
                }
            }

            public Competition(Judge[] judges){
                if (judges == null || judges.Length == 0 || judges.Length != 7){
                    return;
                }
                _judges = new Judge[judges.Length];
                Array.Copy(judges, _judges, judges.Length);

                _participants = new Participant[0];
            }

            public void Evaluate(Participant jumper){
                if (jumper == null || _judges == null){
                    return;
                }
                int[] marks = new int[_judges.Length];
                int markInd = 0;
                for (int i = 0; i < _judges.Length; i++){
                    if (_judges[i] != null){
                        marks[markInd] = _judges[i].CreateMark();
                        markInd++;
                    }
                }

                jumper.Jump(marks);      
            }

            public void Add(Participant participant){
                if (participant == null || _participants == null){
                    return;
                }
                Participant[] New = new Participant[_participants.Length + 1];
                Array.Copy(_participants, New, _participants.Length);
                Evaluate(participant);
                New[New.Length - 1] = participant;
                _participants = New;
            }

            public void Add(Participant[] participants){
                if (participants == null || participants.Length == 0){
                    return;
                }
                Participant[] New = new Participant[_participants.Length + participants.Length];
                Array.Copy(_participants, New, _participants.Length);
                for (int i = _participants.Length; i < New.Length; i++){
                    Evaluate(participants[i - _participants.Length]);
                    New[i] = participants[i - _participants.Length];
                }
                _participants = New;
            }

            public void Sort(){
                Participant.Sort(_participants);
            }

            public void Print(){
                if (_participants != null){
                    for (int i = 0; i < _participants.Length; i++){
                        if (_participants[i] != null){
                            _participants[i].Print();
                        }
                    }
                }
                if (_judges != null){
                    for (int i = 0; i < _judges.Length; i++){
                        if (_judges[i] != null){
                            _judges[i].Print();
                        }
                    }
                }
            }
        }

        public static void PrintArray(int[] array)
        {
            if (array == null || array.Length == 0)
                return;
            for (int i = 0; i < array.Length; i++)
                Console.Write($"{array[i]} ");
            Console.WriteLine();
        }

        public static void PrintArray(double[] array)
        {
            if (array == null || array.Length == 0)
                return;
            for (int i = 0; i < array.Length; i++)
                Console.Write($"{array[i]} ");
            Console.WriteLine();
        }

        public static void PrintArray(Participant[] array)
        {
            if (array == null || array.Length == 0)
                return;
            for (int i = 0; i < array.Length; i++)
                Console.Write($"{array[i].TotalScore} ");
            Console.WriteLine();
        }
    

    }

    

}