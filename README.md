### Environment：
Visual Studio 2022

.net 5.0

### Instruction：
Sample Apriori App：

Instantiate the Apriori object as follows. For example:
```C#
IApriori aIApriori = new AprioriRepository(0.2, 0.2, dataSet);
```

Its constructor has the following parameters:
```C#
/** 
 * support
 * confidence
 * data - List
 */
public AprioriRepository (double support, double confidence, List<string> data)
{
  ...
}
```

Each data should be separated by a comma in the text file. For example:

![image](https://i.postimg.cc/5NDtq5Yd/Screenshot-2023-12-18-210315.png?raw=true)

After instantiating, the results will be stored in the variables `iterationData` and `confidenceData` respectively.

The results are obtained by calling `SetIteration` and `SetConfidence` from the Apriori object respectively. For example:
```C#
aIApriori.SetIteration();
aIApriori.SetConfidence();
```

![image](https://i.postimg.cc/Gmqdgk9m/Screenshot-2023-12-18-210548.png)

To obtain origin data, this method `GetDataSource` is called. For example:
```C#
aIApriori.GetDataSource();
```
![image](https://i.postimg.cc/QdnvWSH1/Screenshot-2023-12-18-210700.png)