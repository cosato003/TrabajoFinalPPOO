// Namespace para los Repositorios

using EjercicioFinalOOP.Systems;

namespace EjercicioFinalOOP.Interfaces;

public interface IHotelManagementSystemService
{
    HotelManagementSystem LoadHotelManagementSystem();
    void SaveHotelManagementSystem(HotelManagementSystem hotelManagementSystem);
}