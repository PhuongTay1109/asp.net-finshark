using MagicVilla_VillaAPI.Models.Dto;

namespace MagicVilla_VillaAPI.Services
{
    public interface IVillaService
    {
        IEnumerable<VillaDTO> GetAllVillas();
        VillaDTO GetVillaById(int id);
        void CreateVilla(VillaDTO villaDTO);
        bool VillaExists(string name);
        void UpdateVilla(VillaDTO villaDTO);
        void DeleteVilla(int id);
    }
}
