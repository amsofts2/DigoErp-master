using DigoErp.Resources.App_Resources;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Mvc;

namespace DigoErp.Helpers
{
    public static class DropdownHelper
    {

        public static IEnumerable<SelectListItem> GetMonthsDropdown()
        {
            var list = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Value = "",
                    Text = AppResource.SelectOption
                }
            };
            var months = DateTimeFormatInfo.InvariantInfo.MonthNames;

            for (int index = 0; index < months.Length; index++)
            {
                var monthName = months[index];
                if (!string.IsNullOrEmpty(monthName))
                {
                    var val = index + 1;
                    list.Add(new SelectListItem
                    {
                        Value = val <= 9 ? "0" + val : val.ToString(),
                        Text = val <= 9 ? "0" + val : val.ToString()
                    });
                }
            }
            return list;
        }

        public static List<SelectListItem> MakeDropdownList(this int minVal, int maxVal)
        {
            var list = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Value = "",
                    Text = AppResource.SelectOption
                }
            };
            for (var count = minVal; count <= maxVal; count++)
            {
                list.Add(new SelectListItem
                {
                    Value = count.ToString(),
                    Text = count.ToString()
                });
            }
            return list;
        }
    }
}