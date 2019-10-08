using System;
using System.Collections.Generic;
using System.Text;

namespace Tres.UtilityDate
{
    public static class BusinessDays
    {
        public enum BusinessWeek
        {
            StandardFiveDay,
            MondayThruThursday
        }
        
        public static bool IsBusinessDay(this DateTime i, BusinessWeek option)
        {
            IBusinessDayChecker checker;

            switch (option)
            {
                case BusinessWeek.MondayThruThursday:

                    checker = new FourTensBusinessDayChecker();
                    break;
              
                default:
                    checker = new StandardFiveDayBusinessDayChecker();
                    break;

            }

            return checker.Check(i);
        }
    }

    interface IBusinessDayChecker{
    
        bool Check(DateTime val);
    }

    class StandardFiveDayBusinessDayChecker : IBusinessDayChecker
    {
        public bool Check(DateTime val)
        {
            var dy = val.DayOfWeek;
            if (dy == DayOfWeek.Friday)
            {
                return true;
            }

            var FourDay = new FourTensBusinessDayChecker();
            return FourDay.Check(val);
            
        }
    }

    class FourTensBusinessDayChecker : IBusinessDayChecker
    {
        public bool Check(DateTime val)
        {
            var dy = val.DayOfWeek;
            if (dy == DayOfWeek.Monday || dy == DayOfWeek.Tuesday || dy == DayOfWeek.Wednesday || dy == DayOfWeek.Thursday)
            {
                return true;
            }
            else
            {
                return false;
            }            
        }
    }
}