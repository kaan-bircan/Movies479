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
	public interface IDirectorService
	{
		IQueryable<DirectorModel> Query();

		bool Add(DirectorModel model);
		bool Update(DirectorModel model);

		bool Delete(int id);
	}
	public class DirectorService : IDirectorService
	{
		private readonly Db _db;

		public DirectorService(Db db)
		{
			_db = db;
		}

		public bool Add(DirectorModel model)
		{
			if (_db.Directors.Any(s => s.Name.ToUpper() == model.Name.ToUpper().Trim()))
				return false;

			Director entity = new Director()
			{
			   Id = model.Id,
               Name = model.Name,
			   Surname = model.Surname,
			   BirthDate = model.BirthDate,
			   IsRetired = model.IsRetired
			};

			_db.Directors.Add(entity);
			_db.SaveChanges();
			return true;
		}

		public bool Delete(int id)
		{
            Director entity = _db.Directors.SingleOrDefault(s => s.Id == id);
            if (entity is null)
                return false;
            _db.Directors.Remove(entity);
            _db.SaveChanges();
            return true;
        }

		public IQueryable<DirectorModel> Query()
		{
			return _db.Directors
		   .OrderByDescending(e => e.Name)
		   .Select(e => new DirectorModel()
		   {
			   Id = e.Id,
			   Name = e.Name,
			   Surname = e.Surname,
			   BirthDate = e.BirthDate,
			   IsRetired = e.IsRetired,

			   IsRetiredOutput  = e.IsRetired ? "Retired" : "Not Retired",
		   });
		}

		public bool Update(DirectorModel model)
		{
            if (_db.Directors.Any(s => s.Name.ToUpper() == model.Name.ToUpper().Trim() && s.Id != model.Id))
                return false;
            Director existingEntity = _db.Directors.SingleOrDefault(s => s.Id == model.Id);
            if (existingEntity is null)
                return false;
            existingEntity.Id = model.Id;
			existingEntity.Name = model.Name;
			existingEntity.Surname = model.Surname;
			existingEntity.IsRetired = model.IsRetired;

            _db.Directors.Update(existingEntity);
            _db.SaveChanges();
            return true;
        }
	}
}
