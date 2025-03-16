using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_7{
    public class Purple_2{
        public struct Participant{
            private const int tramplin = 120;
            private const int deafultPoints = 60;
            private const int extraPoints = 2;
            private string _name;
            private string _surname;
            private int _distance;
            private int[] _marks;
            private int _result;

            public string Name => _name;
            public string Surname => _surname;
            public int Distance => _distance;
            public int[] Marks{
                get{
                    if (_marks == null) return null;
                    int[] copy = new int[_marks.Length];
                    Array.Copy(_marks, copy, _marks.Length);
                    return copy;
                }
            }

            public int Result => _result;
            // public int Result {
            //     get{
                // if (_marks == null) return 0;
                // int sum = 0;

                // int maxI = 0, minI = 1;
                // for (int i = 0; i < _marks.Length; i++){
                //     if (_marks[i] > _marks[maxI]){
                //         maxI = i;
                //     }
                //     if (_marks[i] < _marks[minI]){
                //         minI = i;
                //     }
                // }
                
                // for (int i = 0; i < _marks.Length; i++){
                //     if (i != maxI && i != minI){
                //         sum += _marks[i];
                //     }
                // }

                // int points = deafultPoints;
                // if (_distance >= _target){
                //     points += 60;
                // }
                // if (_distance >= tramplin)
                // {
                //     points += (_distance - tramplin) * extraPoints;
                // }
                // else
                // {
                //     points -= (tramplin - _distance) * extraPoints;
                //     if (points < 0) points = 0;
                // }
                // sum += points;
                // return sum;
                // }
            // }

            public Participant(string name, string surname){
                _name = name;
                _surname = surname;
                _marks = new int[5]{0, 0, 0, 0, 0};
                _result = 0;

            }

            public void Jump(int distance, int[] marks, int target){
                if (marks == null || _marks == null) return;

                if (marks.Length != 5){
                    return;
                }

                for (int i = 0; i < marks.Length; i++){
                    if (marks[i] < 0 || marks[i] > 20){
                        return;
                    }
                }
                _distance = distance;
                Array.Copy(marks, _marks, marks.Length);

                //считаем сумму

                if (_marks == null) return;

                int maxI = 0, minI = 1;
                for (int i = 0; i < _marks.Length; i++){
                    if (_marks[i] > _marks[maxI]){
                        maxI = i;
                    }
                    if (_marks[i] < _marks[minI]){
                        minI = i;
                    }
                }

                _result = 0;
                
                for (int i = 0; i < _marks.Length; i++){
                    if (i != maxI && i != minI){
                        _result += _marks[i];
                    }
                }

                _result += deafultPoints + (_distance - target) * extraPoints;

                if (_result < 0) _result = 0;


                // int points = deafultPoints;
                // if (_distance >= target)
                // {
                //     points += (_distance - target) * extraPoints;
                // }
                // else
                // {
                //     points -= (target - _distance) * extraPoints;
                // }   
                // point - (target - _distance) * extraPoints == point + (_distance - target) * extraPoints
                //     if (points < 0) points = 0;
                // }
                // sum += points;
                // return sum;
                }

            public static void Sort(Participant[] array){
                if (array == null) return;
                
                for (int i = 1; i < array.Length; i++){
                    int k = i, j = k - 1;
                    while (j >= 0){
                        if (array[j].Result < array[k].Result){
                            Participant tmp = array[k];
                            array[k] = array[j];
                            array[j] = tmp;
                        }
                        k = j;
                        j--;
                    }
                }
            }

            public void Print(){
                Console.WriteLine($"Name: {_name}, Surname: {_surname}, Result: {_result}");
            }
        }
    

    public abstract class SkiJumping{
        private string _name;
        private int _standard;
        private Participant[] _participants;
        private int _jumperIndex;

        public string Name => _name;
        public int Standard => _standard;
        public Participant[] Participants{
            get{
                if (_participants == null){
                    return null;
                }
                Participant[] Copy = new Participant[_participants.Length];
                Array.Copy(_participants, Copy, _participants.Length);
                return Copy;
            }
        }
        public SkiJumping(string name, int standard){
            _name = name;
            _standard = standard;
            _participants = new Participant[0];
            _jumperIndex = 0;
        }

        public void Add(Participant participant){
            Participant[] New = new Participant[_participants.Length + 1];
            Array.Copy(_participants, New, _participants.Length);
            New[New.Length - 1] = participant;
            _participants = New;
            }

        public void Add(Participant[] participants){
            Participant[] New = new Participant[_participants.Length + participants.Length];
            Array.Copy(_participants, New, _participants.Length);
            for (int i = 0; i < participants.Length; i++){
                New[_participants.Length + i] = participants[i];
            }
            _participants = New;
        }

        public void Jump(int distance, int[] marks){
            if (marks == null || _participants == null || _jumperIndex >= _participants.Length){
                return;
            }
            _participants[_jumperIndex++].Jump(distance, marks, _standard);
        }

        public void Print(){
            System.Console.WriteLine(_name);
            System.Console.WriteLine(_standard);
            for (int i = 0; i < _participants.Length; i++){
                _participants[i].Print();
            }
        }
        public void Sort(){
            Participant.Sort(_participants);
        }
    }

   

    public class JuniorSkiJumping: SkiJumping{
        public JuniorSkiJumping(): base("100m", 100){}
    }

    public class ProSkiJumping: SkiJumping{
        public ProSkiJumping(): base("150m", 150){}
    }
    

    
    }
}