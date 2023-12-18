using System.Collections.Generic;

namespace SampleAprioriApp.Interface
{
    public interface IApriori
    {
        public void GetDataSource();
        public void SetIteration();
        public void SetConfidence();
        public void IterateData();
        public void ProcessConfidence();
        public List<List<string>> GetSubsets(List<string> data);
        public void Run();
    }
}
