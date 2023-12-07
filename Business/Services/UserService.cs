using Business.Models;
using DataAccess.Contexts;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Business;

public interface IUserService
{
    IQueryable<UserModel> Query();
    bool Add(UserModel model);
    bool Update(UserModel model);

    bool Delete(int id);
    bool DeleteUser(int id);
}

public class UserService : IUserService // UserService is a IUserService (UserService implements IUserService)
{
    #region Db Constructor Injection
    private readonly Db _db;

   
    public UserService(Db db)
    {
        _db = db;
    }
    #endregion

    // method implementations of the method definitions in the interface
    public IQueryable<UserModel> Query()
    {
       
        return _db.Users
            .Select(e => new UserModel()
            {
                // model - entity property assignments
                Id = e.Id,
                Name = e.Name,
                Password = e.Password
            });
    }

    public bool Add(UserModel model)
    {
       
        if (_db.Users.Any(u => u.Name.ToUpper() == model.Name.ToUpper().Trim()))
            return false;

        // entity creation from the model
        User user = new User()
        {
          
            Name = model.Name.Trim(),
            Password = model.Password.Trim(),

        };

        // adding entity to the related db set
        _db.Users.Add(user);

        // changes in all of the db sets are commited to the database with Unit of Work
        _db.SaveChanges();

        return true;
    }

    public bool Update(UserModel model)
    {
       
        var existingUsers = _db.Users.Where(u => u.Id != model.Id).ToList();
        if (existingUsers.Any(u => u.Name.Equals(model.Name.Trim(), StringComparison.OrdinalIgnoreCase)))
            return false;

        // first getting the entity to be updated from the db set
        var user = _db.Users.SingleOrDefault(u => u.Id == model.Id);

        // then updating the entity properties
        if (user is not null) { 
       
            user.Name = model.Name.Trim();
            user.Password = model.Password.Trim();

            // updating the entity in the related db set
            _db.Users.Update(user);

            // changes in all of the db sets are commited to the database with Unit of Work
            _db.SaveChanges();
        }
        return true;
    }


    public bool Delete(int id)
    {
        var UserResourceEntities = _db.Users.Where(ur => ur.Id == id).ToList();

        _db.Users.RemoveRange(UserResourceEntities);

        var userEntity = _db.Users.SingleOrDefault(u => u.Id == id);

        if (userEntity is null)
        {
            return false;
        }
        _db.Users.RemoveRange(userEntity);

        _db.SaveChanges();

        return true;
    }

    public bool DeleteUser(int id)
    {
        var userEntity = _db.Users.SingleOrDefault(u => u.Id == id);
        if (userEntity is null)
        {
            return false;
        }
        
        _db.Users.Remove(userEntity);
        _db.SaveChanges();

        return true;
    }
}

