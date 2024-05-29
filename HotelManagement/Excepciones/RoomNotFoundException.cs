// Namespace para las Excepciones
namespace EjercicioFinalOOP.Excepciones;

public class RoomNotFoundException(int roomNumber)
    : Exception($"La habitación con el número {roomNumber} no fue encontrada.");