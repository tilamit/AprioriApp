using SampleAprioriApp.Interface;
using SampleAprioriApp.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SampleAprioriApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] data = File.ReadAllLines(@"..\..\..\sample_data.txt", Encoding.UTF8);
            List<string> dataSet = new List<string>(data);

            IApriori aIApriori = new AprioriRepository(0.2, 0.2, dataSet);

            aIApriori.GetDataSource();
            Console.WriteLine();

            aIApriori.SetIteration();
            Console.WriteLine();

            aIApriori.SetConfidence();

            Console.Read();
        }
    }
}
