using Business.Models;
using Business.Results;
using Business.Results.Bases;
using DataAccess.Contexts;
using DataAccess.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Business.Services
{
    public interface IRoleService
    {
        IQueryable<RoleModel> Query();
        Result Add(RoleModel model);
        Result Update(RoleModel model);
        Result Delete(int id);
    }

    public class RoleService : IRoleService
    {
        private readonly Db _db;

        public RoleService(Db db)
        {
            _db = db;
        }

        public IQueryable<RoleModel> Query()
        {
            return _db.Roles.Include(r => r.Users).OrderBy(r => r.Name).Select(r => new RoleModel()
            {
                // model - entity property assignments
                Id = r.Id,
                Name = r.Name,

                // modified model - entity property assignments for displaying in views
                UserCountOutput = r.Users.Count // display the user count for each role
            });
        }

        public Result Add(RoleModel model)
        {
      
            var nameSqlParameter = new SqlParameter("name", model.Name.Trim()); // using a parameter prevents SQL Injection
            // we provide SQL parameters to the SQL query as the second and rest parameters for the FromSqlRaw method
            // according to their usage order in the SQL query
            var query = _db.Roles.FromSqlRaw("select * from Roles where UPPER(Name) = UPPER(@name)", nameSqlParameter);
            if (query.Any()) // if there are any results for the query above
                return new ErrorResult("Role with the same name already exists!");

            var entity = new Role()
            {
                Name = model.Name.Trim()
            };
            _db.Roles.Add(entity);
            _db.SaveChanges();
            return new SuccessResult("Role added successfully.");
        }

        public Result Update(RoleModel model)
        {

            var nameSqlParameter = new SqlParameter("name", model.Name.Trim()); // using a parameter prevents SQL Injection
            var idSqlParameter = new SqlParameter("id", model.Id);
            // we provide SQL parameters to the SQL query as the second and rest parameters for the FromSqlRaw method
            // according to their usage order in the SQL query
            var query = _db.Roles.FromSqlRaw("select * from Roles where UPPER(Name) = UPPER(@name) and Id != @id", nameSqlParameter, idSqlParameter);
            if (query.Any()) // if there are any results for the query above
                return new ErrorResult("Role with the same name already exists!");

            // Way 1: retreiving entity from the related database table and updating its properties
            //var entity = _db.Roles.Find(model.Id); // SingleOrDefault can also be used
            //if (entity is null)
            //    return new ErrorResult("Role not found!");
            //entity.Name = model.Name.Trim();
            // Way 2: creating an entity with the model id and setting its properties
            var entity = new Role()
            {
                Id = model.Id, // must be set
                Name = model.Name.Trim()
            };

            // then updating the entity in the related database table
            _db.Roles.Update(entity);
            _db.SaveChanges();
            return new SuccessResult("Role updated successfully.");
        }

        public Result Delete(int id)
        {
            // getting the role entity with relational user entities by role id from the related database table
            var existingEntity = _db.Roles.Include(r => r.Users).SingleOrDefault(r => r.Id == id);
            if (existingEntity is null)
                return new ErrorResult("Role not found!");

            if (existingEntity.Users.Any())
                return new ErrorResult("Role can't be deleted because it has users!");

            // since there is no relational user entities of the role entity, we can delete it
            _db.Roles.Remove(existingEntity);
            _db.SaveChanges();
            return new SuccessResult("Role deleted successfully.");
        }
    }
}