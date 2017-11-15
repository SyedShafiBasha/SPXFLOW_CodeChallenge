#region Using Namespaces...

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Data.Entity.Validation;
using DataModel.GenericRepository;

#endregion

namespace DataModel.UnitOfWork
{

    public class UnitOfWork : IDisposable
    {
        private Product_DBEntities _context = null;
        private Repository<Product> _productRepository;
        private Repository<Review> _reviewRepository;


        public UnitOfWork()
        {
            _context = new Product_DBEntities();
        }

        public Repository<Product> ProductRepository
        {
            get
            {
                if (this._productRepository == null)
                    this._productRepository = new Repository<Product>(_context);
                return _productRepository;
            }
        }

        public Repository<Review> ReviewRepository
        {
            get
            {
                if (this._reviewRepository == null)
                    this._reviewRepository = new Repository<Review>(_context);
                return _reviewRepository;
            }
        }
        public void Save()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {

                var outputLines = new List<string>();
                foreach (var eve in e.EntityValidationErrors)
                {
                    outputLines.Add(string.Format("{0}: Entity of type \"{1}\" in state \"{2}\" has the following validation errors:", DateTime.Now, eve.Entry.Entity.GetType().Name, eve.Entry.State));
                    foreach (var ve in eve.ValidationErrors)
                    {
                        outputLines.Add(string.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage));
                    }
                }
                System.IO.File.AppendAllLines(@"C:\errors.txt", outputLines);

                throw e;
            }

        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    Debug.WriteLine("UnitOfWork is being disposed");
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}