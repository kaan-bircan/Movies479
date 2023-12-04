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

    public interface IGenreService
    {
        IQueryable<GenreModel> Query();

        bool Add(GenreModel model);
        bool Update(GenreModel model);

        bool Delete(int id);
    }
    public class GenreService : IGenreService
    {
        private readonly Db _db;

        public GenreService(Db db)
        {
            _db = db;
        }
        public bool Add(MovieModel model)
        {
            if (_db.Genres.Any(s => s.Name.ToUpper() == model.Name.ToUpper().Trim()))
                return false;

            Genre entity = new Genre()
            {

               Id = model.Id,
               Name = model.Name,
               
            };

            _db.Genres.Add(entity);
            _db.SaveChanges();
            return true;
        }

        public bool Add(GenreModel model)
        {
            if (_db.Genres.Any(s => s.Name.ToUpper() == model.Name.ToUpper().Trim()))
                return false;

            Genre entity = new Genre()
            {

                Id = model.Id,
                Name = model.Name,
                
            };

            _db.Genres.Add(entity);
            _db.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            Genre entity = _db.Genres.SingleOrDefault(s => s.Id == id);
            if (entity is null)
                return false;
            _db.Genres.Remove(entity);
            _db.SaveChanges();
            return true;
        }

        public IQueryable<GenreModel> Query()
        {
            return _db.Genres.OrderByDescending(e => e.Name)
            .Select(e => new GenreModel()
            {
                // model - entity property assignments
                Id = e.Id,
                Name = e.Name
            }); ;
        }

        public bool Update(GenreModel model)
        {
            if (_db.Genres.Any(s => s.Name.ToUpper() == model.Name.ToUpper().Trim() && s.Id != model.Id))
                return false;
            Genre existingEntity = _db.Genres.SingleOrDefault(s => s.Id == model.Id);
            if (existingEntity is null)
                return false;
            existingEntity.Id = model.Id;
            existingEntity.Name = model.Name.Trim();
 
            _db.Genres.Update(existingEntity);
            _db.SaveChanges();
            return true;
        }
    }
}
