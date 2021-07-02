using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IGenericRepository<Category> _categoryRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly StoreContext _context;
        public CategoryService(IGenericRepository<Category> categoryRepo, IUnitOfWork unitOfWork, StoreContext context)
        {
            _context = context;
            _unitOfWork = unitOfWork;
            _categoryRepo = categoryRepo;
        }
         public async Task<IEnumerable<Category>> ListAllAsync2(QueryParameters queryParameters)
        {

            IQueryable<Category> list = _context.Categories.AsQueryable()
                                      .OrderBy(p => p.CategoryName);

            if (queryParameters.HasQuery())
            {
                list = list
                .Where(t => t.CategoryName
                .Contains(queryParameters.Query));
            }

            return await list.ToListAsync();

        }
        public async Task CreateCategoryAsync(Category category)
        {
            try
            {
                _unitOfWork.Repository<Category>().Add(category);
                var result = await _unitOfWork.Complete();
            }
            catch
            {
                throw;
            }
        }
        public async Task UpdateCategoryAsync(Category category)
        {
            try
            {
                _categoryRepo.Update(category);
                var result = await _unitOfWork.Complete();
            }
            catch
            {
                throw;
            }
        }
        public async Task DeleteCategory(Category category)
        {
            try
            {
                _categoryRepo.Delete(category);
                var result = await _unitOfWork.Complete();
            }
            catch
            {
                throw;
            }
        }
        public async Task<IEnumerable<Category>> ListAllAsync()
        {
            return await _unitOfWork.Repository<Category>().ListAllAsync();
        }
        public async Task<IEnumerable<Category>> ListAllAsync1(QueryParameters queryParameters)
        {
            return await _unitOfWork.Repository<Category>().ListAllAsync();
        }
        public async Task<Category> FindCategoryByIdAsync(int id)
        {
            return await _categoryRepo.GetByIdAsync(id);
        }

    }
}


