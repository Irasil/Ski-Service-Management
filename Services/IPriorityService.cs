using Ski_Service_Management.Models;

namespace Ski_Service_Management.Services
{
    /// <summary>
    /// Interface für PriorityService
    /// </summary>
    public interface IPriorityService
    {
        public List<PriorityModel> GetAll();
    }
}
