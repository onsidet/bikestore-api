﻿namespace BikeStoresApi.Dtos
{
    public class UserDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
    public class UserRigisterDto : UserDto { }
    public class UserLoginDto : UserDto { }
}
