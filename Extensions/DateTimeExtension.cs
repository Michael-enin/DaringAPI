using System;

namespace DaringAPI.Extensions
{
    public static class DateTimeExtension
    {
        public static int calculateAge(this DateTime birthDate){
            var today = DateTime.Today;
            var age = today.Year - birthDate.Year;
            if(birthDate.Date > today.AddYears(-age))
                age--;
            return age;
        }
    }
}