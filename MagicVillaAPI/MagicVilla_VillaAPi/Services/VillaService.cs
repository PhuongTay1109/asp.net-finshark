using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Models.Dto;
using MagicVilla_VillaAPI.Models;

namespace MagicVilla_VillaAPI.Services
{
    public class VillaService : IVillaService
    {
        private readonly ApplicationDbContext _db;

        public VillaService(ApplicationDbContext db)
        {
            _db = db;
        }

        public IEnumerable<VillaDTO> GetAllVillas()
        {
            var villas = _db.Villas.ToList();

            var villaDTOs = villas.Select(v => new VillaDTO
            {
                Id = v.Id,
                Name = v.Name,
                Details = v.Details,
                Amenity = v.Amenity,
                ImageUrl = v.ImageUrl,
                Occupancy = v.Occupancy,
                Rate = v.Rate,
                Sqft = v.Sqft
            }).ToList();

            return villaDTOs;
        }

        public VillaDTO GetVillaById(int id)
        {
            var villa = _db.Villas.FirstOrDefault(u => u.Id == id);
            if (villa == null) return null;

            return new VillaDTO
            {
                Id = villa.Id,
                Name = villa.Name,
                Details = villa.Details,
                Amenity = villa.Amenity,
                ImageUrl = villa.ImageUrl,
                Occupancy = villa.Occupancy,
                Rate = villa.Rate,
                Sqft = villa.Sqft
            };
        }

        public void CreateVilla(VillaDTO villaDTO)
        {
            Villa model = new Villa()
            {
                Amenity = villaDTO.Amenity,
                Details = villaDTO.Details,
                ImageUrl = villaDTO.ImageUrl,
                Name = villaDTO.Name,
                Occupancy = villaDTO.Occupancy,
                Rate = villaDTO.Rate,
                Sqft = villaDTO.Sqft,
            };
            _db.Villas.Add(model);
            _db.SaveChanges();
        }

        public bool VillaExists(string name)
        {
            return _db.Villas.Any(u => u.Name.ToLower() == name.ToLower());
        }

        public void UpdateVilla(VillaDTO villaDTO)
        {
            Villa model = new Villa()
            {
                Amenity = villaDTO.Amenity,
                Details = villaDTO.Details,
                Id = villaDTO.Id,
                ImageUrl = villaDTO.ImageUrl,
                Name = villaDTO.Name,
                Occupancy = villaDTO.Occupancy,
                Rate = villaDTO.Rate,
                Sqft = villaDTO.Sqft,
            };
            _db.Villas.Update(model);
            _db.SaveChanges();
        }

        public void DeleteVilla(int id)
        {
            var villa = _db.Villas.FirstOrDefault(u => u.Id == id);
            if (villa != null)
            {
                _db.Villas.Remove(villa);
                _db.SaveChanges();
            }
        }
    }
}
