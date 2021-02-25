using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCaseGenerator
{
    class QuestionModel
    {
        public int Qid { get; set; }

        public int TopicId { get; set; }

        public string QuestionStem { get; set; }

        public Status IsHint { get; set; }

        public string Hint { get; set; }

        public Status HasChallange { get; set; }

        public int ChallangeId { get; set; }

        public Status HasAlternate { get; set; }

        public string AlternateId { get; set; }
    }

    class TestCases
    {
        public int Qid { get; set; }

        public byte[] Input { get; set; }

        public byte[] ExpOutput { get; set; }

        public Status IsSample { get; set; }
    }

    class Challange
    {
        public int Qid { get; set; }

        public int ChallangeId { get; set; }

        public String ChallangeStem { get; set; }
    }

    class Alternate
    {
        public int Qid { get; set; }

        public int AltId { get; set; }

        public String AlternateStem { get; set; }
    }

    enum Status
    {
        Available=1,
        NotAvailable=0,
    }
}
