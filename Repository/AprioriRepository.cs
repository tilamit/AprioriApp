using SampleAprioriApp.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SampleAprioriApp.Repository
{
    public class AprioriRepository : IApriori
    {
        private int support;
        private double confidence;

        private List<List<bool>> originDataMapping;

        private Hashtable indexMapping;

        private List<Dictionary<string, int>> iterationData;
        private Dictionary<string, double> confidenceData;

        public AprioriRepository(double support, double confidence, List<string> data)
        {
            this.support = (int)(support * data.Count);
            this.confidence = confidence;
            this.originDataMapping = new List<List<bool>>();
            this.indexMapping = new Hashtable();
            this.iterationData = new List<Dictionary<string, int>>();
            this.confidenceData = new Dictionary<string, double>();
            List<string> originDataList = new List<string>(data);
            HashSet<string> itemSet = new HashSet<string>();

            //Unique elements are added - Starts
            for (int i = 0; i < originDataList.Count; i++)
            {
                List<string> temp = new List<string>(originDataList[i].Split(','));

                for (int j = 0; j < temp.Count; j++)
                {
                    temp[j] = temp[j].Trim();
                }

                foreach (string item in temp)
                {
                    itemSet.Add(item);
                }
            }
            //Unique elements are added - Ends

            //Item with indexes - Starts
            int index = 0;
            foreach (string item in itemSet)
            {
                this.indexMapping.Add(item, index);
                this.indexMapping.Add(index, item);

                index++;
            }
            //Item with indexes - Ends

            for (int i = 0; i < originDataList.Count; i++)
            {
                List<string> temp = new List<string>(originDataList[i].Split(','));

                for (int j = 0; j < temp.Count; j++)
                {
                    temp[j] = temp[j].Trim();
                }

                List<bool> tempMap = new List<bool>(itemSet.Count);

                //Items added with status false - Starts
                for (int j = 0; j < itemSet.Count; j++)
                {
                    tempMap.Add(false);
                }
                //Items added with status false - Ends

                //Item found updated with status true - Starts
                foreach (string item in temp)
                {
                    tempMap[(int)indexMapping[item]] = true;
                }
                //Item found updated with status true - Ends

                originDataMapping.Add(tempMap); //List updated with status true or false
            }

            this.Run();
        }

        public void GetDataSource()
        {
            Console.WriteLine("Data Source:");

            for (int i = 0; i < this.originDataMapping.Count; i++)
            {
                Console.Write("\t" + (i + 1) + "\t");

                List<string> data = new List<string>();

                for (int j = 0; j < this.originDataMapping[i].Count; j++)
                {
                    if (this.originDataMapping[i][j])
                    {
                        data.Add(" " + indexMapping[j].ToString());
                    }
                }

                Console.WriteLine(string.Join(',', data.ToArray()));
            }
        }

        public void SetIteration()
        {
            for (int i = 0; i < this.iterationData.Count; i++)
            {
                Console.WriteLine("I" + (i + 1) + ":");

                List<KeyValuePair<string, int>> dicList = this.iterationData[i].ToList();
                dicList.Sort((p1, p2) => p1.Key.CompareTo(p2.Key));

                for (int j = 0; j < dicList.Count; j++)
                {
                    Console.WriteLine("\t{ " + dicList[j].Key + " } => " + dicList[j].Value);
                }

                Console.WriteLine();
            }
        }

        public void SetConfidence()
        {
            Console.WriteLine("Confidence：");

            List<KeyValuePair<string, double>> dicList = this.confidenceData.ToList();
            dicList.Sort((p1, p2) => p1.Key.CompareTo(p2.Key));

            for (int i = 0; i < dicList.Count; i++)
            {
                Console.WriteLine("\t" + dicList[i].Key + " : " + dicList[i].Value.ToString("0.0000"));
            }

            Console.WriteLine();
        }

        public void IterateData()
        {
            int originCount = this.originDataMapping.Count;

            if (originCount <= 0)
            {
                return;
            }

            //Initial count
            //Add count of items in dictionary - Starts
            Dictionary<string, int> itemCount = new Dictionary<string, int>();

            for (int i = 0; i < originCount; i++)
            {
                for (int j = 0; j < this.originDataMapping[i].Count; j++)
                {
                    if (this.originDataMapping[i][j])
                    {
                        if (itemCount.ContainsKey(this.indexMapping[j].ToString()))
                        {
                            itemCount[this.indexMapping[j].ToString()] += 1;
                        }
                        else
                        {
                            itemCount.Add(this.indexMapping[j].ToString(), 1);
                        }
                    }
                }
            }
            //Add count of items in dictionary - Ends

            this.iterationData.Add(itemCount);

            int iterate = 0;
            while (true)
            {
                //Items having count less than or equal to 1, gets added here - Starts
                if (this.iterationData[iterate].Count <= 1)
                {
                    if (this.iterationData[iterate].Count == 0)
                    {
                        this.iterationData.RemoveAt(iterate);
                    }
                    break;
                }
                //Items having count less than or equal to 1, gets added here - Ends

                //Items having count greater than 1, gets added here - Starts
                List<List<string>> last = new List<List<string>>();

                foreach (string key in this.iterationData[iterate].Keys)
                {
                    List<string> temp = new List<string>(key.Split(','));
                    temp.Sort((s1, s2) => s1.CompareTo(s2));

                    last.Add(temp);
                }
                //Items having count greater than 1, gets added here - Ends

                List<List<string>> conjTable = new List<List<string>>();

                for (int i = 0; i < last.Count; i++)
                {
                    List<string> toConj = new List<string>();

                    for (int j = i + 1; j < last.Count; j++)
                    {
                        bool canConj = true;
                        for (int k = 0; k < iterate; k++)
                        {
                            if (!last[i][k].Equals(last[j][k]))
                            {
                                canConj = false;
                                break;
                            }
                        }
                        if (canConj)
                        {
                            toConj.Add(last[j][iterate]);
                        }
                    }

                    //Creates pattern with each item; example item apple with other items - Starts
                    foreach (string item in toConj)
                    {
                        List<string> conj = new List<string>(last[i]);
                        conj.Add(item);
                        conj.Sort((s1, s2) => s1.CompareTo(s2));
                        conjTable.Add(conj);
                    }
                    //Creates pattern with each item; example item apple with other items - Ends
                }

                //Adds occurrences of items; example Apple Banana Fish 2 - Starts
                Dictionary<string, int> freqSet = new Dictionary<string, int>();
                for (int i = 0; i < conjTable.Count; i++)
                {
                    string key = string.Join(',', conjTable[i].ToArray());
                    int num = 0;

                    foreach (List<bool> data in this.originDataMapping)
                    {
                        bool canAdd = true;
                        for (int j = 0; j < conjTable[i].Count; j++)
                        {
                            if (!data[(int)this.indexMapping[conjTable[i][j]]])
                            {
                                canAdd = false;
                                break;
                            }
                        }
                        if (canAdd)
                        {
                            num++;
                        }
                    }
                    if (num >= this.support)
                    {
                        freqSet.Add(key, num);
                    }
                }

                this.iterationData.Add(freqSet);

                iterate++;
                //Adds occurrences of items; example Apple Banana Fish 2 - Ends
            }
        }

        public void ProcessConfidence()
        {
            if (iterationData.Count == 1) return;

            for (int i = 1; i < iterationData.Count; i++)
            {
                foreach (var key in iterationData[i].Keys)
                {
                    //Gets the subset of items
                    List<List<string>> subsets = GetSubsets(new List<string>(key.Split(',')));

                    for (int j = 0; j < subsets.Count; j++)
                    {
                        if (subsets[j].Count == 0 || subsets[j].Count == i + 1) continue;
                        for (int k = 0; k < subsets.Count; k++)
                        {
                            //Calls the subset; example for item Apple, creates possible subsets - Starts
                            List<string> fullSet = new List<string>(subsets[j]);
                            fullSet.AddRange(subsets[k]);
                            fullSet.Sort((s1, s2) => s1.CompareTo(s2));

                            if (!string.Join(',', fullSet.ToArray()).Equals(key)) continue;
                            //Calls the subset; example for item Apple, creates possible subsets - Ends

                            subsets[j].Sort((s1, s2) => s1.CompareTo(s2));
                            subsets[k].Sort((s1, s2) => s1.CompareTo(s2));

                            string theKey = string.Join(",", subsets[j].ToArray());

                            int conditionNum = iterationData[subsets[j].Count - 1][theKey];
                            int totalNum = iterationData[key.Split(',').Length - 1][key];

                            double conf = totalNum * 1.0 / conditionNum;

                            //Prints data comparing threshold value - Starts
                            if (conf > this.confidence)
                            {
                                this.confidenceData.Add("{" + theKey + "} ==> {" + string.Join(" ", subsets[k].ToArray()) + "}", conf);
                            }
                            //Prints data comparing threshold value - Ends
                        }
                    }
                }
            }
        }

        public List<List<string>> GetSubsets(List<string> data)
        {
            int n = data.Count;
            List<List<string>> res = new List<List<string>>();
            List<string> t = new List<string>();

            //Adds subset of items - Starts
            for (int mask = 0; mask < (1 << n); ++mask)
            {
                t.Clear();
                for (int i = 0; i < n; ++i)
                {
                    if ((mask & (1 << i)) != 0)
                    {
                        t.Add(data[i]);
                    }
                }
                res.Add(new List<string>(t));
            }
            //Adds subset of items - Ends

            return res;
        }

        public void Run()
        {
            this.IterateData();
            this.ProcessConfidence();
        }
    }
}