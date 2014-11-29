using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarPageGenerator {
    public static class IEnumerableExtensions {
        public static T[,] ToTwoDimensionalArray<T>(this IEnumerable<T> linearArray, int secondDimension) {
            var list = linearArray.ToList();
            var firstDimension = list.Count / secondDimension;
            var result = new T[firstDimension, secondDimension];
            int offset = 0;
            for (int i = 0; i < firstDimension; i++)
                for (int j = 0; j < secondDimension; j++)
                    result[i, j] = list[offset++];
            return result;
        }
    }
}
