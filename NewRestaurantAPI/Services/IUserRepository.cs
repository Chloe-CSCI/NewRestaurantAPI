﻿namespace NewRestaurantAPI.Services
{
    public interface IUserRepository

    {
        Task<ApplicationUser?> ReadByUsernameAsync(string username);
    }
}
