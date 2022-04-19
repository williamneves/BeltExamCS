using System;
using System.ComponentModel.DataAnnotations;
namespace DojoMeetUP.Models
{
    public class CustomDateRangeAttribute : RangeAttribute
    {
        public CustomDateRangeAttribute() : base(typeof(DateTime), DateTime.Now.ToString(), DateTime.Now.AddYears(10).ToString()) 
        { } 
    }
}