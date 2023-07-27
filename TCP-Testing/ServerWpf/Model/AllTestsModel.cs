using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerWpf.Model
{
    internal class AllTestsModel
    {
        #region MultipleChoiceTest1
        private string _question1_MCT1 = "как дела?";
        private string _answer1_MCT1 = "ответ1";
        private string _answer2_MCT1 = "ответ2";
        private string _answer3_MCT1 = "ответ3";
        private string _answer4_MCT1 = "ответ4";
        private int _trueAnswer_MCT1 = 3;
        #endregion

        #region MultipleChoiceTest2
        private string _question1_MCT2 = "как дела2?";
        private string _answer1_MCT2 = "ответ12";
        private string _answer2_MCT2 = "ответ22";
        private string _answer3_MCT2 = "ответ32";
        private string _answer4_MCT2 = "ответ42";
        private int _trueAnswer_MCT2 = 2;
        #endregion

        public List<MultipleChoiceTest> AllMultipleChoiceTests { get; set; }
        //public List<MultipleChoiceTest> AllMultipleChoiceTests { get; set; }

        public AllTestsModel()
        {
            AllMultipleChoiceTests = new List<MultipleChoiceTest>();

            MultipleChoiceTest mctTest1 = new MultipleChoiceTest(_question1_MCT1, _answer1_MCT1, _answer2_MCT1, _answer3_MCT1, _answer4_MCT1, _trueAnswer_MCT1);
            MultipleChoiceTest mctTest2 = new MultipleChoiceTest(_question1_MCT2, _answer1_MCT2, _answer2_MCT2, _answer3_MCT2, _answer4_MCT2, _trueAnswer_MCT2);
            AllMultipleChoiceTests.Add(mctTest1);
            AllMultipleChoiceTests.Add(mctTest2);
        }

        public string GetTestData(object selectedItem)
        {
            ITest tmpTest = selectedItem as ITest;
            StringBuilder result = new StringBuilder();
            if(tmpTest.Type == TestTypesEnum.MultipleChoiceTest)
            {
                MultipleChoiceTest test = selectedItem as MultipleChoiceTest;
                result.AppendLine($"На следующий вопрос вам нужно дать единственный ответ. Напишите цифру ответа");
                result.AppendLine($"Вопрос: {test.Question}");
                result.AppendLine($"Ответ 1: {test.Answer1}");
                result.AppendLine($"Ответ 2: {test.Answer2}");
                result.AppendLine($"Ответ 3: {test.Answer3}");
                result.AppendLine($"Ответ 4: {test.Answer4}");
            }
            return result.ToString();
        }
    }
}
