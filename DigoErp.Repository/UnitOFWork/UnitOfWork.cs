using DigoErp.Repository.Edmx;
using DigoErp.Repository.Exceptions;
using DigoErp.Resources.App_Resources;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;

namespace DigoErp.Repository.UnitOFWork
{
    public class UnitOfWork : IDisposable
    {
        private bool disposed = false;
        private readonly DigoErp_Db context = new DigoErp_Db();


        private GenericRepository<Tbl_User> userRepository;
        private GenericRepository<Tbl_Branch> branchRepository;
        private GenericRepository<Tbl_Role> roleRepository;
        private GenericRepository<Tbl_Customer> customerRepository;
        private GenericRepository<Tbl_Item> itemRepository;
        private GenericRepository<Tbl_Bill> billRepository;
        private GenericRepository<Tbl_Bill_Item> bill_ItemRepository;
        private GenericRepository<Tbl_Vendor> vendorRepository;
        private GenericRepository<Tbl_Category> categoryRepository;
        private GenericRepository<Tbl_Currency> currencyRepository;
        private GenericRepository<Tbl_Transaction> paymentRepository;
        private GenericRepository<Tbl_Account> accountRepository;
        private GenericRepository<Tbl_Tax> taxRepository;
        private GenericRepository<Tbl_Transfer> transfersRepository;
        private GenericRepository<Tbl_Transaction> revenueRepository;
        private GenericRepository<Tbl_Transaction> transactionRepository;
        private GenericRepository<Tbl_Invoice> invoiceRepository;
        private GenericRepository<Tbl_InvoiceItem> invoiceitemRepository;
        private GenericRepository<Tbl_ItemUnit> itemUnitRepository;
        private GenericRepository<Tbl_Default> defaultRepository;
        private GenericRepository<Tbl_Reconciliation> reconciliationRepository;
        private GenericRepository<Tbl_Reconciliation_Transaction> reconciliationTransactionRepository;

        public GenericRepository<Tbl_User> UserRepository
        {
            get
            {
                if (this.userRepository == null)
                {
                    this.userRepository = new GenericRepository<Tbl_User>(context);
                }
                return userRepository;
            }
        }


        public GenericRepository<Tbl_Branch> BranchRepository
        {
            get
            {
                if (this.branchRepository == null)
                {
                    this.branchRepository = new GenericRepository<Tbl_Branch>(context);
                }
                return branchRepository;
            }
        }

        public GenericRepository<Tbl_Role> RoleRepository
        {
            get
            {
                if (this.roleRepository == null)
                {
                    this.roleRepository = new GenericRepository<Tbl_Role>(context);
                }
                return roleRepository;
            }
        }


        public GenericRepository<Tbl_Customer> CustomerRepository
        {
            get
            {
                if (this.customerRepository == null)
                {
                    this.customerRepository = new GenericRepository<Tbl_Customer>(context);
                }
                return customerRepository;
            }
        }

        public GenericRepository<Tbl_Item> ItemRepository
        {
            get
            {
                if (this.itemRepository == null)
                {
                    this.itemRepository = new GenericRepository<Tbl_Item>(context);
                }
                return itemRepository;
            }
        }

        public GenericRepository<Tbl_Bill> BillRepository
        {
            get
            {
                if (this.billRepository == null)
                {
                    this.billRepository = new GenericRepository<Tbl_Bill>(context);
                }
                return billRepository;
            }
        }

        public GenericRepository<Tbl_Bill_Item> Bill_ItemRepository
        {
            get
            {
                if (this.bill_ItemRepository == null)
                {
                    this.bill_ItemRepository = new GenericRepository<Tbl_Bill_Item>(context);
                }
                return bill_ItemRepository;
            }
        }

        public GenericRepository<Tbl_Vendor> VendorRepository
        {
            get
            {
                if (this.vendorRepository == null)
                {
                    this.vendorRepository = new GenericRepository<Tbl_Vendor>(context);
                }
                return vendorRepository;
            }
        }

        public GenericRepository<Tbl_Category> CategoryRepository
        {
            get
            {
                if (this.categoryRepository == null)
                {
                    this.categoryRepository = new GenericRepository<Tbl_Category>(context);
                }
                return categoryRepository;
            }
        }

        public GenericRepository<Tbl_Currency> CurrencyRepository
        {
            get
            {
                if (this.currencyRepository == null)
                {
                    this.currencyRepository = new GenericRepository<Tbl_Currency>(context);
                }
                return currencyRepository;
            }
        }

