using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
namespace Business.Concrete
{
	public class ProductManager : IProductService
	{
		IProductDal _productDal;
		public ProductManager(IProductDal productDal)
		{
			_productDal = productDal;
		}


		[ValidationAspect(typeof(ProductValidator))]
		public IResult Add(Product product)
		{
			//validation kodları yazmışdıq oları validationtoola atdıq
			//business codes


			_productDal.Add(product);
			return new SuccessResult(Massages.ProductAdded);
		}


		public IDataResult<List<Product>> GetAll()
		{
			//if (DateTime.Now.Hour == 22)
			//{
				//return new ErrorDataResult<List<Product>>(Massages.MaintenanceTime);
			//}
			return new SuccessDataResult<List<Product>>(_productDal.GetAll(), Massages.ProductsListed);

		}

		public IDataResult<List<Product>> GetAllByCategoryId(int id)
		{

			return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.CategoryId == id));

		}

		public IDataResult<Product> GetById(int productId)
		{
			return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductId == productId));
		}

		public IDataResult<List<Product>> GetByUnitProce(decimal min, decimal max)
		{
			return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max));
		}

		public IDataResult<List<ProductDetailDto>> GetProductDetails()
		{
			return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetails());
		}
	}
}
