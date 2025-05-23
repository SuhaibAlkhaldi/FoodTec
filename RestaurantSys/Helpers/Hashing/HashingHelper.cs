﻿using System.Security.Cryptography;
using System.Text;

namespace RestaurantSys.Helpers.Hashing
{
    public static class HashingHelper
    {
        public static string HashValueWith384(string inputValue)
        {
            var cleanInput = inputValue.Trim(); 

            //convert string to bytes array
            var inputBytes = Encoding.UTF8.GetBytes(cleanInput);
            //inlization hashing alogrthim object 
            var hasher = SHA384.Create();
            //compute hash
            var hashedByte = hasher.ComputeHash(inputBytes);
            //convert hashed byte to string 
            return BitConverter.ToString(hashedByte).Replace("-", "").ToLower();
        }
    }
}
