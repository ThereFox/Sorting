using System.Globalization;

var sortingCollection = new Sorting();

var collectionForSort = new List<int>()
{
    3, 5, 2, 5, 4, 1
    //2, 4, 5, 7, 1, 2, 3, 6
};

//sortingCollection.InsertionSort(collectionForSort);
var arr = collectionForSort.ToArray();
sortingCollection.mergeSort(arr, 0, collectionForSort.Count - 1);

foreach (var item in arr)
{
    Console.WriteLine(item);
}


public class Sorting
{
    public void InsertionSort(List<int> collection)
    {
        for (int j = 1; j < collection.Count; j++)
        {
            var key = collection[j];

            var i = j - 1;

            while (i >= 0 && collection[i] > key)
            {
                collection[i + 1] = collection[i];
                i = i - 1;
            }

            collection[i + 1] = key;
        }
    }

    public void mergeSort(int[] collection, int startIndex, int endIndex)
    {
        if(startIndex >= endIndex)
        {
            return;
        }
        var middleIndex = (endIndex + startIndex) / 2;

        mergeSort(collection, startIndex, middleIndex);
        mergeSort(collection, middleIndex + 1, endIndex);
        merge(collection, startIndex, middleIndex, endIndex);

    }
    private void merge(int[] sortedCollection, int startIndex, int middleIndex, int endIndex)
    {
        //  startIndex ... middleIndex; middleIndex + 1 ... endIndex

        var leftLength = middleIndex - startIndex + 1;
        var rightLength = endIndex - middleIndex;

        var leftCollection = new int[leftLength + 1];
        var rightCollection = new int[rightLength + 1];
        
        for (int iter = 0; iter < leftLength; iter++)
        {
            leftCollection[iter] = sortedCollection[startIndex + iter];
        }

        for (int iter2 = 1; iter2 <= rightLength; iter2++)
        {
            rightCollection[iter2 - 1] = sortedCollection[middleIndex + iter2];
        }

        leftCollection[leftLength] = int.MaxValue;
        rightCollection[rightLength] = int.MaxValue;

        var i = 0;
        var j = 0;

        for (int k = startIndex; k <= endIndex; k++)
        {
            if (leftCollection[i] < rightCollection[j])
            {
                sortedCollection[k] = leftCollection[i];
                i++;
            }
            else
            {
                sortedCollection[k] = rightCollection[j];
                j++;
            }
        }
    }

    }