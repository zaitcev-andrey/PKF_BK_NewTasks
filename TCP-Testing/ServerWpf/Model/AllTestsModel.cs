﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerWpf.Model
{
    internal class AllTestsModel
    {
        #region MultipleChoiceTest1
        private string _question1_MCT1 = "Какие цвета есть на флаге России?";
        private string _answer1_MCT1 = "1) Жёлтый";
        private string _answer2_MCT1 = "2) Синий";
        private string _answer3_MCT1 = "3) Белый";
        private string _answer4_MCT1 = "4) Чёрный";
        private string _answer5_MCT1 = "5) Зелёный";
        private string _answer6_MCT1 = "6) Красный";
        private string _trueAnswer_MCT1 = "236";
        #endregion

        #region MultipleChoiceTest2
        private string _question1_MCT2 = "Какие животные относятся к семейству кошачьих?";
        private string _answer1_MCT2 = "1) Гепард";
        private string _answer2_MCT2 = "2) Слон";
        private string _answer3_MCT2 = "3) Жираф";
        private string _answer4_MCT2 = "4) Снежный барс";
        private string _answer5_MCT2 = "5) Тигр";
        private string _answer6_MCT2 = "6) Волк";
        private string _trueAnswer_MCT2 = "145";


        private List<string> _answersForMultipleChoiceTests;
        #endregion

        public List<MultipleChoiceTest> AllMultipleChoiceTests { get; set; }
        //public List<MultipleChoiceTest> AllMultipleChoiceTests { get; set; }

        public AllTestsModel()
        {
            AllMultipleChoiceTests = new List<MultipleChoiceTest>();

            MultipleChoiceTest mctTest1 = new MultipleChoiceTest(_question1_MCT1, _answer1_MCT1, _answer2_MCT1,
                _answer3_MCT1, _answer4_MCT1, _answer5_MCT1, _answer6_MCT1, _trueAnswer_MCT1);
            MultipleChoiceTest mctTest2 = new MultipleChoiceTest(_question1_MCT2, _answer1_MCT2, _answer2_MCT2,
                _answer3_MCT2, _answer4_MCT2, _answer5_MCT2, _answer6_MCT2, _trueAnswer_MCT2);
            AllMultipleChoiceTests.Add(mctTest1);
            AllMultipleChoiceTests.Add(mctTest2);

            _answersForMultipleChoiceTests = new List<string>
            {
                _trueAnswer_MCT1,
                _trueAnswer_MCT2
            };
        }

        public TestData GetTestData(object selectedTest)
        {
            ITest tmpTest = selectedTest as ITest;
            StringBuilder testString = new StringBuilder();
            TestData result = null;
            if (tmpTest.Type == TestTypes.MultipleChoiceTest)
            {
                MultipleChoiceTest test = selectedTest as MultipleChoiceTest;
                testString.AppendLine($"Перечислите через запятую все верные ответы в следующем вопросе");
                testString.AppendLine($"Вопрос: {test.Question}");
                testString.AppendLine($"{test.Answer1}");
                testString.AppendLine($"{test.Answer2}");
                testString.AppendLine($"{test.Answer3}");
                testString.AppendLine($"{test.Answer4}");
                testString.AppendLine($"{test.Answer5}");
                testString.AppendLine($"{test.Answer6}");

                result = new TestData(testString.ToString(), TestTypes.MultipleChoiceTest, test.Index);
            }
            return result;
        }

        public int GetResultForTest(string answer, TestData data)
        {
            int result = 0;
            if(data.Type == TestTypes.MultipleChoiceTest)
            {
                string[] answers = answer.Trim().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                string realAnswer = _answersForMultipleChoiceTests[data.Index - 1];
                int len = realAnswer.Length;
                float countOfTrueAnswer = 0;
                foreach (var ans in answers)
                {
                    if (string.IsNullOrWhiteSpace(realAnswer))
                        break;
                    if (!realAnswer.Contains(ans))
                        countOfTrueAnswer--;
                    else
                    {
                        int ind = realAnswer.IndexOf(ans);
                        realAnswer = realAnswer.Remove(ind, ans.Length);
                        countOfTrueAnswer++;
                    }
                }
                if(countOfTrueAnswer <= 0)
                    result = 0;
                else
                    result = (int)(countOfTrueAnswer / len * 100);
            }
            return result;
        }
    }
}
