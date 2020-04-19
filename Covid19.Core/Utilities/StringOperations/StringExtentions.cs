using Covid19.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Covid19.Core.Utilities.StringOperations
{
    [Serializable]
    public class StringExtentions
    {
        public static string IsAnyNullOrEmpty(List<EntityField> obj)
        {
            var result = string.Empty;

            if (obj == null) return result;
            foreach (var item in obj)
            {
                if (result == string.Empty)
                {
                    result = string.IsNullOrEmpty(item.Value) ? item.Name : false || string.IsNullOrWhiteSpace(item.Value) ? item.Name : string.Empty;
                }
                else
                    return result;
            }
            return result;
        }

        public static string IsAnyNullOrEmptyV2(List<EntityField> obj)
        {
            var result = string.Empty;

            if (obj == null) return result;
            foreach (var item in obj)
            {
                if (result == string.Empty)
                {
                    result = string.IsNullOrEmpty(item.Value) ? item.Name : false || string.IsNullOrWhiteSpace(item.Value) ? item.Name : string.Empty;
                }
                else
                    return result;
            }
            return result;
        }
        public static string SetHtmlMessage<T>(IEnumerable<T> list, params Func<T, object>[] fxns)
        {
            var sb = new StringBuilder();
            sb.Append("<Müşteri Konuşma Geçmişi>\n");
            foreach (var item in list)
            {
                sb.Append("\n");
                foreach (var fxn in fxns)
                {
                    sb.Append(fxn(item));
                }
                sb.Append("\n");
            }
            sb.Append("<Müşteri Konuşma Geçmişi>");
            return sb.ToString();
        }
        public static string FirstCharToUpper(string input)
        {
            if (String.IsNullOrEmpty(input))
                throw new ArgumentException("ARGH!");
            return input.First().ToString().ToUpper() + input.Substring(1);
        }
        // Genişletme metodu, karşılaştırma matrisini de out parametresi olarak döndürmektedir. 
        public static int FindLevenshteinDistance(string Source, string Target, out int[,] Matrix)
        {
            int n = Source.Length;
            int m = Target.Length;

            Matrix = new int[n + 1, m + 1]; // Hesaplama matrisi üretilir. 2 boyutlu matrisin boyut uzunlukları ise kaynak ve hedef metinlerin karakter uzunluklarına göre set edilir

            if (n == 0) // Eğer kaynak metin yoksa zaten hedef metnin tüm harflerinin değişimi söz konusu olduğundan, hedef metnin uzunluğu kadar bir yakınlık değeri mümkün olabilir 
                return m;

            if (m == 0) // Yukarıdaki durum hedefin karakter içermemesi halinde de geçerlidir 
                return n;

            // Aşağıdaki iki döngü ile yatay ve düşey eksenlerdeki standart 0,1,2,3,4...n elemanları doldurulur 
            for (int i = 0; i <= n; i++)
                Matrix[i, 0] = i;

            for (int j = 0; j <= m; j++)
                Matrix[0, j] = j;

            // Kıyaslama ve derecelendirme operasyonu yapılır 
            for (int i = 1; i <= n; i++)
                for (int j = 1; j <= m; j++)
                {
                    int cost = (Target[j - 1] == Source[i - 1]) ? 0 : 1;
                    Matrix[i, j] = Math.Min(Math.Min(Matrix[i - 1, j] + 1, Matrix[i, j - 1] + 1), Matrix[i - 1, j - 1] + cost);
                }

            return Matrix[n, m]; // sağ alt taraftaki hücre değeri döndürülür 
        }
        public static bool IsValueOnlyNumeric(string text)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(text, "[^0-9]"))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