        public GenericRepository<Tbl_Account> AccountRepository
        {
            get
            {
                if (this.accountRepository == null)
                {
                    this.accountRepository = new GenericRepository<Tbl_Account>(context);
                }
                return accountRepository;
            }
        }

        public GenericRepository<Tbl_Transaction> PaymentRepository
        {
            get
            {
                if (this.paymentRepository == null)
                {
                    this.paymentRepository = new GenericRepository<Tbl_Transaction>(context);
                }
                return paymentRepository;
            }
        }

        public GenericRepository<Tbl_Tax> TaxRepository
        {
            get
            {
                if (this.taxRepository == null)
                {
                    this.taxRepository = new GenericRepository<Tbl_Tax>(context);
                }
                return taxRepository;
            }
        }

        public GenericRepository<Tbl_Transfer> TransfersRepository
        {
            get
            {
                if (this.transfersRepository == null)
                {
                    this.transfersRepository = new GenericRepository<Tbl_Transfer>(context);
                }
                return transfersRepository;
            }
        }

        public GenericRepository<Tbl_Transaction> RevenueRepository
        {
            get
            {
                if (this.revenueRepository == null)
                {
                    this.revenueRepository = new GenericRepository<Tbl_Transaction>(context);
                }
                return revenueRepository;
            }
        }

        public GenericRepository<Tbl_Transaction> TransactionRepository
        {
            get
            {
                if (this.transactionRepository == null)
                {
                    this.transactionRepository = new GenericRepository<Tbl_Transaction>(context);
                }
                return transactionRepository;
            }
        }

        public GenericRepository<Tbl_Invoice> InvoiceRepository
        {
            get
            {
                if (this.invoiceRepository == null)
                {
                    this.invoiceRepository = new GenericRepository<Tbl_Invoice>(context);
                }
                return invoiceRepository;
            }
        }

        public GenericRepository<Tbl_InvoiceItem> InvoiceItemRepository
        {
            get
            {
                if (this.invoiceitemRepository == null)
                {
                    this.invoiceitemRepository = new GenericRepository<Tbl_InvoiceItem>(context);
                }
                return invoiceitemRepository;
            }
        }

        public GenericRepository<Tbl_ItemUnit> ItemUnitRepository
        {
            get
            {
                if (this.itemUnitRepository == null)
                {
                    this.itemUnitRepository = new GenericRepository<Tbl_ItemUnit>(context);
                }
                return itemUnitRepository;
            }
        }

        public GenericRepository<Tbl_Default> DefaultRepository
        {
            get
            {
                if (this.defaultRepository == null)
                {
                    this.defaultRepository = new GenericRepository<Tbl_Default>(context);
                }
                return defaultRepository;
            }
        }

        public GenericRepository<Tbl_Reconciliation> ReconciliationRepository
        {
            get
            {
                if (this.reconciliationRepository == null)
                {
                    this.reconciliationRepository = new GenericRepository<Tbl_Reconciliation>(context);
                }
                return reconciliationRepository;
            }
        }

        public GenericRepository<Tbl_Reconciliation_Transaction> ReconciliationTransactionRepository
        {
            get
            {
                if (this.reconciliationTransactionRepository == null)
                {
                    this.reconciliationTransactionRepository = new GenericRepository<Tbl_Reconciliation_Transaction>(context);
                }
                return reconciliationTransactionRepository;
            }
        }

        public void Save()
        {
            try
            {
                context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                var message = AppResource.GetErrorValidation + " (";
                ex.EntityValidationErrors.SelectMany(error => error.ValidationErrors).ToList().ForEach(item => { message += " column name: " + item.PropertyName + " " + "ErrorMessage " + item.ErrorMessage; });
                message += ")";
                var e = new DigoErpException(message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var message = AppResource.GetErrorConcurrency;
                var e = new DigoErpException(message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;
            }
            catch (DbUpdateException ex)
            {
                var message = AppResource.GetErrorDB;
                var e = new DigoErpException(message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;
            }
            catch (NotSupportedException ex)
            {
                var message = AppResource.GetErrorNotSupported;
                var e = new DigoErpException(message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;
            }
            catch (ObjectDisposedException ex)
            {
                var message = AppResource.GetErrorDisposed;
                var e = new DigoErpException(message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;
            }
            catch (InvalidOperationException ex)
            {
                var message = AppResource.GetErrorInvalidOperation;
                var e = new DigoErpException(message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;
            }
            catch (Exception ex)
            {
                var e = new DigoErpException(ex.Message, (int)SystemExceptions.Err_SavingDataDBFailure, ex);
                throw e;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            disposed = true;
        }

        public Tbl_Item GetItem()
        {
            return context.Tbl_Item.FirstOrDefault();
        }
    }
}
