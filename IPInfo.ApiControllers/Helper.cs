/*
    Author:     Esequiel de Paiva Ferreira - esequiel.paiva@gmail.com
    Date:       11/04/2024
    Purpose:    Demo Asp.net Core Web Api.    
*/

namespace IPInfo.ApiControllers
{
    public class Helper
    {
        public static bool CheckIsValidIPAddress(string strIP)
        {
            System.Net.IPAddress result = null;
            return
                !string.IsNullOrEmpty(strIP) &&
                System.Net.IPAddress.TryParse(strIP, out result);
        }
    }
}
