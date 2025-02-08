namespace İngilizceKelimeler.Models
{
    public class RandomSayiUret
    {
         Random rnd = new Random();
        public int RandomSayiUretme(int minValue,int maxValue)
        {

           return rnd.Next(minValue, maxValue);
        }
    }
}
