using DigoErp.Repository.Edmx;
using DigoErp.Service.Models;
using System;
using System.Globalization;
using System.Threading;

namespace DigoErp.Service.Extentions
{
    public static class Bill_Item_Extentions
    {
        static Bill_Item_Extentions()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
        }
        public static Tbl_Bill_Item MapFrom(this Bill_Item bill_Item,long? billId)
        {
            return new Tbl_Bill_Item
            {
                Id = bill_Item.Id,
                Bill_Id = billId > 0 ? billId : bill_Item.Bill_Id,
                ItemId = bill_Item.ItemId,
                Name = bill_Item.Name,
                Quantity = bill_Item.Quantity,
                Price = bill_Item.Price,
                TaxId = bill_Item.TaxId,
                Tax = bill_Item.Tax,
                Created_At = bill_Item.Id < 0 ? DateTime.Now : default(DateTime?),
                Updated_At = bill_Item.Id > 0 ? DateTime.Now : default(DateTime?),
            };
        }

        public static Bill_Item MapFrom(this Tbl_Bill_Item bill_Item)
        {
            return new Bill_Item
            {
                Id = bill_Item.Id,
                Bill_Id = bill_Item.Bill_Id,
                ItemId = bill_Item.ItemId,
                Name = bill_Item.Name,
                Quantity = bill_Item.Quantity,
                Price = bill_Item.Price,
                TaxId = bill_Item.TaxId,
                Tax = bill_Item.Tax,
                Created_At = bill_Item.Created_At,
                Updated_At = bill_Item.Updated_At,
            };
        }
    }
}
