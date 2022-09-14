using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resume.Service.DTOs.UserDTOs;

public class UserTokenViewModel
{
    public string Token { get; set; }

	public UserTokenViewModel(string token)
	{
		Token = token;
	}
}
