using System.Security.Cryptography;
using System.Text;

namespace MainWeb
{
    public class Helper
    {

        static RSACryptoServiceProvider provider = new  RSACryptoServiceProvider();
        public static string CreateRandomPassword(int length = 5)
        {
            StringBuilder res = new StringBuilder();
            string CapitalLetters = "QWERTYUIOPASDFGHJKLZXCVBNM";
            res.Append(CreateSpecialPassword(CapitalLetters));   
            
            string SmallLetters = "qwertyuiopasdfghjklzxcvbnm";
            res.Append(CreateSpecialPassword(SmallLetters));
            
            string Digits = "0123456789";
            res.Append(CreateSpecialPassword(Digits));


            string SpecialCharacters = "!@#$%^&*()-_=+<,>.";
            string AllChar = CapitalLetters + SmallLetters + Digits + SpecialCharacters;

            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(AllChar[rnd.Next(AllChar.Length)]);
            }

            res.Append(CreateSpecialPassword(SpecialCharacters));
            return res.ToString();


        }




        public static string CreateSpecialPassword(string AllChar, int length =1)
        {

            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(AllChar[rnd.Next(AllChar.Length)]);
            }

            return res.ToString();


        }


        public static string CalculateYourAge(DateTime Dob)
        {
            if (Dob == null)
                return "";
            DateTime Now = DateTime.Now;
            int Years = new DateTime(DateTime.Now.Subtract(Dob).Ticks).Year - 1;
            DateTime PastYearDate = Dob.AddYears(Years);
            int Months = 0;
            for (int i = 1; i <= 12; i++)
            {
                if (PastYearDate.AddMonths(i) == Now)
                {
                    Months = i;
                    break;
                }
                else if (PastYearDate.AddMonths(i) >= Now)
                {
                    Months = i - 1;
                    break;
                }
            }
            return String.Format("{0} Tahun,  {1} Bulan", Years, Months);
        }

       public static Task<string> SaveFoto(IWebHostEnvironment env, byte[] bytes)
        {
            string wwwPath = env.WebRootPath;
            string contentPath = env.ContentRootPath;
            string path = Path.Combine(env.WebRootPath, "photos");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            var fileName= $"{Guid.NewGuid().ToString()}.png";
            File.WriteAllBytes($"{path}/{fileName}", bytes);
            return Task.FromResult( fileName);
        }


       public static Task<bool> DeleteFoto(IWebHostEnvironment env, string fileName)
        {
            string wwwPath = env.WebRootPath;
            string contentPath = env.ContentRootPath;
            string path = Path.Combine(env.WebRootPath, "photos");
            File.Delete($"{path}/{fileName}");
            return Task.FromResult(true);
        }

    }
}