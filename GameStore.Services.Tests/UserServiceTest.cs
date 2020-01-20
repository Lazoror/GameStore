using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using GameStore.Domain.Models;
using GameStore.Domain.Models.SqlModels.AccountModels;
using GameStore.Domain.Models.SqlModels.SortModels;
using GameStore.Interfaces.DAL.Data;
using GameStore.Interfaces.DAL.RepositorySql;
using GameStore.Services.Services;
using Moq;
using Xunit;

namespace GameStore.Services.Tests
{
    public class UserServiceTest
    {
        private readonly UserService _userService;
        private readonly Mock<ICrudRepository<User>> _userRepository;
        private readonly Mock<ICrudRepository<Role>> _roleRepository;
        private readonly User _user;

        public UserServiceTest()
        {
            InitializeTestData(out _user);

            var unitOfWork = new Mock<IUnitOfWork>();
            _roleRepository = new Mock<ICrudRepository<Role>>();
            _userRepository = new Mock<ICrudRepository<User>>();

            _userRepository.Setup(i => i.FirstOrDefault(It.IsAny<Expression<Func<User, bool>>>()))
                .Returns(_user);
            _roleRepository.Setup(i => i.FirstOrDefault(It.IsAny<Expression<Func<Role, bool>>>()))
                .Returns(new Role());
            _roleRepository.Setup(i => i.GetMany(0, int.MaxValue, It.IsAny<Expression<Func<Role, bool>>>(), null, It.IsAny<SortDirection>()))
                .Returns(new List<Role>());
            _userRepository.Setup(i => i.GetMany(0, int.MaxValue, It.IsAny<Expression<Func<User, bool>>>(), null, It.IsAny<SortDirection>(), It.IsAny<Expression<Func<User, object>>>()))
                .Returns(new List<User> { _user });

            unitOfWork.Setup(u => u.GetRepository<ICrudRepository<User>>(RepositoryType.SQL)).Returns(_userRepository.Object);
            unitOfWork.Setup(u => u.GetRepository<ICrudRepository<Role>>(RepositoryType.SQL)).Returns(_roleRepository.Object);

            _userService = new UserService(unitOfWork.Object);
        }

        [Fact]
        public void GetUserById_ShouldCallGetSingeOnce_WhenUserId()
        {
            // Act
            _userService.GetUserById(Guid.Empty);

            // Assert
            _userRepository.Verify(i => i.FirstOrDefault(It.IsAny<Expression<Func<User, bool>>>()), Times.Once);
        }

        [Fact]
        public void GetRoleByName_ShouldCallGetSingeOnce_WhenRole()
        {
            // Act
            _userService.GetRoleByName("role");

            // Assert
            _roleRepository.Verify(i => i.FirstOrDefault(It.IsAny<Expression<Func<Role, bool>>>()), Times.Once);
        }

        [Fact]
        public void GetAllRoles_ShouldCallGetManyOnce_WhenNothing()
        {
            // Act
            _userService.GetAllRoles();

            // Assert
            _roleRepository.Verify(i => i.GetMany(0, int.MaxValue, It.IsAny<Expression<Func<Role, bool>>>(), null, It.IsAny<SortDirection>()), Times.Once);
        }

        [Fact]
        public void GetRoleNames_ShouldCallGetSingeOnce_WhenRole()
        {
            // Act
            _userService.GetRoleNames();

            // Assert
            _roleRepository.Verify(i => i.GetMany(0, int.MaxValue, It.IsAny<Expression<Func<Role, bool>>>(), null, It.IsAny<SortDirection>()), Times.Once);
        }

        [Fact]
        public void Register_ShouldCallInsertOnceWhenEmailAndPassword()
        {
            // Act
            _userService.Register("email", "password");

            // Assert
            _userRepository.Verify(i => i.Insert(It.IsAny<User>()), Times.Once);
        }

        [Fact]
        public void CreateRole_ShouldCallGetSingeOnce_WhenRole()
        {
            // Act
            _userService.CreateRole("role");

            // Assert
            _roleRepository.Verify(i => i.Insert(It.IsAny<Role>()), Times.Once);
        }

        [Fact]
        public void EditRole_ShouldCallUpdateeOnce_WhenRole()
        {
            // Act
            _userService.EditRole(Guid.Empty, "role");

            // Assert
            _roleRepository.Verify(i => i.Update(It.IsAny<Role>()), Times.Once);
        }

        [Fact]
        public void GetUserByEmail_ShouldCallGetSingeOnce_WhenEmail()
        {
            // Act
            _userService.GetUserByEmail("email");

            // Assert
            _userRepository.Verify(i => i.FirstOrDefault(It.IsAny<Expression<Func<User, bool>>>(), It.IsAny<Expression<Func<User, object>>>()), Times.Once);
        }

        [Fact]
        public void GetUserByEmail_ShouldCallGetSingeOnce_WhenNullUser()
        {
            // Arrange
            _userRepository.Setup(i => i.FirstOrDefault(It.IsAny<Expression<Func<User, bool>>>(), It.IsAny<Expression<Func<User, object>>>()))
                .Returns(_user);

            // Act
            _userService.GetUserByEmail("email");

            // Assert
            _userRepository.Verify(i => i.FirstOrDefault(It.IsAny<Expression<Func<User, bool>>>(), It.IsAny<Expression<Func<User, object>>>()), Times.Once);
        }

        [Fact]
        public void ChangeRole_ShouldCallUpdateOnce_WhenEmailAndRoles()
        {
            // Arrange
            _userRepository.Setup(i => i.FirstOrDefault(It.IsAny<Expression<Func<User, bool>>>(), It.IsAny<Expression<Func<User, object>>>()))
                .Returns(_user);

            // Act
            _userService.ChangeRole("email", new List<string> { "role" });

            // Assert
            _userRepository.Verify(i => i.Update(It.IsAny<User>()), Times.Once);
        }

        [Fact]
        public void GetAllUsers_ShouldGetSingleOnce_WhenNothing()
        {
            // Act
            _userService.GetAllUsers();

            // Assert
            _roleRepository.Verify(i => i.FirstOrDefault(It.IsAny<Expression<Func<Role, bool>>>()), Times.Once);
        }

        [Fact]
        public void RestoreUser_ShouldGetSingleOnce_WhenEmail()
        {
            // Act
            _userService.RestoreUser("email");

            // Assert
            _userRepository.Verify(i => i.FirstOrDefault(It.IsAny<Expression<Func<User, bool>>>()), Times.Once);
        }

        [Fact]
        public void DeleteUser_ShouldGetSingleOnce_WhenEmail()
        {
            // Act
            _userService.DeleteUser("email");

            // Assert
            _userRepository.Verify(x => x.FirstOrDefault(It.IsAny<Expression<Func<User, bool>>>()), Times.Once);
        }

        [Fact]
        public void DeleteRole_ShouldGetSingleOnce_WhenRole()
        {
            // Act
            _userService.DeleteRole("role");

            // Assert
            _roleRepository.Verify(x => x.FirstOrDefault(It.IsAny<Expression<Func<Role, bool>>>()), Times.Once);
        }

        private void InitializeTestData(out User user)
        {
            user = new User
            {
                Roles = new List<UserRole>
                {
                    new UserRole
                    {
                        Role = new Role{Name = "role", Id = Guid.NewGuid()}
                    }
                }
            };
        }
    }
}