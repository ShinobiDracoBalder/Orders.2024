using Microsoft.EntityFrameworkCore;
using Orders.Backend.Data;
using Orders.Backend.Intertfaces;
using Orders.Shared.Entities;
using Orders.Shared.Responses;

namespace Orders.Backend.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DataContext _context;
        private readonly DbSet<T> _entity;
        public GenericRepository(DataContext context)
        {
            _context = context;
            _entity = context.Set<T>();
        }

        public async Task<JsonResponseOjbect<T>> AddAsync(T entity)
        {
            _context.Add(entity);
            try
            {
                await _context.SaveChangesAsync();
                return new JsonResponseOjbect<T>
                {
                    ItsSuccessful = true,
                    ResultModel = entity
                };
            }
            catch (DbUpdateException dbUpdateException)
            {
                return DbUpdateExceptionResponse(dbUpdateException);
            }
            catch (Exception exception)
            {
                return ExceptionResponse(exception);
            }
        }

        public async Task<JsonResponseOjbect<T>> DeleteAsync(int id)
        {
            var row = await _entity.FindAsync(id);
            if (row != null)
            {
                _entity.Remove(row);
                await _context.SaveChangesAsync();
                return new JsonResponseOjbect<T>
                {
                    ItsSuccessful = true,
                };
            }
            return new JsonResponseOjbect<T>
            {
                ItsSuccessful = false,
                Message = "Registro no encontrado"
            };
        }

        public async Task<T> GetAsync(int id)
        {
            var row = await _entity.FindAsync(id);
            return row!;
        }

        public async Task<IEnumerable<T>> GetAsync()
        {
            return await _entity.ToListAsync();
        }

        public async Task<JsonResponseOjbect<T>> UpdateAsync(T entity)
        {
            try
            {
                _context.Update(entity);
                await _context.SaveChangesAsync();
                return new JsonResponseOjbect<T>
                {
                    ItsSuccessful = true,
                    ResultModel = entity
                };
            }
            catch (DbUpdateException dbUpdateException)
            {
                return DbUpdateExceptionResponse(dbUpdateException);
            }
            catch (Exception exception)
            {
                return ExceptionResponse(exception);
            }
        }

        private JsonResponseOjbect<T> ExceptionResponse(Exception exception)
        {
            return new JsonResponseOjbect<T>
            {
                ItsSuccessful = false,
                Message = exception.Message
            };
        }

        private JsonResponseOjbect<T> DbUpdateExceptionResponse(DbUpdateException dbUpdateException)
        {
            if (dbUpdateException.InnerException!.Message.Contains("duplicate"))
            {
                return new JsonResponseOjbect<T>
                {
                    ItsSuccessful = false,
                    Message = "Ya existe el registro que estas intentando crear."
                };
            }
            else
            {
                return new JsonResponseOjbect<T>
                {
                    ItsSuccessful = false,
                    Message = dbUpdateException.InnerException.Message
                };
            }
        }

        public async Task<Country> GetCountryAsync(int id)
        {
            var country = await _context.Countries
                //.Include(c => c.States!)
                //.ThenInclude(s => s.Cities)
                .FirstOrDefaultAsync(c => c.Id == id);
            return country!;
        }

        Task<JsonResponseOjbect<T>> IGenericRepository<T>.DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        //public async Task<State> GetStateAsync(int id)
        //{
        //    var state = await _context.States
        //            .Include(s => s.Cities)
        //            .FirstOrDefaultAsync(c => c.Id == id);
        //    return state!;
        //}

    }
}
