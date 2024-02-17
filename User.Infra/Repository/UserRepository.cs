using System.Web;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using User.Domain.Interfaces;
using User.Domain.Models;
using User.Infra.Entities;
using User.Infra.Exceptions;
using User.Infra.Services;


namespace User.Infra.Repository
{

    public class UserRepository : IUserRepository
    {

        private readonly IMapper _mapper;
        private readonly UserManager<UserEntity> _userManager;
        private readonly SignInManager<UserEntity> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly TokenService _tokenService;
        private readonly EmailService _emailService;

        public UserRepository(IMapper mapper, UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager, TokenService tokenService, EmailService emailService, RoleManager<IdentityRole> roleManager)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _emailService = emailService;
            _roleManager = roleManager;
        }

        public void ActiveUser(ActiveUser request)
        {
            var identityUser =
            _userManager
            .Users
            .FirstOrDefault(u => u.Id == request.UserId);
            if (identityUser is not null)
            {
                if (!identityUser.EmailConfirmed)
                {
                    var identityResult = _userManager.ConfirmEmailAsync(identityUser, request.ActivateCode).Result;
                    if (!identityResult.Succeeded)
                    {
                        throw new BadRequestException("Falha ao ativar usuário");
                    }
                }
                else
                {
                    throw new BadRequestException("Usuário já foi autorizado");
                }
            }
            else
            {
                throw new InvalidOperationException("Usuário não encontrado");
            }
        }

        public void AddUserAsync(UserCreateModel createUser)
        {
            UserEntity user = _mapper.Map<UserEntity>(createUser);
            user.UserName = createUser.Email;

            IdentityResult result = _userManager.CreateAsync(user, createUser.Password).Result;
            if (result.Succeeded)
            {
                var code = _userManager.GenerateEmailConfirmationTokenAsync(user).Result;
                var encodeCode = HttpUtility.UrlEncode(code);
                _userManager.AddToRoleAsync(user, "admin");
                _emailService.SendEmail(new[] { user.Email }, "Link de ativação", user.Id, encodeCode, "create");
            }
            else
            {
                if (result.Errors.Any() && result.Errors.FirstOrDefault()?.Code == "DuplicateUserName")
                {
                    throw new BadRequestException("Email já existente");

                }
                else
                {
                    throw new BadRequestException("Erro ao cadastrar usuário");
                }

            }

        }

        public string LoginAsync(LoginModel loginModel)
        {
            var result = _signInManager.PasswordSignInAsync(loginModel.UserName, loginModel.Password, false, false).Result;
            if (result.Succeeded)
            {
                var completeUser = _signInManager
                .UserManager
                .Users
                .FirstOrDefault(u => u.NormalizedUserName == loginModel.UserName.ToUpper());
                UserModel user = _mapper.Map<UserModel>(completeUser);
                var token = _tokenService.GenerateToken(user, _signInManager.UserManager.GetRolesAsync(completeUser).Result.FirstOrDefault());
                return token;
            }

            else
            {
                if (result.IsNotAllowed)
                {
                    throw new BadRequestException("Usuário se encontra bloqueado, verifique caixa de entrada do e-mail");
                }
                throw new BadRequestException("Verifique seus dados e tente novamente");
            }

        }

        public void PasswordReset(PasswordReset request)
        {
            var identityUser =
         _userManager
         .Users
         .FirstOrDefault(u => u.Id == request.IdUser);

            if (identityUser is not null)
            {
                var resetPassword = _signInManager.UserManager.ResetPasswordAsync(identityUser, request.Token, request.Password).Result;
                if (!resetPassword.Succeeded)
                {
                    throw new BadRequestException("Erro ao redefinir senha do usuário");
                }

            }
            else throw new BadRequestException("Usuário não encontrado");
        }

        public void RequestPasswordReset(RequestPasswordReset request)
        {
            var identityUser =
            _userManager
            .Users
            .FirstOrDefault(u => u.Email == request.Email);

            if (identityUser is not null)
            {
                string recuperationCode = _signInManager.UserManager.GeneratePasswordResetTokenAsync(identityUser).Result;
                _emailService.SendEmail(new[] { identityUser.Email }, "Link para resetar senha", identityUser.Id, recuperationCode, "resetPassword");
            }
            else throw new BadRequestException("Usuário não encontrado");
        }
    }


}