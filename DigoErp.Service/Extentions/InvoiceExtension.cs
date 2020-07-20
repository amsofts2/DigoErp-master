using DigoErp.Repository.Edmx;
using DigoErp.Service.Models;
using System;
using System.Linq;
using DigoErp.Service.Enums;

namespace DigoErp.Service.Extentions
{
    public static class InvoiceExtension
    {
        public static Invoice MapFrom(this Tbl_Invoice invoice)
        {
            return new Invoice
            {
                Id = invoice.Id,
                CustomerId = invoice.CustomerId,
                CustomerName = invoice.Tbl_Customer?.Name ?? string.Empty,
                CurrencyId = invoice.CurrencyId,
                CurrencyName = invoice.Tbl_Currency?.Name ?? string.Empty,
                InvoiceDate = invoice.InvoiceDate,
                DueDate = invoice.DueDate,
                InvoiceNumber = invoice.InvoiceNumber,
                OrderNumber = invoice.OrderNumber,
                Notes = invoice.Notes,
                Created_At = invoice.Created_At,
                Updated_At = invoice.Updated_At,
                Footer = invoice.Footer,
                CategoryId = invoice.CategoryId,
                CategoryName = invoice.Tbl_Category?.Name ?? string.Empty,
                Recurring = invoice.Recurring,
                Attachment = invoice.Attachment,
                SubTotal = invoice.SubTotal,
                Discount = invoice.Discount,
                Tax = invoice.Tax,
                GrandTotal = invoice.GrandTotal,
                Status = invoice.Status,
                CurrencySymbol = invoice.Tbl_Currency?.Symbol ?? string.Empty,
                SymbolPosition = invoice.Tbl_Currency?.SymbolPosition ?? string.Empty,
                Discount_Percentage = invoice.Discount_Percentage,
                InvoiceItems = invoice.Tbl_InvoiceItems?.Select(i => i.MapFrom()).ToList()
            };
        }

        public static Tbl_Invoice MapFrom(this Invoice invoice)
        {
            return new Tbl_Invoice
            {
                Id = invoice.Id,
                CustomerId = invoice.CustomerId,
                CurrencyId = invoice.CurrencyId,
                InvoiceDate = invoice.InvoiceDate,
                DueDate = invoice.DueDate,
                InvoiceNumber = invoice.InvoiceNumber,
                OrderNumber = invoice.OrderNumber,
                Created_At = invoice.Id > 0 ? invoice.Created_At : DateTime.Now,
                Updated_At = invoice.Id > 0 ? DateTime.Now : default(DateTime?),
                Notes = invoice.Notes,
                Footer = invoice.Footer,
                CategoryId = invoice.CategoryId,
                Recurring = invoice.Recurring,
                Attachment = invoice.Attachment,
                SubTotal = invoice.SubTotal,
                Discount = invoice.Discount,
                Tax = invoice.Tax,
                GrandTotal = invoice.GrandTotal,
                Status = string.IsNullOrEmpty(invoice.Status) ? invoice.Status : InvoiceStatus.DRAFT.ToString(),
                Discount_Percentage = invoice.Discount_Percentage,
                Tbl_InvoiceItems = invoice.InvoiceItems?.Select(i => i.MapFrom(invoice.Id)).ToList()
            };
        }
    }
}
