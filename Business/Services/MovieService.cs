using Business.Models;
using DataAccess.Contexts;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public interface IMovieService
    {
        IQueryable<MovieModel> Query();

        bool Add(MovieModel model);
        bool Update(MovieModel model);

        bool Delete(int id);
    }
    public class MovieService : IMovieService
    {
        private readonly Db _db;

        public MovieService(Db db)
        {
            _db = db;
        }

        public bool Add(MovieModel model)
        {
			if (_db.Movies.Any(s => s.Name.ToUpper() == model.Name.ToUpper().Trim()))
				return false;
            
            Movie entity = new Movie(){
             
                Id = model.Id,
                Name = model.Name,
                Revenue = model.Revenue,
                Year = model.Year,
                DirectorId = model.DirectorId,
                Guid = model.Guid.ToString()
			};
			
            _db.Movies.Add(entity);
			_db.SaveChanges();
			return true;
		}

        public bool Delete(int id)
        {
			Movie entity = _db.Movies.SingleOrDefault(s => s.Id == id);
			if (entity is null)
				return false;
			_db.Movies.Remove(entity);
			_db.SaveChanges();
			return true;
		}

        public IQueryable<MovieModel> Query()
        {
            return _db.Movies.OrderByDescending(e => e.Year)
            .ThenBy(e => e.Name)
            .Select(e => new MovieModel()
            {
                // model - entity property assignments
                Id = e.Id,
                Name = e.Name,
                Year = e.Year,
                Revenue = e.Revenue,
                DirectorId = e.DirectorId
            });
        }

        public bool Update(MovieModel model)
        {
			if (_db.Movies.Any(s => s.Name.ToUpper() == model.Name.ToUpper().Trim() && s.Id != model.Id))
				return false;
			Movie existingEntity = _db.Movies.SingleOrDefault(s => s.Id == model.Id);
			if (existingEntity is null)
				return false;
			existingEntity.Id = model.Id;
			existingEntity.Name = model.Name.Trim();
			existingEntity.Year = model.Year;
            existingEntity.Revenue = model.Revenue;
            existingEntity.DirectorId = model.DirectorId;
			_db.Movies.Update(existingEntity);
			_db.SaveChanges();
			return true;
		}
    }
}
