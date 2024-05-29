// Namespace para los Repositorios

using EjercicioFinalOOP.Models;

namespace EjercicioFinalOOP.Interfaces;

public interface IRoomRepository
{
    Room GetRoomByNumber(int roomNumber);
    List<Room> GetAvailableRooms();
    void AddRoom(Room room);
    void UpdateRoom(Room room);
    void DeleteRoom(int roomNumber);
}