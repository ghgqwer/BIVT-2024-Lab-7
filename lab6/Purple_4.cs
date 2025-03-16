using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_7{
    public class Purple_4{
        public class Sportsman{
            private string _name;
            private string _surname;
            private double _time;

            public string Name => _name;
            public string Surname => _surname;
            public double Time => _time;

            public Sportsman(string name, string surname){
                _name = name;
                _surname = surname;
                _time = 0;
            }

            public void Run(double time){
                if (_time != 0) return;
                _time = time;
            }

            public static void Sort(Sportsman[] array){
                if (array == null){
                    return;
                }
                for (int i = 1; i < array.Length; i++){
                    int k = i, j = k - 1;
                    while (j >= 0){
                        if (array[j].Time > array[k].Time){
                            Sportsman temp = array[j];
                            array[j] = array[k];
                            array[k] = temp;
                        }
                        k = j;
                        j--;
                    }
                }
            }

            public void Print(){
                System.Console.WriteLine($"{_name} {_surname} {_time:F2}");
            }
        }

        public class SkiMan: Sportsman{
            public SkiMan(string name, string surname) : base(name, surname){}
            
            public SkiMan(string name, string surname, double time): base(name, surname){}
        }

        public class SkiWoman: Sportsman{
            public SkiWoman(string name, string surname) : base(name, surname){}
            
            public SkiWoman(string name, string surname, double time) : base(name, surname){}
        }

        //Создать классы-наследники от Sportsman: SkiMan и SkiWoman.

        public class Group{
            private string _name;
            private Sportsman[] _sportsmen;

            public string Name => _name;
            
            // public Sportsman[] Sportsmen => _sportsmen;
            public Sportsman[] Sportsmen {
                get{
                    if (_sportsmen == null) return null;
                    Sportsman[] copy = new Sportsman[_sportsmen.Length];
                    Array.Copy(_sportsmen, copy, _sportsmen.Length);
                    return copy;
                }
            }

            public Group(string name){
                _name = name;
                _sportsmen = new Sportsman[0];
            }

            public Group(Group otherGroup){
                if (otherGroup._sportsmen == null || otherGroup._name == null) {
                    _sportsmen = new Sportsman[0];
                    _name = "Новая группа";
                }
                else{
                    _name = otherGroup.Name;
                    _sportsmen = otherGroup.Sportsmen;
                }
                
                // _sportsmen = new Sportsman[otherGroup.Sportsmen.Length];
                // Array.Copy(otherGroup.Sportsmen, _sportsmen, otherGroup.Sportsmen.Length);   
            }

            public void Add(Sportsman sportsman){
                if (_sportsmen == null){
                    _sportsmen = new Sportsman[0];
                }

                Array.Resize(ref _sportsmen, _sportsmen.Length + 1);
                _sportsmen[_sportsmen.Length-1] = sportsman;
            }

            public void Add(Sportsman[] sportsmen){
                if (_sportsmen == null || sportsmen == null || sportsmen.Length == 0) return;
                int originalLen = _sportsmen.Length;
                Array.Resize(ref _sportsmen, _sportsmen.Length + sportsmen.Length);
                for (int i = 0; i < sportsmen.Length; i++){
                    _sportsmen[originalLen+i] = sportsmen[i];
                }
            }

            public void Add(Group group){
                if (group._sportsmen == null) return;
                if (_sportsmen == null){
                    _sportsmen = new Sportsman[0];
                }

                for (int i = 0; i < group.Sportsmen.Length; i++){
                    Add(group.Sportsmen[i]);
                }
            }

            public void Sort(){
                Sportsman.Sort(_sportsmen);
            }
            // public void Sort(){
            //     if (_sportsmen == null) return;

            //     for (int i = 1; i < _sportsmen.Length; i++){
            //         int k = i, j = k - 1;
            //         while (j >= 0){
            //             if (_sportsmen[j].Time > _sportsmen[k].Time){
            //                 Sportsman tmp = _sportsmen[j];
            //                 _sportsmen[j] = _sportsmen[k];
            //                 _sportsmen[k] = tmp;
            //             }
            //             k = j;
            //             j--;
            //         }
            //     }
            // }

            public void Split(out Sportsman[] men, out Sportsman[] women){
                int men_count = 0;
                int women_count = 0;

                men = new Sportsman[men_count];
                women = new Sportsman[women_count];

                if (_sportsmen == null || _sportsmen.Length == 0){
                    return;
                }

                for (int i = 0; i < _sportsmen.Length; i++){
                    if (_sportsmen[i] is SkiMan){
                        men_count++;
                    }

                    if (_sportsmen[i] is SkiWoman){
                        women_count++;
                    }
                }

                men = new Sportsman[men_count];
                women = new Sportsman[women_count];

                int manInd = 0, womanInd = 0;
                for (int i = 0; i < _sportsmen.Length; i++){
                    if (_sportsmen[i] is SkiMan){
                        men[manInd++] = _sportsmen[i];
                    }

                    if (_sportsmen[i] is SkiWoman){
                        women[womanInd++] = _sportsmen[i];
                    }
                }
            }

            public void Shuffle(){
                if (_sportsmen == null){
                    return;
                }
                Sportsman[] sortedMen = new Sportsman[0];
                Sportsman[] sortedWomen = new Sportsman[0];

                Split(out sortedMen, out sortedWomen);

                Sportsman.Sort(sortedMen);
                Sportsman.Sort(sortedWomen);
                // PrintArray(sortedMen);
                // PrintArray(sortedWomen);
                // System.Console.WriteLine(sortedMen.Length);
                // System.Console.WriteLine(sortedWomen.Length);
                int manInd = 0, womanInd = 0;

                Sportsman[] sportsmen = new Sportsman[_sportsmen.Length];
                for (int i = 0; i < Math.Min(sortedMen.Length, sortedWomen.Length)*2;){ //*2 чтобы мужчина-женщина
                    sportsmen[i++] = sortedMen[manInd++];
                    sportsmen[i++] = sortedWomen[womanInd++];
                }

                int start = Math.Min(sortedMen.Length, sortedWomen.Length)*2;
                for (int i = start; i < _sportsmen.Length; i++){
                    if (sortedMen.Length > sortedWomen.Length){
                        sportsmen[i] = sortedMen[manInd++];
                    }
                    else{
                        sportsmen[i] = sortedWomen[womanInd++];
                    }
                }

                _sportsmen = sportsmen;
            }

            public static Group Merge(Group group1, Group group2){
                if (group1._sportsmen == null || group2._sportsmen == null) return new Group("Финалисты");

                Group newGroup = new Group("Финалисты");
                int g1 = 0, g2 = 0;
                while (g1 < group1.Sportsmen.Length && g2 < group2.Sportsmen.Length){
                    if (group1.Sportsmen[g1].Time < group2.Sportsmen[g2].Time){
                        newGroup.Add(group1.Sportsmen[g1]);
                        g1++;
                    }
                    else{
                        newGroup.Add(group2.Sportsmen[g2]);
                        g2++;
                    }
                }
                while (g1 < group1.Sportsmen.Length){
                    newGroup.Add(group1.Sportsmen[g1]);
                    g1++;
                }
                while (g2 < group2.Sportsmen.Length){
                    newGroup.Add(group2.Sportsmen[g2]);
                    g2++;
                }
                return newGroup;
            }

            public void Print(){}
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

        public static void PrintArray(Sportsman[] array)
        {
            if (array == null || array.Length == 0)
                return;
            for (int i = 0; i < array.Length; i++)
                Console.WriteLine($"{array[i].Name} {array[i].Surname} {array[i].Time} ");
            Console.WriteLine();
        }
    
    }
}