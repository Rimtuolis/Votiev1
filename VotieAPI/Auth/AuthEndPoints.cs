using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Security.Claims;
using VotieAPI.Auth.Entity;

namespace VotieAPI.Auth
{
    public static class AuthEndPoints
    {
        public static void AddAuthApi(this WebApplication app)
        {
            //register
            app.MapPost("api/register", async (UserManager<VotieUser> userManager, RegisterUserDto registerUserDto) =>
            {
                // check user exists
                var user = await userManager.FindByNameAsync(registerUserDto.UserName);
                if (user != null) 
                {
                    return Results.UnprocessableEntity("Username already taken");
                }

                var newUser = new VotieUser
                {
                    Email = registerUserDto.Email,
                    UserName = registerUserDto.UserName,
                    Name = registerUserDto.Name,
                    LastName = registerUserDto.LastName
                };

                var createUserResult = await userManager.CreateAsync(newUser, registerUserDto.Password);

                if(!createUserResult.Succeeded)
                    return Results.UnprocessableEntity();

                await userManager.AddToRoleAsync(newUser, VotieRoles.Voter);

                return Results.Created("api/login", new UserDto(newUser.Email,newUser.UserName));
            });
            //register
            app.MapPost("api/registerCandidate", async (UserManager<VotieUser> userManager, RegisterUserDto registerUserDto) =>
            {
                // check user exists
                var user = await userManager.FindByNameAsync(registerUserDto.UserName);
                if (user != null)
                {
                    return Results.UnprocessableEntity("Username already taken");
                }

                var newUser = new VotieUser
                {
                    Email = registerUserDto.Email,
                    UserName = registerUserDto.UserName,
                    Name = registerUserDto.Name,
                    LastName = registerUserDto.LastName
                };

                var createUserResult = await userManager.CreateAsync(newUser, registerUserDto.Password);

                if (!createUserResult.Succeeded)
                    return Results.UnprocessableEntity();

                await userManager.AddToRoleAsync(newUser, VotieRoles.Candidate);

                return Results.Created("api/login", new UserDto(newUser.Email, newUser.UserName));
            });

            //login
            app.MapPost("api/login", async (UserManager<VotieUser> userManager,JwtTokenService jwtTokenService, LoginUserDto loginUserDto) =>
            {
                // check user exists
                var user = await userManager.FindByNameAsync(loginUserDto.UserName);
                if (user == null)
                {
                    return Results.UnprocessableEntity("Username or password was incorrect.");
                }

                var isPasswordValid = await userManager.CheckPasswordAsync(user,loginUserDto.Password);
                if(!isPasswordValid)
                {
                    return Results.UnprocessableEntity("Username or password was incorrect.");
                }

                user.ForceRelogin = false;
                await userManager.UpdateAsync(user);

                var roles = await userManager.GetRolesAsync(user);

                var accessToken = jwtTokenService.CreateAccessToken(user.UserName, user.Id, roles);
                var refreshToken = jwtTokenService.CreateRefreshToken(user.Id);

                return Results.Ok(new SuccesfullLoginDto(accessToken,refreshToken));
            });

            //accessToken
            app.MapPost("api/accessToken", async (UserManager<VotieUser> userManager, JwtTokenService jwtTokenService, RefreshAccessToken refreshAccessToken) =>
            {
                if(!jwtTokenService.TryParseRefreshToken(refreshAccessToken.RefreshToken, out var claims))
                {
                    return Results.UnprocessableEntity();
                }


                var userId = claims.FindFirstValue(JwtRegisteredClaimNames.Sub);

                var user = await userManager.FindByIdAsync(userId);

                if(user == null) 
                {
                    return Results.UnprocessableEntity("Invalid token");
                }

                if(user.ForceRelogin) 
                {
                    return Results.UnprocessableEntity();
                }

                var roles = await userManager.GetRolesAsync(user);

                var accessToken = jwtTokenService.CreateAccessToken(user.UserName, user.Id, roles);
                var refreshToken = jwtTokenService.CreateRefreshToken(user.Id);

                return Results.Ok(new SuccesfullLoginDto(accessToken, refreshToken));
            });
            app.MapPatch("api/changePassword", async (UserManager<VotieUser> userManager, JwtTokenService jwtTokenService, ChangePasswordDto changePasswordDto) =>
            {
                // check user exists
                var user = await userManager.FindByNameAsync(changePasswordDto.UserName);
                if (user == null)
                {
                    return Results.UnprocessableEntity("Username or password was incorrect.");
                }

                var isPasswordValid = await userManager.CheckPasswordAsync(user, changePasswordDto.OldPassword);
                if (!isPasswordValid)
                {
                    return Results.UnprocessableEntity("Username or password was incorrect.");
                }
                await userManager.ChangePasswordAsync(user, changePasswordDto.OldPassword, changePasswordDto.NewPassword);
                user.ForceRelogin = false;
                await userManager.UpdateAsync(user);

                var roles = await userManager.GetRolesAsync(user);

                var accessToken = jwtTokenService.CreateAccessToken(user.UserName, user.Id, roles);
                var refreshToken = jwtTokenService.CreateRefreshToken(user.Id);

                return Results.Ok(new SuccesfullLoginDto(accessToken, refreshToken));
            });
        }
    }
}
public record UserDto(string Email,string UserName);
public record RegisterUserDto(string UserName, string Email, string Password, string Name, string LastName);
public record LoginUserDto(string UserName, string Password);
public record SuccesfullLoginDto(string AccessToken, string RefreshToken);
public record RefreshAccessToken(string RefreshToken);

public record ChangePasswordDto(string UserName, string OldPassword, string NewPassword );