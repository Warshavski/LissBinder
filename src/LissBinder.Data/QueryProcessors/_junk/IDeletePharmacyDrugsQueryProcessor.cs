using System.Threading.Tasks;

namespace Escyug.LissBinder.Data.QueryProcessors
{
    public interface IDeletePharmacyDrugsQueryProcessor
    {
        Task<bool> DeleteDrugsAsync(int pharmacyId);
    }
}
