using Resume.Service.Extentions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Resume.UnitTest.Extentions;
#pragma warning disable
public class StringExtentionsUnitTests
{
    #region Checking Valid Password
    [Fact]
    public void ValidPasswordLowerCharChecking()
    {
        string password = "MUHAMMAD1011";
        var res = password.IsValidPassword(out string error);

        Assert.False(res);
        Assert.Equal(error, "Password should contain at least one lower case letter.");
    }

    [Fact]
    public void ValidPasswordUpperCharChecking()
    {
        string password = "muhammad1011";
        var res = password.IsValidPassword(out string error);

        Assert.False(res);
        Assert.Equal(error, "Password should contain at least one upper case letter.");
    }

    [Fact]
    public void ValidPasswordMinCharsLengthChecking()
    {
        string password = "Muh";
        var res = password.IsValidPassword(out string error);

        Assert.False(res);
        Assert.Equal(error, "Password should not be lesser than 8 or greater than 15 characters.");
    }

    [Fact]
    public void ValidPasswordMAxCharsLengthChecking()
    {
        string password = "Muhsldfjasjklfhwedkfasj;ldfja;dflasjkf;alfja;sldfkjqoiwqpofjasdl;fkal345134kldjfhkjlf";
        var res = password.IsValidPassword(out string error);

        Assert.False(res);
        Assert.Equal(error, "Password should not be lesser than 8 or greater than 15 characters.");
    }

    [Fact]
    public void ValidPasswordNumberChecking()
    {
        string password = "Muhsldfjasjk";
        var res = password.IsValidPassword(out string error);

        Assert.False(res);
        Assert.Equal(error, "Password should be contain at least one numeric value.");
    }

    [Fact]
    public void ValidPasswordNotNullChecking()
    {
        string password = null;
        var res = password.IsValidPassword(out string error);

        Assert.False(res);
        Assert.Equal(error, "Password should not be empty");
    }

    [Fact]
    public void ValidPasswordNotEmptyChecking()
    {
        string password = "";
        var res = password.IsValidPassword(out string error);

        Assert.False(res);
        Assert.Equal(error, "Password should not be empty");
    }

    [Fact]
    public void ValidPasswordCorrectChecking()
    {
        string password = "Muhammad1701";
        var res = password.IsValidPassword(out string error);

        Assert.True(res);
    }

    #endregion


}
