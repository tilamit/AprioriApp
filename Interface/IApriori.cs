using System.Collections.Generic;

namespace SampleAprioriApp.Interface
{
    public interface IApriori
    {
        public void GetDataSource();
        public void GetIteration();
        public void GetConfidence();
        public void SetIteration();
        public void SetConfidence();
        public List<List<string>> GetSubsets(List<string> data);
        public void Run();
    }
}
