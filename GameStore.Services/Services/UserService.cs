using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Helpers;
using GameStore.Domain;
using GameStore.Domain.Models;
using GameStore.Domain.Models.SqlModels.AccountModels;
using GameStore.Interfaces.DAL.Data;
using GameStore.Interfaces.DAL.RepositorySql;
using GameStore.Interfaces.Services;

namespace GameStore.Services.Services
{
    public class UserService : IUserService
    {
        private readonly ICrudRepository<User> _userRepository;
        private readonly ICrudRepository<Role> _roleRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _roleRepository = unitOfWork.GetRepository<ICrudRepository<Role>>(RepositoryType.SQL);
            _unitOfWork = unitOfWork;
            _userRepository = unitOfWork.GetRepository<ICrudRepository<User>>(RepositoryType.SQL);
        }

        public void Register(string email, string password)
        {
            var userId = Guid.NewGuid();

            var user = new User
            {
                Id = userId,
                Name = email,
                Email = email,
                Password = Crypto.HashPassword(password),
                Roles = new List<UserRole>()
            };

            _userRepository.Insert(user);
            _unitOfWork.Commit();

            AddUserRole(user.Email);
        }

        public void ChangeRole(string email, IEnumerable<string> roles)
        {
            var user = _userRepository.FirstOrDefault(x => x.Email == email, x => x.Roles);

            if (roles == null)
            {
                return;
            }

            if (user == null)
            {
                return;
            }

            var newRoles = new List<UserRole>();

            foreach (string role in roles)
            {
                var roleEntity = _roleRepository.FirstOrDefault(x => x.Name == role);

                newRoles.Add(new UserRole { Role = roleEntity, User = user });
            }

            user.Roles = newRoles;

            _userRepository.Update(user);
            _unitOfWork.Commit();
        }

        public void CreateRole(string role)
        {
            if (!String.IsNullOrEmpty(role))
            {
                var roleEntity = new Role
                {
                    Id = Guid.NewGuid(),
                    Name = role
                };

                _roleRepository.Insert(roleEntity);
                _unitOfWork.Commit();
            }
        }

        public void DeleteRole(string role)
        {
            var roleEntity = _roleRepository.FirstOrDefault(x => x.Name == role);

            if (roleEntity == null)
            {
                return;
            }

            _roleRepository.Delete(roleEntity);
            _unitOfWork.Commit();
        }

        public void DeleteUser(string email)
        {
            var user = _userRepository.FirstOrDefault(x => x.Email == email);

            if (user == null)
            {
                return;
            }

            user.IsDeleted = true;

            _userRepository.Update(user);
            _unitOfWork.Commit();
        }

        public void RestoreUser(string email)
        {
            var user = _userRepository.FirstOrDefault(x => x.Email == email);

            if (user == null)
            {
                return;
            }

            user.IsDeleted = false;

            _userRepository.Update(user);
            _unitOfWork.Commit();
        }

        public bool IsLastAdmin(string email)
        {
            var adminEntity = _roleRepository.FirstOrDefault(x => x.Name == RoleName.Admin);
            var adminCount = _unitOfWork.GetRepository<ICrudRepository<UserRole>>()
                                        .Count(g => g.User.IsDeleted == false && g.Role.Id == adminEntity.Id);

            return adminCount == 1;
        }

        public void EditRole(Guid roleId, string role)
        {
            var roleEntity = _roleRepository.FirstOrDefault(x => x.Id == roleId);

            if (roleEntity != null)
            {
                roleEntity.Name = role;

                _roleRepository.Update(roleEntity);
                _unitOfWork.Commit();
            }
        }

        public User GetUserByEmail(string email)
        {
            var user = _userRepository.FirstOrDefault(x => x.Email == email, x => x.Roles);

            if (user != null)
            {
                var userRoles = user.Roles.Select(userRole =>
                {
                    var role = _roleRepository.FirstOrDefault(x => x.Id == userRole.RoleId);

                    userRole.Role = role;

                    return userRole;
                }).ToList();

                user.Roles = userRoles;
            }

            return user;
        }

        public User GetUserById(Guid userId)
        {
            var user = _userRepository.FirstOrDefault(x => x.Id == userId);

            return user;
        }

        public Role GetRoleByName(string role)
        {
            var roleEntity = _roleRepository.FirstOrDefault(x => x.Name == role);

            return roleEntity;
        }

        public IEnumerable<User> GetAllUsers()
        {
            var users = _userRepository.GetMany(includes: x => x.Roles);

            foreach (var user in users)
            {
                var userRoles = user.Roles.Select(x =>
                {
                    var role = _roleRepository.FirstOrDefault(y => y.Id == x.RoleId);

                    x.Role = role;

                    return x;
                }).ToList();

                user.Roles = userRoles;
            }

            return users;
        }

        public IEnumerable<Role> GetAllRoles()
        {
            var roles = _roleRepository.GetMany();

            return roles;
        }

        public IEnumerable<string> GetRoleNames()
        {
            var roleNames = _roleRepository.GetMany().Select(x => x.Name);

            return roleNames;
        }

        private void AddUserRole(string email)
        {
            var user = _userRepository.FirstOrDefault(x => x.Email == email);
            var userRole = _roleRepository.FirstOrDefault(x => x.Name == RoleName.User);

            if (user != null)
            {
                user.Roles.Add(new UserRole { RoleId = userRole.Id, UserId = user.Id });

                _userRepository.Update(user);
            }

            _unitOfWork.Commit();
        }
    }
}