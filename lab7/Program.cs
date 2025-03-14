using System;
using System.Linq;

namespace Lab_7
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Test_Purple_1_2();
            Test_Purple_2_2();
            Test_Purple_2_3();
            //Test_Purple_3_3();
        }

        static void Test_Purple_1_2()
        {   
            string[,] namesTask_1 = new string[,]
            {
                { "Дарья", "Тихонова" },
                { "Александр", "Козлов" },
                { "Никита", "Павлов" },
                { "Юрий", "Луговой" },
                { "Юрий", "Степанов" },
                { "Мария", "Луговая" },
                { "Виктор", "Жарков" },
                { "Марина", "Иванова" },
                { "Марина", "Полевая" },
                { "Максим", "Тихонов" }
            };

            double[][] coefficientsTask_1 = new double[][]
            {
            new double[]{ 2.58, 2.90, 3.04, 3.43 },
            new double[]{ 2.95, 2.63, 3.16, 2.89 },
            new double[]{ 2.56, 3.40, 2.91, 2.69 },
            new double[]{ 2.86, 2.90, 3.19, 3.14 },
            new double[]{ 2.81, 2.64, 2.76, 3.20 },
            new double[]{ 2.74, 3.30, 2.94, 3.27 },
            new double[]{ 2.57, 2.79, 2.71, 3.46 },
            new double[]{ 3.09, 2.67, 2.90, 3.50 },
            new double[]{ 2.65, 3.47, 3.11, 3.39 },
            new double[]{ 3.14, 3.46, 2.96, 2.76 }
            };

            int[,,] scores = new int[10, 4, 7]
            {
            {
                { 3, 4, 1, 2, 1, 3, 1 },
                { 5, 3, 4, 3, 3, 3, 3 },
                { 2, 4, 1, 5, 6, 1, 2 },
                { 6, 4, 3, 2, 2, 1, 1 },
                 },
                {
                { 3, 5, 4, 4, 5, 1, 4 },
                { 1, 6, 5, 2, 1, 4, 1 },
                { 6, 2, 4, 1, 2, 6, 5 },
                { 6, 5, 2, 2, 4, 3, 4 },
                 },
                {
                { 1, 1, 3, 5, 5, 5, 2 },
                { 4, 1, 1, 2, 2, 2, 5 },
                { 5, 2, 3, 3, 2, 2, 3 },
                { 3, 1, 3, 4, 2, 4, 5 },
                 },
                {
                { 3, 3, 5, 2, 1, 2, 4 },
                { 5, 5, 4, 2, 3, 2, 2 },
                { 6, 3, 1, 2, 2, 6, 6 },
                { 5, 1, 6, 6, 3, 2, 5 },
                 },
                {
                { 4, 3, 5, 4, 5, 1, 1 },
                { 5, 3, 4, 2, 1, 1, 2 },
                { 2, 2, 4, 2, 6, 3, 4 },
                { 3, 2, 1, 3, 5, 1, 5 },
                 },
                {
                { 6, 5, 5, 4, 2, 6, 4 },
                { 5, 4, 3, 2, 4, 6, 1 },
                { 1, 1, 3, 4, 4, 1, 6 },
                { 3, 1, 5, 1, 4, 3, 1 },
                 },
                {
                { 4, 6, 1, 4, 5, 3, 4 },
                { 1, 2, 3, 1, 5, 4, 3 },
                { 3, 6, 2, 3, 1, 6, 3 },
                { 3, 3, 6, 6, 3, 6, 6 },
                 },
                {
                { 6, 5, 3, 2, 6, 5, 3 },
                { 5, 4, 4, 2, 1, 2, 4 },
                { 4, 2, 2, 5, 1, 3, 1 },
                { 6, 5, 6, 1, 6, 3, 3 },
                 },
                {
                   { 3, 6, 3, 5, 4, 2, 3 },
                   { 4, 6, 1, 4, 2, 1, 5 },
                   { 1, 1, 3, 1, 3, 2, 6 },
                   { 1, 4, 4, 6, 6, 2, 5 },
                 },
                {
                    { 3, 3, 1, 4, 5, 6, 2 },
                    { 6, 4, 5, 4, 2, 3, 1 },
                    { 3, 3, 4, 2, 2, 3, 6 },
                    { 5, 1, 5, 5, 1, 3, 4 },
                }
            };

            Purple_1.Participant[] participants = new Purple_1.Participant[namesTask_1.GetLength(0)];

            Purple_1.Judge[] judges = new Purple_1.Judge[7];

            for (int i = 0; i < 7; i++)
            {
                int[] marks = new int[40];    
                for (int j = 0, iter = 0; j < 10; j++)
                {
                    for (int k = 0; k < 4; k++)
                    {
                        marks[iter++] = scores[j, k, i];
                    }
                }
                judges[i] = new Purple_1.Judge($"Judge No{i + 1}", marks);
            }

            Purple_1.Competition competiton = new Purple_1.Competition(judges);

            for (int i = 0; i < namesTask_1.GetLength(0); i++)
            {
                Purple_1.Participant part = new Purple_1.Participant(namesTask_1[i, 0], namesTask_1[i, 1]);
                part.SetCriterias(coefficientsTask_1[i]);
                for (int j = 0; j < 4; j++)
                    competiton.Evaluate(part);
                participants[i] = part;
            }
            competiton.Add(participants);
            
            competiton.Sort();
            competiton.Print();
        }

    static void Test_Purple_2_2()
        {
            string[,] namesTask_2 = new string[,]
            {
                { "Оксана", "Сидорова" },
                { "Полина", "Полевая" },
                { "Дмитрий", "Полевой" },
                { "Евгения", "Распутина" },
                { "Савелий", "Луговой" },
                { "Евгения", "Павлова" },
                { "Егор", "Свиридов" },
                { "Степан", "Свиридов" },
                { "Анастасия", "Козлова" },
                { "Светлана", "Свиридова" },
            };
            int[] distancesTask_2 = new int[] { 135, 191, 147, 115, 112, 151, 186, 166, 112, 197 };
            int[][] marksTask_2 = new int[][]
            {
                new int[]{ 15, 1, 3, 9, 15 },
                new int[]{ 19, 14, 9, 11, 4 },
                new int[]{ 20, 9, 1, 13, 6 },
                new int[]{ 5, 20, 17, 9, 16 },
                new int[]{ 19, 8, 1, 6, 17 },
                new int[]{ 16, 12, 5, 20, 4 },
                new int[]{ 5, 20, 3, 19, 18 },
                new int[]{ 16, 12, 5, 4, 15 },
                new int[]{ 7, 4, 19, 11, 12 },
                new int[]{ 14, 3, 6, 17, 1 },
            };
            Purple_2.ProSkiJumping proSkiJumping = new Purple_2.ProSkiJumping();

            for (int i = 0; i < distancesTask_2.Length; i++)
            {
                Purple_2.Participant part = new Purple_2.Participant(namesTask_2[i, 0], namesTask_2[i, 1]);
                proSkiJumping.Add(part);
                proSkiJumping.Jump(distancesTask_2[i], marksTask_2[i]);
            }
            proSkiJumping.Print();

            proSkiJumping.Sort();

            proSkiJumping.Print();

        }

         static void Test_Purple_2_3()
        {
            string[,] namesTask_2 = new string[,]
            {
                { "Оксана", "Сидорова" },
                { "Полина", "Полевая" },
                { "Дмитрий", "Полевой" },
                { "Евгения", "Распутина" },
                { "Савелий", "Луговой" },
                { "Евгения", "Павлова" },
                { "Егор", "Свиридов" },
                { "Степан", "Свиридов" },
                { "Анастасия", "Козлова" },
                { "Светлана", "Свиридова" },
            };
            int[] distancesTask_2 = new int[] { 135, 191, 147, 115, 112, 151, 186, 166, 112, 197 };
            int[][] marksTask_2 = new int[][]
            {
                new int[]{ 15, 1, 3, 9, 15 },
                new int[]{ 19, 14, 9, 11, 4 },
                new int[]{ 20, 9, 1, 13, 6 },
                new int[]{ 5, 20, 17, 9, 16 },
                new int[]{ 19, 8, 1, 6, 17 },
                new int[]{ 16, 12, 5, 20, 4 },
                new int[]{ 5, 20, 3, 19, 18 },
                new int[]{ 16, 12, 5, 4, 15 },
                new int[]{ 7, 4, 19, 11, 12 },
                new int[]{ 14, 3, 6, 17, 1 },
            };
            Purple_2.JuniorSkiJumping juniorSkiJumping = new Purple_2.JuniorSkiJumping();

            for (int i = 0; i < distancesTask_2.Length; i++)
            {
                Purple_2.Participant part = new Purple_2.Participant(namesTask_2[i, 0], namesTask_2[i, 1]);
                juniorSkiJumping.Add(part);
                juniorSkiJumping.Jump(distancesTask_2[i], marksTask_2[i]);
            }

            juniorSkiJumping.Print();

            juniorSkiJumping.Sort();

            juniorSkiJumping.Print();


        }

        static void Test_Purple_3_1()
        {
            string[,] namesTask_3 = new string[,]
            {
                { "Виктор", "Полевой" },
                { "Алиса", "Козлова" },
                { "Ярослав", "Зайцев" },
                { "Савелий", "Кристиан" },
                { "Алиса", "Козлова" },
                { "Алиса", "Луговая" },
                { "Александр", "Петров" },
                { "Мария", "Смирнова" },
                { "Полина", "Сидорова" },
                { "Татьяна", "Сидорова" },
            };
            double[,] marksTask_3 = new double[,]
            {
                { 5.93, 5.44, 1.2, 0.28, 1.57, 1.86, 5.89 },
                { 1.68, 3.79, 3.62, 2.76, 4.47, 4.26, 5.79 },
                { 2.93, 3.1, 5.46, 4.88, 3.99, 4.79, 5.56 },
                { 4.2, 4.69, 3.9, 1.67, 1.13, 5.66, 5.4 },
                { 3.27, 2.43, 0.9, 5.61, 3.12, 3.76, 3.73 },
                { 0.75, 1.13, 5.43, 2.07, 2.68, 0.83, 3.68 },
                { 3.78, 3.42, 3.84, 2.19, 1.2, 2.51, 3.51 },
                { 1.35, 3.4, 1.85, 2.02, 2.78, 3.23, 3.03 },
                { 0.55, 5.93, 0.75, 5.15, 4.35, 1.51, 2.77 },
                { 3.86, 0.19, 0.46, 5.14, 5.37, 0.94, 0.84 },
            };
            Purple_3.Participant[] participants = new Purple_3.Participant[10];

            for (int i = 0; i < participants.Length; i++)
            {
                Purple_3.Participant part = new Purple_3.Participant(namesTask_3[i, 0], namesTask_3[i, 1]);
                for (int j = 0; j < marksTask_3.GetLength(1); j++)
                {
                    part.Evaluate(marksTask_3[i, j]);
                }
                participants[i] = part;
            }
            Purple_3.Participant.SetPlaces(participants);
            Purple_3.Participant.Sort(participants);

            for (int i = 0; i < participants.Length; i++)
            {

                participants[i].Print();
            }
        }

        static void Test_Purple_3_2()
        {
            string[,] namesTask_3 = new string[,]
            {
                { "Виктор", "Полевой" },
                { "Алиса", "Козлова" },
                { "Ярослав", "Зайцев" },
                { "Савелий", "Кристиан" },
                { "Алиса", "Козлова" },
                { "Алиса", "Луговая" },
                { "Александр", "Петров" },
                { "Мария", "Смирнова" },
                { "Полина", "Сидорова" },
                { "Татьяна", "Сидорова" },
            };
            double[][] marksTask_3 = new double[][]
            {
                new double[] { 5.93, 5.44, 1.2, 0.28, 1.57, 1.86, 5.89 },
                new double[] { 1.68, 3.79, 3.62, 2.76, 4.47, 4.26, 5.79 },
                new double[]  { 2.93, 3.1, 5.46, 4.88, 3.99, 4.79, 5.56 },
                new double[] { 4.2, 4.69, 3.9, 1.67, 1.13, 5.66, 5.4 },
                new double[] { 3.27, 2.43, 0.9, 5.61, 3.12, 3.76, 3.73 },
                new double[] { 0.75, 1.13, 5.43, 2.07, 2.68, 0.83, 3.68 },
                new double[] { 3.78, 3.42, 3.84, 2.19, 1.2, 2.51, 3.51 },
                new double[] { 1.35, 3.4, 1.85, 2.02, 2.78, 3.23, 3.03 },
                new double[] { 0.55, 5.93, 0.75, 5.15, 4.35, 1.51, 2.77 },
                new double[]  { 3.86, 0.19, 0.46, 5.14, 5.37, 0.94, 0.84 },
            };

            double[] moods_3 = new double[] { 1, 2, 3, 4, 5, 6, 7 };

            Purple_3.FigureSkating figureSkating = new Purple_3.FigureSkating(moods_3);

            Purple_3.Participant[] participants = new Purple_3.Participant[namesTask_3.GetLength(0)];

            for (int i = 0; i < participants.Length; i++)
            {
                Purple_3.Participant part = new Purple_3.Participant(namesTask_3[i, 0], namesTask_3[i, 1]);
                figureSkating.Add(part);
                figureSkating.Evaluate(marksTask_3[i]);
            }
        }

        static void Test_Purple_3_3()
        {
            string[,] namesTask_3 = new string[,]
            {
                { "Виктор", "Полевой" },
                { "Алиса", "Козлова" },
                { "Ярослав", "Зайцев" },
                { "Савелий", "Кристиан" },
                { "Алиса", "Козлова" },
                { "Алиса", "Луговая" },
                { "Александр", "Петров" },
                { "Мария", "Смирнова" },
                { "Полина", "Сидорова" },
                { "Татьяна", "Сидорова" },
            };
            double[][] marksTask_3 = new double[][]
            {
                new double[] { 5.93, 5.44, 1.2, 0.28, 1.57, 1.86, 5.89 },
                new double[] { 1.68, 3.79, 3.62, 2.76, 4.47, 4.26, 5.79 },
                new double[]  { 2.93, 3.1, 5.46, 4.88, 3.99, 4.79, 5.56 },
                new double[] { 4.2, 4.69, 3.9, 1.67, 1.13, 5.66, 5.4 },
                new double[] { 3.27, 2.43, 0.9, 5.61, 3.12, 3.76, 3.73 },
                new double[] { 0.75, 1.13, 5.43, 2.07, 2.68, 0.83, 3.68 },
                new double[] { 3.78, 3.42, 3.84, 2.19, 1.2, 2.51, 3.51 },
                new double[] { 1.35, 3.4, 1.85, 2.02, 2.78, 3.23, 3.03 },
                new double[] { 0.55, 5.93, 0.75, 5.15, 4.35, 1.51, 2.77 },
                new double[]  { 3.86, 0.19, 0.46, 5.14, 5.37, 0.94, 0.84 },
            };

            double[] moods_3 = new double[] { 1, 2, 3, 4, 5, 6, 7 };

            Purple_3.IceSkating iceSkating = new Purple_3.IceSkating(moods_3);

            Purple_3.Participant[] participants = new Purple_3.Participant[namesTask_3.GetLength(0)];

            for (int i = 0; i < participants.Length; i++)
            {
                Purple_3.Participant part = new Purple_3.Participant(namesTask_3[i, 0], namesTask_3[i, 1]);
                iceSkating.Add(part);
                iceSkating.Evaluate(marksTask_3[i]);
            }
        }


        }
}
    //    Purple_1.Participant[] participants1 = new Purple_1.Participant[]
    //     {
    //         new Purple_1.Participant("Иван", "Иванов"),
    //         new Purple_1.Participant("Петр", "Петров"),
    //         new Purple_1.Participant("Сергей", "Сергеев")
    //     };

    //     participants1[0].SetCriterias(new double[] { 3.0, 3.2, 3.1, 3.3 });
    //     participants1[1].SetCriterias(new double[] { 2.8, 3.0, 3.2, 3.1 });
    //     participants1[2].SetCriterias(new double[] { 3.1, 3.0, 2.9, 3.2 });

    //     participants1[0].Jump(new int[] { 5, 6, 5, 4, 5, 6, 5 }); 
    //     participants1[0].Jump(new int[] { 6, 6, 5, 5, 6, 6, 5 });
    //     participants1[0].Jump(new int[] { 5, 5, 4, 5, 5, 5, 4 });
    //     participants1[0].Jump(new int[] { 6, 6, 6, 5, 6, 6, 5 });

    //     participants1[1].Jump(new int[] { 4, 5, 4, 5, 4, 5, 4 });
    //     participants1[1].Jump(new int[] { 5, 5, 5, 5, 5, 5, 5 });
    //     participants1[1].Jump(new int[] { 6, 6, 6, 6, 6, 6, 6 });
    //     participants1[1].Jump(new int[] { 5, 5, 5, 5, 5, 5, 5 });

    //     participants1[2].Jump(new int[] { 6, 6, 6, 6, 6, 6, 6 });
    //     participants1[2].Jump(new int[] { 5, 5, 5, 5, 5, 5, 5 });
    //     participants1[2].Jump(new int[] { 4, 4, 4, 4, 4, 4, 4 });
    //     participants1[2].Jump(new int[] { 3, 3, 3, 3, 3, 3, 3 });

    //     Purple_1.Participant.Sort(participants1);

    //     System.Console.WriteLine("Purple1");

    //     Console.WriteLine("Итоговая таблица:");
    //     for (int i = 1; i <= participants1.Length; i++)
    //     {
    //         Console.WriteLine($"{participants1[i-1].Surname} {participants1[i-1].Name}: " +
    //             $"Итоговый результат: {i}. {participants1[i-1].TotalScore:F2}");
    //     }
    //     System.Console.WriteLine();

    //     System.Console.WriteLine("Purple2");
    //     Purple_2.Participant p1 = new Purple_2.Participant("Vadim1", "Shubin");
    //     p1.Jump(120, new int[] { 1, 1, 1, 1, 1 });
    //     Purple_2.Participant p2 = new Purple_2.Participant("Vadim2", "Shubin");
    //     p2.Jump(130, new int[] { 1, 1, 1, 1, 1 });
    //     Purple_2.Participant p3 = new Purple_2.Participant("Vadim3", "Shubin");
    //     p3.Jump(119, new int[] { 1, 1, 1, 1, 1 });

    //     Purple_2.Participant[] participantsP2 = new Purple_2.Participant[] { p1, p2, p3 };

    //     Console.WriteLine("До сортировки:");
    //     foreach (Purple_2.Participant member in participantsP2)
    //     {
    //         Console.WriteLine($"{member.Name} - {member.Result}");
    //     }

    //     Purple_2.Participant.Sort(participantsP2);

    //     Console.WriteLine("\nПосле сортировки:");
    //     foreach (Purple_2.Participant member in participantsP2)
    //     {
    //         Console.WriteLine($"{member.Name} - {member.Result}");
    //     }
    //     System.Console.WriteLine();

    //     //3
    //     System.Console.WriteLine("Purple_3");
    //     Purple_3.Participant[] participants = new Purple_3.Participant[]
    //         {
    //             new Purple_3.Participant("Виктор", "Полевой"),
    //             new Purple_3.Participant("Алиса", "Козлова"),
    //             new Purple_3.Participant("Ярослав", "Зайцев"),
    //             new Purple_3.Participant("Савелий", "Кристиан"),
    //             new Purple_3.Participant("Алиса", "Козлова"), 
    //             new Purple_3.Participant("Алиса", "Луговая"),
    //             new Purple_3.Participant("Александр", "Петров"),
    //             new Purple_3.Participant("Мария", "Смирнова"),
    //             new Purple_3.Participant("Полина", "Сидорова"),
    //             new Purple_3.Participant("Татьяна", "Сидорова")
    //         };

    //         participants[0].Evaluate(5.93); participants[0].Evaluate(5.44); participants[0].Evaluate(1.20);
    //         participants[0].Evaluate(0.28); participants[0].Evaluate(1.57); participants[0].Evaluate(1.86);
    //         participants[0].Evaluate(5.89);

    //         participants[1].Evaluate(1.68); participants[1].Evaluate(3.79); participants[1].Evaluate(3.62);
    //         participants[1].Evaluate(2.76); participants[1].Evaluate(4.47); participants[1].Evaluate(4.26);
    //         participants[1].Evaluate(5.79);

    //         participants[2].Evaluate(2.93); participants[2].Evaluate(3.10); participants[2].Evaluate(5.46);
    //         participants[2].Evaluate(4.88); participants[2].Evaluate(3.99); participants[2].Evaluate(4.79);
    //         participants[2].Evaluate(5.56);

    //         participants[3].Evaluate(4.20); participants[3].Evaluate(4.69); participants[3].Evaluate(3.90);
    //         participants[3].Evaluate(1.67); participants[3].Evaluate(1.13); participants[3].Evaluate(5.66);
    //         participants[3].Evaluate(5.40);

    //         participants[4].Evaluate(3.27); participants[4].Evaluate(2.43); participants[4].Evaluate(0.90);
    //         participants[4].Evaluate(5.61); participants[4].Evaluate(3.12); participants[4].Evaluate(3.76);
    //         participants[4].Evaluate(3.73);

    //         participants[5].Evaluate(0.75); participants[5].Evaluate(1.13); participants[5].Evaluate(5.43);
    //         participants[5].Evaluate(2.07); participants[5].Evaluate(2.68); participants[5].Evaluate(0.83);
    //         participants[5].Evaluate(3.68);

    //         participants[6].Evaluate(3.78); participants[6].Evaluate(3.42); participants[6].Evaluate(3.84);
    //         participants[6].Evaluate(2.19); participants[6].Evaluate(1.20); participants[6].Evaluate(2.51);
    //         participants[6].Evaluate(3.51);

    //         participants[7].Evaluate(1.35); participants[7].Evaluate(3.40); participants[7].Evaluate(1.85);
    //         participants[7].Evaluate(2.02); participants[7].Evaluate(2.78); participants[7].Evaluate(3.23);
    //         participants[7].Evaluate(3.03);

    //         participants[8].Evaluate(0.55); participants[8].Evaluate(5.93); participants[8].Evaluate(0.75);
    //         participants[8].Evaluate(5.15); participants[8].Evaluate(4.35); participants[8].Evaluate(1.51);
    //         participants[8].Evaluate(2.77);

    //         participants[9].Evaluate(3.86); participants[9].Evaluate(0.19); participants[9].Evaluate(0.46);
    //         participants[9].Evaluate(5.14); participants[9].Evaluate(5.37); participants[9].Evaluate(0.94);
    //         participants[9].Evaluate(0.84);

    //         Purple_3.Participant.SetPlaces(participants);

    //         Purple_3.Participant.Sort(participants);

    //         Console.WriteLine("Имя\t\tСумма мест\tНаивысшее место\tСумма очков");
    //         foreach (var p in participants)
    //         {
    //             p.Print();
    //         }
    //         System.Console.WriteLine();

    //     //4
    //     System.Console.WriteLine("Purple4");
    //     Purple_4.Sportsman sportsman1 = new Purple_4.Sportsman("Иван", "Иванов");
    //     sportsman1.Run(28.7);
    //     Purple_4.Sportsman sportsman2 = new Purple_4.Sportsman("Петр", "Петров");
    //     sportsman2.Run(30.5);
    //     Purple_4.Sportsman[] list = new Purple_4.Sportsman[2]{sportsman1, sportsman2};
    //     Purple_4.Group group1 = new Purple_4.Group("Группа 1");
    //     // group1.Add(sportsman1);
    //     // group1.Add(sportsman2);
    //     group1.Add(list);
    //     group1.Sort();

    //     Purple_4.Sportsman sportsman3 = new Purple_4.Sportsman("Сергей", "Сергеев");
    //     sportsman3.Run(29.8);
    //     Purple_4.Sportsman sportsman4 = new Purple_4.Sportsman("Алексей", "Алексеев");
    //     sportsman4.Run(27.3);

    //     Purple_4.Group group2 = new Purple_4.Group("Группа 2");
    //     group2.Add(sportsman3);
    //     group2.Add(sportsman4);
    //     group2.Sort();

    //     Purple_4.Group finalGroup = Purple_4.Group.Merge(group1, group2);

    //     Console.WriteLine($"Группа: {finalGroup.Name}");
    //     foreach (var sportsman in finalGroup.Sportsmen)
    //     {
    //         Console.WriteLine($"{sportsman.Surname} {sportsman.Name}: {sportsman.Time} сек");
    //     }
    //     System.Console.WriteLine();

    //     //5
    //     System.Console.WriteLine("Purple5");
    //     Purple_5.Research research = new Purple_5.Research("Опрос о Японии");
    //     research.Add(new string[] { "Тануки", "", "" });
    //     research.Add(new string[] { "Кошка", "Амбициозность", "Аниме" });
    //     research.Add(new string[] { "Серау", "Скромность", "Фудзияма" });
    //     research.Add(new string[] { "Коала", "Внимательность", "Кимоно" });
    //     research.Add(new string[] { "Коала", "Целеустремленность", "Самурай" });
    //     research.Add(new string[] { "Панда", "Проницательность", "Манга" });
    //     research.Add(new string[] { "Серау", "Скромность", "Суши" });
    //     research.Add(new string[] { "Макака", "Амбициозность", "" });
    //     research.Add(new string[] { "Сима энага", "Внимательность", "Фудзияма" });
    //     research.Add(new string[] { "Панда", "Уважительность", "Фудзияма" });
    //     research.Add(new string[] { "Тануки", "Скромность", "Манга" });
    //     research.Add(new string[] { "Тануки", "Проницательность", "Сакура" });
    //     research.Add(new string[] { "Тануки", "Целеустремленность", "Кимоно" });
    //     research.Add(new string[] { "Кошка", "Дружелюбность", "Манга" });
    //     research.Add(new string[] { "Тануки", "Проницательность", "" });
    //     research.Add(new string[] { "Сима энага", "Проницательность", "Самурай" });
    //     research.Add(new string[] { "Кошка", "Целеустремленность", "" });
    //     research.Add(new string[] { "Сима энага", "Внимательность", "Фудзияма" });
    //     research.Add(new string[] { "", "Амбициозность", "Сакура" });
    //     research.Add(new string[] { "Коала", "Проницательность", "Самурай" });
    //     research.Print();