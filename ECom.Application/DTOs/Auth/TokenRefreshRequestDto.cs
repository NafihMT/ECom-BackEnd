using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECom.Application.DTOs.Auth
{
    public class TokenRefreshRequestDto
    {
        public string RefreshToken { get; set; }
    }
}