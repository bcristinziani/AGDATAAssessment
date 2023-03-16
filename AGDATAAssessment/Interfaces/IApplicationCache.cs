using AGDATAAssessment.Data.Models;

namespace AGDATAAssessment.Interfaces
{
    public interface IApplicationCache
    {
        Task<IList<T>> GetList<T>(string cacheKey) where T : Person;

        void Refresh(string cacheKey);
    }
}
