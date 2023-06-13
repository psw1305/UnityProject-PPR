using System;
using System.Collections.Generic;
using System.Linq;

namespace PSW.Core.Extensions
{
    public static class ExtensionsList
    {
        private static readonly System.Random rand = new();

        // ÂüÁ¶ : http://stackoverflow.com/questions/273313/randomize-a-listt/1262619#1262619 
        public static void Shuffle<T>(this IList<T> list)
        {
            int i = list.Count;
            while (i > 1)
            {
                i--;
                int k = rand.Next(i + 1);
                (list[i], list[k]) = (list[k], list[i]);
            }
        }

        public static T Random<T>(this IList<T> list)
        {
            return list[rand.Next(list.Count)];
        }

        public static T Last<T>(this IList<T> list)
        {
            return list[list.Count - 1];
        }

        public static List<T> GetRandomElements<T>(this List<T> list, int elementsCount)
        {
            return list.OrderBy(arg => Guid.NewGuid()).Take(list.Count < elementsCount ? list.Count : elementsCount).ToList();
        }
    }
}
