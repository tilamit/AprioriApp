### Project

Sample Apriori App

### Approach：

#### Intro：

Apriori algorithm is used in mining frequent product or item sets and relevant association rules. Generally, the apriori algorithm operates on a database containing a huge number of transactions. The algorithm helps customers to buy their products with ease and increases the sales performance of outlets.

#### Components of Apriori algorithm

The below three components are essential part of the apriori algorithm:

###### Support
###### Confidence
###### Lift

#### Example 

Suppose there are 2000 transactions in a outlet. The Support, Confidence, and Lift have to be calculated, say for products laptop and laptop bag. These two products or items are frequently bought by customers. Out of 2000 transactions, 400 transactions contain laptop bags, 600 transactions contain laptop and among 600 transactions, 200 transactions contain laptop and laptop bags. With these data, we will figure out the support, confidence, and lift.

##### Support

Support refers to the default popularity of any product. The support is calculated as a quotient of division of the number of transactions comprising a product by the total number of transactions. In our case, we get 

Support (laptop bags) = (Transactions with laptop bags) / (Total transactions)

= 400 / 2000 = 20 percent.

##### Confidence

Confidence is the possibility that customers buy laptops and laptop bags together. In this scenario, we have to divide number of transactions that comprise both laptops and laptop bags by the total number of transactions to get the confidence.

Confidence = (Transactions with both laptop bags and laptops) / (Total transactions having laptop bags)

= 200 / 400 = 50 percent

##### Lift

lift refers to the increase in the ratio of sales of laptops when laptop bags are sold. The mathematical equation of lift is given below:

Lift = (Confidence (laptop bags - laptops) / (Support (laptop bags)

= 50 / 20 = 2.5

##### Implementation

Using brute force, Analysis has been done for all possible rules and the technique finds the support and confidence levels for individual rule. Later it eliminates the values that are less than the threshold of support and confidence levels.

### Environment：
Visual Studio 2022

.net 5.0

### Instruction：

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