using System.Globalization;

var sortingCollection = new Sorting();
var serchingCollection = new Serching();
var anoutherAlghorithms = new AnoutherAlghorithms();

var collectionForSort = new List<int>()
{
    3, -5, -2, 5, -1, 4, -1, 9, 4, -2
};



/*
//sortingCollection.InsertionSort(collectionForSort);
var arr = collectionForSort.ToArray();
sortingCollection.mergeSort(arr, 0, collectionForSort.Count - 1);

foreach (var item in arr)
{
    //Console.WriteLine(item);
}
//
var res = serchingCollection.FindMaximumSubArray(collectionForSort, 0, collectionForSort.Count - 1);

Console.WriteLine($"{res.Item1} {res.Item2} {res.Item3}");
*/

var res = sortingCollection.HeapSort(collectionForSort.ToArray());

Console.WriteLine(res);

public class Sorting
{
    #region InsertionSort
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
    #endregion

    #region mergeSort
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
    #endregion

    #region heapSort

    public int[] HeapSort(int[] arrayForSort)
    {
        var heap = BuildMaxHeap(arrayForSort);
        for (int i = heap.Length - 1 ; i >= 1; i--)
        {
            var current = heap[i];
            heap[i] = heap[0];
            heap[0] = current;

            heap.Count -= 1;

            MaxHeapify(heap, 0);

        }
        return heap.data;
    }

    private Heap BuildMaxHeap(int[] array)
    {
        var heap = new Heap(array);
        heap.Count = heap.Length;
        for (int i = heap.Length - 1; i >= 0; i--)
        {
            MaxHeapify(heap, i);
        }
        return heap;
    }

    private void MaxHeapify(Heap heap, int i)
    {
        var largestElementIndex = 0;

        var leftElementIndex = GetLeftSubElementIndex(i);
        var rightElementIndex = GetRinghtSubElementIndex(i);

        if (heap.Count > leftElementIndex && heap[leftElementIndex] > heap[i])
        {
            largestElementIndex = leftElementIndex;
        }
        else
        {
            largestElementIndex = i;
        }

        if (heap.Count > rightElementIndex && heap[rightElementIndex] > heap[largestElementIndex])
        {
            largestElementIndex = rightElementIndex;
        }

        if (largestElementIndex != i)
        {
            var current = heap[i];

            heap[i] = heap[largestElementIndex];

            heap[largestElementIndex] = current;

            MaxHeapify(heap, largestElementIndex);
        }

    }
    private int GetRinghtSubElementIndex(int i)
    {
        return 2 * i + 1;
    }
    private int GetLeftSubElementIndex(int i)
    {
        return 2 * i;
    }
    #endregion
}


public class Serching
{
    #region FindMaximumSubArray
    public (int,int,int) FindMaximumSubArray(List<int> collection, int lowerIndex, int hightIndex)
    {
        if(hightIndex == lowerIndex)
        {
            return (lowerIndex, hightIndex, collection[lowerIndex]);
        }

            var middleIndex = (lowerIndex + hightIndex) / 2;

            var leftVariant = FindMaximumSubArray(collection, lowerIndex, middleIndex);
            var rightVariant = FindMaximumSubArray(collection, middleIndex + 1, hightIndex);
            var middleVariant = findMaxCrossingSubArray(collection, lowerIndex, middleIndex, hightIndex);

            if(leftVariant.Item3 >= rightVariant.Item3 && leftVariant.Item3 >= middleVariant.Item3)
            {
                return (leftVariant.Item1, leftVariant.Item2, leftVariant.Item3);
            }
            else if(rightVariant.Item3 >= leftVariant.Item3 && rightVariant.Item3 >= middleVariant.Item3)
            {
                return (rightVariant.Item1, rightVariant.Item2, rightVariant.Item3);
            }
            else
            {
                return (middleVariant.Item1, middleVariant.Item2, middleVariant.Item3);
            }

    }

    private (int, int, int) findMaxCrossingSubArray(List<int> collection, int lowerIndex, int middleIndex, int hightIndex)
    {
        var leftSum = int.MinValue;
        var summ = 0;
        int maxLeft = 0;

        for (int i = middleIndex; lowerIndex < i; i--)
        {
            summ -= collection[i];
            if(summ > leftSum)
            {
                leftSum = summ;
                maxLeft = i;
            }
        }

        var rightSumm = int.MinValue;
        summ = 0;
        int maxRight = 0;

        for (int i = middleIndex + 1; i < hightIndex; i++)
        {
            summ += collection[i];

            if(summ > rightSumm)
            {
                rightSumm = summ;
                maxRight = i;
            }

        }

        return(maxLeft, maxRight, leftSum + rightSumm);
    }
#endregion



}

public class AnoutherAlghorithms
{
    public int[,] MultiplyMatrix(int[,] firstMatrix, int[,] secondMatrix)
    {
        return new int[1,1]; 
    }
}

#region models
    public class Heap
    {
        public int[] data;
        public int Count;

        public Heap(int[] ArrayForSort)
        {
            data = ArrayForSort;
        }
        public int Length { get
            {
                return data.Length;
            }}

        public int this[int index]
        {
            get { return data[index]; }
            set { data[index] = value; }
        }
    }
#endregion