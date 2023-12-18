using Business.Models;
using Business.Results;
using Business.Results.Bases;
using DataAccess.Contexts;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Business;

public interface IUserService
{
    IQueryable<UserModel> Query();
    Result Add(UserModel model);
    Result Update(UserModel model);

    Result Delete(int id);
    Result DeleteUser(int id);
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

    public Result Add(UserModel model)
    {
        bool exist = _db.Users.Any(u => u.Name.ToUpper() == model.Name.ToUpper().Trim());
        if (exist)
            return new ErrorResult("User with the same user name already exists!");

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

        return new SuccessResult("User added successfuly");
    }

    public Result Update(UserModel model)
    {

        var existingUsers = _db.Users.Where(u => u.Id != model.Id).ToList();
        if (existingUsers.Any(u => u.Name.Equals(model.Name.Trim(), StringComparison.OrdinalIgnoreCase)))
            return new ErrorResult("User with the same user name already exists!");

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
        return new SuccessResult("User updated successfully.");
    }


    public Result Delete(int id)
    {
        var UserResourceEntities = _db.Users.Where(ur => ur.Id == id).ToList();

        _db.Users.RemoveRange(UserResourceEntities);

        var userEntity = _db.Users.SingleOrDefault(u => u.Id == id);

        if (userEntity is null)
        {
            return new ErrorResult("User not found.");
        }
        _db.Users.RemoveRange(userEntity);

        _db.SaveChanges();

        return new SuccessResult("User deleted successfuly.");
    }

    public Result DeleteUser(int id)
    {
        var userEntity = _db.Users.SingleOrDefault(u => u.Id == id);
        if (userEntity is null)
        {
            return new ErrorResult("User not found.");
        }
        
        _db.Users.Remove(userEntity);
        _db.SaveChanges();

        return new SuccessResult("User deleted successfuly.");
    }

   
}

