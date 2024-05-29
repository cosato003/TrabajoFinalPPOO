using EjercicioFinalOOP.Excepciones;
using EjercicioFinalOOP.Interfaces;
using EjercicioFinalOOP.Models;

namespace EjercicioFinalOOP.Loaders
{
    public class RoomFileLoader : IRoomRepository
    {
        private string FilePath { get; set; }
        List<Room> Rooms { get; set; }

        public RoomFileLoader(string filePath)
        {
            FilePath = filePath;
            Rooms = new List<Room>();
            // Leemos CSV de habitaciones, usando el separador ;
            using var reader = new StreamReader(FilePath);
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (line != null)
                {
                    var values = line.Split(';');
                    switch (values[0])
                    {
                        case "Simple":
                            Rooms.Add(new SimpleRoom(uint.Parse(values[1]), uint.Parse(values[2]), float.Parse(values[3]), (BedType)Enum.Parse(typeof(BedType), values[4])));
                            break;
                        case "Executive":
                            Rooms.Add(new ExecutiveRoom(uint.Parse(values[1]), uint.Parse(values[2]), float.Parse(values[3]), (BedType)Enum.Parse(typeof(BedType), values[4])));
                            break;
                        case "Suite":
                            Rooms.Add(new Suite(uint.Parse(values[1]), uint.Parse(values[2]), float.Parse(values[3]), (BedType)Enum.Parse(typeof(BedType), values[4])));
                            break;
                    }
                }
            }
        }

        public void AddRoom(Room room)
        {
            Rooms.Add(room);
        }

        public void DeleteRoom(int roomNumber)
        {
            Room room = Rooms.FirstOrDefault(r => r.RoomNumber == roomNumber) ?? throw new RoomNotFoundException(roomNumber);
            Rooms.Remove(room);
        }

        public List<Room> GetAvailableRooms()
        {
            return Rooms.Where(r => !r.Occupied).ToList();
        }

        public Room GetRoomByNumber(int roomNumber)
        {
            return Rooms.FirstOrDefault(r => r.RoomNumber == roomNumber) ?? throw new RoomNotFoundException(roomNumber);
        }

        public void UpdateRoom(Room room)
        {
            Room oldRoom = Rooms.FirstOrDefault(r => r.RoomNumber == room.RoomNumber) ?? throw new RoomNotFoundException((int)room.RoomNumber);
            Rooms[Rooms.IndexOf(oldRoom)] = room;
        }


    }

}
