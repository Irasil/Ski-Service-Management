namespace Ski_Service_Management.Services
{
	/// <summary>
	/// Interface für TokenService
	/// </summary>
	public interface ITokenService
	{
		string CreateToken(string username);
	}
}
