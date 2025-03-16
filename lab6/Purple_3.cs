using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_7{
    public class Purple_3{
        public struct Participant{
            private string _name;
            private string _surname;
            private double[] _marks;
            private int[] _places;
            private int _currentMark = 0;

            public string Name => _name;
            public string Surname => _surname;
            public double[] Marks{
                get{
                    if (_marks == null) return null;
                    double[] copy = new double[_marks.Length];
                    Array.Copy(_marks, copy, _marks.Length);
                    return copy;
                }
            }
            public int[] Places{
                get{
                    if (_places == null) return null;
                    int[] copy = new int[_places.Length];
                    Array.Copy(_places, copy, _places.Length);
                    return copy;
                }
            }

            public int Score{
                get{
                    if (_places == null) return 0;
                    int sum = 0;
                    for (int i = 0; i < _places.Length; i++){
                        sum += _places[i];
                    }
                    return sum;
                }
            }

            public Participant(string name, string surname){
                _name = name;
                _surname = surname;
                _marks = new double[7]{0, 0, 0, 0, 0, 0, 0};
                _places = new int[7]{0, 0, 0, 0, 0, 0, 0};
            }

            public void Evaluate(double result){
                if (_marks == null) return;

                if (_currentMark < _marks.Length){
                    _marks[_currentMark] = result;
                    _currentMark++;
                }
            }

            public static void SetPlaces(Participant[] participants){
                if (participants == null) return;

                for (int judje = 0; judje < 7; judje++){
                    for (int i = 1; i < participants.Length; i++){
                        if (participants[i-1]._marks == null || participants[i]._marks == null) continue;
                        int k = i, j = k - 1;
                        while (j >= 0){
                            if (participants[j].Marks[judje] < participants[k].Marks[judje]){
                                Participant tmp = participants[k];
                                participants[k] = participants[j];
                                participants[j] = tmp;
                            }
                            k = j;
                            j--;
                        }
                    }
                    //participants - отсортирован относительно i-го судьи

                    for (int place = 0; place < participants.Length; place++){
                        if (participants[place]._places != null){
                            participants[place]._places[judje] = place + 1;
                        } 
                    }
                }
                // for (int i = 1; i < participants.Length; i++){
                //     int k = i, j = k-1;
                //     while (j >= 0){
                //         if (participants[j].Places[6] < participants[k].Places[6]){
                //             Participant tmp = participants[k];
                //             participants[k] = participants[j];
                //             participants[j] = tmp;
                //         }
                //         k = j;
                //         j--;
                //     }
                //     }
            }

            public static void Sort(Participant[] array){
                if (array == null) return;
                
                for (int i = 1; i < array.Length; i++){
                    int k = i, j = k - 1;
                    if (array[i]._places == null || array[k]._places == null || array[j]._places == null){
                        continue;
                    }
                    while (j >= 0){
                        if (array[i]._places == null || array[k]._places == null || array[j]._places == null){
                            continue;
                        }
                        if (array[j]._places.Sum() > array[k]._places.Sum()){
                            Participant tmp = array[k];
                            array[k] = array[j];
                            array[j] = tmp;
                        }
                        else if (array[j]._places.Sum() == array[k]._places.Sum()){
                            if (array[j]._places.Min() > array[k]._places.Min()){
                                Participant tmp = array[k];
                                array[k] = array[j];
                                array[j] = tmp;
                            }
                            if (array[j]._marks == null || array[k]._marks == null){
                                continue;
                            }
                            else if (array[j]._places.Min() == array[k]._places.Min() && array[j]._marks.Sum() < array[k]._marks.Sum()){
                                Participant tmp = array[k];
                                array[k] = array[j];
                                array[j] = tmp;
                            }
                        }
                        k = j;
                        j--;
                    }
                }
            }   

            public void Print() {
                if (_marks == null) return;
                Console.Write(_name + "\t");
                Console.Write(Score + "\t");
                Console.Write(_places.Min() + "\t");
                Console.Write($"{_marks.Sum():F2}\t");
                Console.WriteLine();
            }
            private static int MaxVal(int[] arr){
                int maxVal = int.MinValue;
                for (int i = 0; i < arr.Length; i++){
                    if (arr[i] > maxVal){
                        maxVal = arr[i];
                    }
                }
                return maxVal;
        }
        }

        public abstract class Skating{
            private Participant[] _participants;
            protected double[] _moods;
            private int _participant_index;
            public Participant[] Participants => _participants;
            // public Participant[] Participants{
            //     get{
            //         if (_participants == null){
            //             return null;
            //         }
            //         Participant[] Copy = new Participant[_participants.Length];
            //         Array.Copy(_participants, Copy, _participants.Length);
            //         return Copy;
            //     }
            // }

            public double[] Moods{
                get{
                    if (_moods == null){
                    return null;
                }
                double[] Copy = new double[_moods.Length];
                Array.Copy(_moods, Copy, _moods.Length);
                return Copy;
                }
            }

            protected abstract void ModificateMood();

            public Skating(double[] moods){
                if (moods == null || moods.Length != 7){
                    return;
                }
                _moods = moods;
                ModificateMood();
                _participant_index = 0;
                _participants = new Participant[0];
            }

            public void Evaluate(double[] marks){
                if (marks == null || _participants == null || _moods == null || _participant_index >= _participants.Length){
                    return;
                }

                for (int i = 0; i < marks.Length; i++){
                    _participants[_participant_index].Evaluate(marks[i] * _moods[i]);
                }
                _participant_index++;
            }

            public void Add(Participant participant){
                if (_participants == null){
                    return;
                }
                Participant[] New = new Participant[_participants.Length+1];
                Array.Copy(_participants, New, _participants.Length);
                New[New.Length-1] = participant;
                _participants = New;
            }

            public void Add(Participant[] participants){
                if (participants == null || _participants == null){
                    return;
                }
                Participant[] New = new Participant[_participants.Length+participants.Length];
                Array.Copy(_participants, New, _participants.Length);
                for (int i = 0; i < participants.Length; i++){
                    New[_participants.Length + i] = participants[i];
                }
                _participants = New;
            }   

            public void Print(){
                PrintArray(_participants);
                PrintArray(_moods);
            }         
        }

        public class FigureSkating: Skating{
            public FigureSkating(double[] moods): base(moods){}
            protected override void ModificateMood(){
                if (_moods == null || _moods.Length == 0){
                    return;
                }
                for (int i = 0; i < _moods.Length; i++){
                    _moods[i] += (i+1)/10.0;
                }
            }
        }

        public class IceSkating: Skating{
            public IceSkating(double[] moods): base(moods){}
            protected override void ModificateMood(){
                if (_moods == null || _moods.Length == 0){
                    return;
                }

                for (int i = 0; i < _moods.Length; i++){
                    _moods[i] += _moods[i]*((i+1)/100.0);
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
                Console.Write($"{array[i].Score} ");
            Console.WriteLine();
        }
    

    }
}