using DigoErp.Repository.Edmx;
using DigoErp.Service.Models;
using System;

namespace DigoErp.Service.Extentions
{
    public static class InvoiceItemExtension
    {
        public static InvoiceItem MapFrom(this Tbl_InvoiceItem invoice)
        {
            return new InvoiceItem
            {
                Id = invoice.Id,
                InvoiceId = invoice.InvoiceId,
                ItemId = invoice.ItemId,
                Name = invoice.Name,
                Quantity = invoice.Quantity,
                Price = invoice.Price,
                TaxId = invoice.TaxId,
                Tax = invoice.Tax,
                Created_At = invoice.Created_At,
                Updated_At = invoice.Updated_At,
            };
        }

        public static Tbl_InvoiceItem MapFrom(this InvoiceItem invoice,long? invoiceId)
        {
            return new Tbl_InvoiceItem
            {
                Id = invoice.Id,
                InvoiceId = invoiceId > 0 ? invoiceId : invoice.InvoiceId,
                ItemId = invoice.ItemId,
                Name = invoice.Name,
                Quantity = invoice.Quantity,
                Price = invoice.Price,
                TaxId = invoice.TaxId,
                Tax = invoice.Tax,
                Created_At = invoice.Id <= 0 ? DateTime.Now : default(DateTime?),
                Updated_At = invoice.Id > 1 ? DateTime.Now : default(DateTime?)
            };
        }
    }
}
