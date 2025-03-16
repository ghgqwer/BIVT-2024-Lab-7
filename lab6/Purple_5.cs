namespace Lab_7{
    public class Purple_5{
        public struct Response{
            private string _animal;
            private string _characterTrait;
            private string _concept;

            public string Animal => _animal;
            public string CharacterTrait => _characterTrait;
            public string Concept => _concept;

            public Response(string animal, string characterTrait, string concept){
                _animal = animal;
                _characterTrait = characterTrait;
                _concept = concept;
            }

            public int CountVotes(Response[] responses, int questionNumber){
                if (responses == null) return 0;

                int count = 0;
                for (int i = 0; i < responses.Length; i++){
                    string ans;
                    if (questionNumber == 1){
                        ans = responses[i]._animal;
                    }
                    else if (questionNumber == 2){
                        ans = responses[i]._characterTrait;
                    }
                    else if (questionNumber == 3){
                        ans = responses[i]._concept;
                    }
                    else{
                        return 0;
                    }

                    if (ans != null && ans != ""){
                        count++;
                    }
                }

                return count;
            }

            public void Print(){
                Console.WriteLine($"1){_animal} 2){_characterTrait} 3){_concept}");
            }
        }
        public struct Research{
                private string _name;
                private Response[] _responses;

                public string Name => _name;

                public Response[] Responses => _responses;
                // public Response[] Responses {
                //     get{
                //         if (_responses == null) return null;
                //         Response[] copy = new Response[_responses.Length];
                //         Array.Copy(_responses, copy, _responses.Length);
                //         return copy;
                //     }
                // }

                public Research(string name){
                    _name = name;
                    _responses = new Response[0];
                }

                public void Add(string[] answers){
                    if (answers == null || answers.Length == 0 || _responses == null) return;
                    
                    if (answers.Length != 3){
                        return;
                    }
                    
                    Console.WriteLine("{0}, {1}, {2}", answers[0], answers[1], answers[2]);

                    Response resp = new Response(answers[0], answers[1], answers[2]);
                    //Response[] newResponses = new Response[_responses.Length + 1];
                    // newResponses[_responses.Length] = resp;
                    // _responses = newResponses;
                    // Response resp = new Response(answers[0], answers[1], answers[2]);
                    Array.Resize(ref _responses, _responses.Length + 1);
                    _responses[_responses.Length - 1] = resp;
                    _responses[_responses.Length - 1].Print();
                }

                public string[] GetTopResponses(int question){
                    if (question < 1 || question > 3 || _responses == null){
                        return null;
                    }

                    string[] ans = new string[_responses.Length];
                    int answerCount = 0;

                    for (int i = 0; i < _responses.Length; i++){
                        string answer = question switch{
                            1 => _responses[i].Animal,
                            2 => _responses[i].CharacterTrait,
                            3 => _responses[i].Concept,
                            _ => ""
                        };

                        if (answer != null && answer != ""){
                            ans[answerCount] = answer;
                            answerCount++;
                        }
                    }

                    // System.Console.WriteLine(answerCount);
                    // foreach (var member in ans){
                    //     System.Console.Write(member+" ");
                    // }


                    // Листнуть вниз там та же логика с моими комментами
                    string[] uniqueAnswers = new string[answerCount];
                    int[] counts = new int[answerCount];
                    int uniqueCount = 0;

                    for (int i = 0; i < answerCount; i++)
                    {
                        bool found = false;
                        for (int j = 0; j < uniqueCount; j++)
                        {
                            if (ans[i] == uniqueAnswers[j])
                            {
                                counts[j]++; // если находим на j месте такой то просто в списке количества на этом же месте ++
                                found = true;
                                break;
                            }
                        }

                        if (!found) //если не нашли в уникальных ответах новое поле + в количествах новое поле = 1 и теперь количество уникальных на 1 больше
                        {
                            uniqueAnswers[uniqueCount] = ans[i];
                            counts[uniqueCount] = 1;
                            uniqueCount++;
                        }
                    }

                    // System.Console.WriteLine("unique");
                    // foreach (var memb in counts){
                    //     System.Console.Write(memb + " ");
                    // }
                    // System.Console.WriteLine();

                    for (int i = 1; i < uniqueCount; i++){
                        int k = i, j = k - 1;
                        while (j >= 0){
                            if (counts[j] < counts[k]){ 
                                string tempAnswer = uniqueAnswers[k];
                                uniqueAnswers[k] = uniqueAnswers[j];
                                uniqueAnswers[j] = tempAnswer;

                                int tempCount = counts[k];
                                counts[k] = counts[j];
                                counts[j] = tempCount;
                            }
                            k = j;
                            j--;
                        }
                    }

                    int topCount = Math.Min(5, uniqueCount);
                    string[] topResponses = new string[topCount];

                    for (int i = 0; i < topCount; i++)
                    {
                        double percentage = (counts[i] * 100.0) / answerCount;
                        topResponses[i] = $"{uniqueAnswers[i]}";
                    }

                    return topResponses;

                } 

                public void Print(){
                    System.Console.WriteLine($"Name {_name}");
                    for (int i = 1; i <= 3; i++){
                        System.Console.WriteLine($"Top 5 of question {i}");
                        string[] TopResp = GetTopResponses(i);
                        if (TopResp == null) return;
                        for (int j = 0; j < TopResp.Length; j++){
                            System.Console.WriteLine(TopResp[j]);
                        }
                        System.Console.WriteLine();
                    }
                }
            }
    
        public class Report{
            Research[] _researches;
            private static int _research_id;

            public Research[] researches{
                get{
                    if (_researches == null){
                        return null;
                    }
                    Research[] Copy = new Research[_researches.Length];
                    Array.Copy(_researches, Copy, _researches.Length);
                    return Copy;
                }
            }

            static Report(){
                _research_id = 1;
            }

            public Report(){
                _researches = new Research[0];
            }

            public Research MakeResearch(){
                DateTime date = DateTime.Now;
                string name = $"No_{_research_id++}_{date.Month}_{date.Year}";
                if (_researches == null){
                    _researches = new Research[0];
                }

                Research[] New = new Research[_researches.Length+1];
                Array.Copy(_researches, New, _researches.Length);

                Research NewResearch = new Research(name);

                New[New.Length - 1] = NewResearch;

                _researches = New;

                return NewResearch;
            }

            public (string, double)[] GetGeneralReport(int question){
                if (question < 0 || question >= _researches.Length){
                    return null;
                }

                int totalAns = 0;
                foreach (var research in _researches){ //считаем все исследования (соотв все ответы)
                    totalAns += research.Responses.Length;
                }

                string[] allAns = new string[totalAns];
                int index = 0;

                //сохр в список все ответы
                foreach(var research in researches){
                    foreach(var response in research.Responses){
                        string resp = null;

                        resp = question switch 
                        {
                            1 => response.Animal,
                            2 => response.CharacterTrait,
                            3 => response.Concept,
                        };

                        if (resp != null){
                            allAns[index++] = resp;
                        }
                    }
                }

                //убираем нулы
                string[] answers = new string[index];
                for (int i = 0; i < index; i++){
                    answers[i] = allAns[i];
                }

                string[] unique = new string[answers.Length];
                int[] count = new int[answers.Length];
                int uniqueCount = 0;

                //идем по всем ответам
                for (int i = 0; i < answers.Length; i++){
                    string word = answers[i]; 
                    bool found = false;
                    for(int j = 0; j < uniqueCount; j++){
                        if (word == unique[j]){ //если слово уже в уникальным просто увел его счетчик
                            count[j]++; //j - индкес этого слово в списке уникальных
                            found = true;
                            break;
                        }
                    }

                    if (!found){//если слово встретилось первый раз (его нет в уникальгыъ)
                        unique[uniqueCount] = word; //добавляем в уникальные по индексу
                        count[uniqueCount] = 1; //по этому же инкексу счетчик
                        uniqueCount++; //добавляем индекс для след слова
                    }
                }

                (string, double)[] generalReport = new (string, double)[uniqueCount];

                for (int i = 0; i < uniqueCount; i++){
                    double percentage = 100.0 * count[i] / answers.Length;
                    generalReport[i] = (unique[i], percentage);
                }

                return generalReport;
            }

            // public (string, double)[] GetGeneralReport(int question){
            //     if (question < 1 || question > 3 || _researches == null || _researches.Length == 0){
            //         return null;
            //     }

            //     string[] allAns = new string[0];
            //     int totalNoEmptyAns = 0;
            //     foreach (var research in _researches) // цикл по всем исследованиям
            //     {
            //         if (research.Responses == null || research.Responses.Length == 0)
            //         {
            //             continue;
            //         }
            //         foreach (var response in research.Responses){ //цикл по ответам в исследовании
            //             string answer = question switch 
            //             {   
            //                 1 => response.Animal,
            //                 2 => response.CharacterTrait,
            //                 3 => response.Concept,
            //             };

            //             if (answer != null){
            //                 totalNoEmptyAns++;
            //                 Array.Resize(ref allAns, allAns.Length + 1);
            //                 allAns[allAns.Length - 1] = answer; // записываем ответ в список всех не пустых ответов
            //             }
            //         }
            //     }
            //     if (totalNoEmptyAns == 0){
            //         return null; //vse otveti pustie
            //     }
            //     System.Console.WriteLine("q");
            //     string[] uniqueAns = new string[0];
            //     int[] counts = new int[0];
            //     for (int i = 0; i < allAns.Length; i++){ //цикл по всем ответам
            //         bool found = false; //kogda len = 0 mi srazu dobavim element v unque т к не зайдем в некст цикл
            //         for (int j = 0; j < uniqueAns.Length; j++){ //цикл по уникальным ответам
            //             if (allAns[i] == uniqueAns[j]){// если такой элемент уже уникальный просто добавляем его к количеству
            //                 counts[j]++; //j - индексация по уникальному списку
            //                 found = true;
            //                 break;
            //             }
            //         }
            //         if (!found){
            //             Array.Resize(ref uniqueAns, uniqueAns.Length + 1);
            //             uniqueAns[uniqueAns.Length - 1] = allAns[i];

            //             Array.Resize(ref counts, counts.Length + 1);
            //             counts[counts.Length - 1] = 1;
            //         }
                    
            //     }

            //     var result = new (string, double)[uniqueAns.Length];

            //     for(int i = 0; i < uniqueAns.Length; i++){
            //         result[i] = (uniqueAns[i], counts[i]*100.0/uniqueAns.Length);
            //         System.Console.WriteLine(uniqueAns[i]);
            //         System.Console.WriteLine(counts[i]*100.0/totalNoEmptyAns);
            //     }

            //     return result;
            // }
        
        }
    }
}