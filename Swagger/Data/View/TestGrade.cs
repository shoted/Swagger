using Swagger.Common;

namespace Swagger.Data.View
{
    public class TestGrade
    {
        public int Grade { get; set; }
        public string Analysis { get; set; }
        public int ismax { get; set; }

        public TestGrade()
        {
            Grade = CTools.GetRandom(80, 100);
            Analysis = "";
        }
    }
}
