using System;
using System.Collections;
using System.IO;

namespace Knapsack
{
    class Program
    {
        class ItemWeightAndValue
        {
            public int weight;
            public int value;

            public ItemWeightAndValue(int weight, int value)
            {
                this.weight = weight;
                this.value = value;                
            }
        }

        class ValueWeightCompare : IComparer
        {
            public int Compare(Object x, Object y)
            {
                ItemWeightAndValue item1 = (ItemWeightAndValue)x;
                ItemWeightAndValue item2 = (ItemWeightAndValue)y;
                double cpr1 = (double)item1.value
                              / (double)item1.weight;
                double cpr2 = (double)item2.value
                              / (double)item2.weight;

                if (cpr1 < cpr2)
                    return 1;
                else if (cpr1 == cpr2)
                {
                    if (item2.value > item1.value)
                        return 1;
                    else
                        return -1;
                }
                else
                    return -1;
            }
        }

        static int KnapSack(ItemWeightAndValue[] items, int BagWeight)
        {
            ValueWeightCompare cmp = new ValueWeightCompare();
            Array.Sort(items, cmp);

            int totalVal = 0;
            int currW = BagWeight;
            Console.WriteLine("Çantaya eklenen nesneler:\n");
            foreach (ItemWeightAndValue i in items)
            {

                if (i.weight <= currW)
                {
                    totalVal = totalVal + i.value;
                    currW = currW - i.weight;
                    Console.WriteLine(i.weight + " kg ağırlığında " + i.value + " değerindeki nesne çantaya eklendi.");
                }
            }
            Console.WriteLine("\nÇantanın toplam değeri: " + totalVal);
            return totalVal;
        }

        static void Main(string[] args)
        {
            string FilePath = @"..\items.txt";
            ItemWeightAndValue[] items = ReadValueAndWeightsFromFile(FilePath);
            int bagweight = 0;
            var firstrow = File.ReadAllLines(FilePath)[0];
            if(firstrow != null)
                bagweight = Convert.ToInt32(firstrow);

            WriteFileContent(FilePath, bagweight, items);

            KnapSack(items, bagweight);
            Console.ReadKey();
        }

        static ItemWeightAndValue[] ReadValueAndWeightsFromFile(string FilePath)
        {
            var valueandweightList = File.ReadAllLines(FilePath);
            
            ItemWeightAndValue[] valuesAndWeights = new ItemWeightAndValue[valueandweightList.Length - 1];
            int counter = 0;
            for (int i=1; i< valueandweightList.Length; i++)
            {
                var valueandweightsplitted = valueandweightList[i].Split(' ');
                valuesAndWeights[counter] = new ItemWeightAndValue(Convert.ToInt32(valueandweightsplitted[0]), Convert.ToInt32(valueandweightsplitted[1]));

                counter++;
            }

            return valuesAndWeights;            
        }

        static void WriteFileContent(string FilePath, int bagweight, ItemWeightAndValue[] items)
        {
            Console.WriteLine("Çanta Kapasitesi: " + bagweight + "\n");
            Console.WriteLine("Nesne Ağırlığı - Nesne Değeri");
            foreach (var item in items)
            {
                Console.WriteLine("       " + item.weight + "            " + item.value);
            }
            Console.WriteLine("\n");
        }

    }
}