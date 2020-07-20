using DigoErp.Repository.Edmx;
using DigoErp.Service.Models;
using System;
using System.Linq;
using DigoErp.Service.Enums;
using System.Threading;
using System.Globalization;

namespace DigoErp.Service.Extentions
{
    public static class BillExtensions
    {
        static BillExtensions()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
        }
        public static Bill MapFrom(this Tbl_Bill bill)
        {
            return new Bill
            {
                Id = bill.Id,
                Number = bill.Number,
                VendorId = bill.VendorId,
                VendorName = bill.Tbl_Vendor?.Name ?? string.Empty,
                Amount = bill.Amount,
                BillDate = bill.BillDate,
                DueDate = bill.DueDate,
                Status = bill.Status,
                Created_At = bill.Created_At,
                Updated_At = bill.Updated_At,
                CurrencyId = bill.CurrencyId,
                CurrencyName = bill.Tbl_Currency?.Name ?? string.Empty,
                Notes = bill.Notes,
                OrderNumber = bill.OrderNumber,
                CategoryId = bill.CategoryId,
                CategoryName = bill.Tbl_Category?.Name ?? string.Empty,
                Recurring = bill.Recurring,
                Attachment = bill.Attachment,
                SubTotal = bill.SubTotal,
                Discount = bill.Discount,
                Tax = bill.Tax,
                GrandTotal = bill.GrandTotal,
                Discount_Percentage = bill.Discount_Percentage,
                CurrencySymbol = bill.Tbl_Currency?.Symbol ?? string.Empty,
                SymbolPosition = bill.Tbl_Currency?.SymbolPosition ?? string.Empty,
                Bill_Items = bill.Tbl_Bill_Items?.Select(x => x.MapFrom()).ToList()
            };
        }

        public static Tbl_Bill MapFrom(this Bill bill)
        {
            return new Tbl_Bill
            {
                Id = bill.Id,
                Number = bill.Number,
                VendorId = bill.VendorId,
                Amount = bill.Amount,
                BillDate = bill.BillDate,
                DueDate = bill.DueDate,
                Created_At = bill.Id > 0 ? bill.Created_At : DateTime.Now,
                Updated_At = bill.Id > 0 ? DateTime.Now : default(DateTime?),
                CurrencyId = bill.CurrencyId,
                Notes = bill.Notes,
                OrderNumber = bill.OrderNumber,
                CategoryId = bill.CategoryId,
                Recurring = bill.Recurring,
                Attachment = bill.Attachment,
                SubTotal = bill.SubTotal,
                Discount = bill.Discount,
                Tax = bill.Tax,
                GrandTotal = bill.GrandTotal,
                Discount_Percentage = bill.Discount_Percentage,
                Status = string.IsNullOrEmpty(bill.Status) ? bill.Status : InvoiceStatus.DRAFT.ToString(),
                Tbl_Bill_Items = bill.Bill_Items?.Select(x => x.MapFrom(bill.Id)).ToList()
            };
        }
    }
}
