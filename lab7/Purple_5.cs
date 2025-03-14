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

            public void Print(){}
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
                    
                    Response resp = new Response(answers[0], answers[1], answers[2]);
                    //Response[] newResponses = new Response[_responses.Length + 1];
                    // newResponses[_responses.Length] = resp;
                    // _responses = newResponses;
                    // Response resp = new Response(answers[0], answers[1], answers[2]);
                    Array.Resize(ref _responses, _responses.Length + 1);
                    _responses[_responses.Length - 1] = resp;
                    // _responses.Append(resp);
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
                                counts[j]++;
                                found = true;
                                break;
                            }
                        }

                        if (!found)
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
                            if (counts[j] < counts[k]){ // не сказано по какому принципу сортируются варианты с одинаковым кол-вом ответов
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
    }
}