// Namespace para las Clases Concretas
namespace EjercicioFinalOOP.Models
{
    public class Minibar(
        int liquorBottles = default,
        int waterBottles = default,
        int personalCareKits = default,
        int sodas = default,
        int wineBottles = default,
        int id = default)
    {
        public int LiquorBottles { get; set; } = liquorBottles;

        public int WaterBottles { get; set; } = waterBottles;

        public int PersonalCareKits { get; set; } = personalCareKits;

        public int Sodas { get; set; } = sodas;

        public int WineBottles { get; set; } = wineBottles;

        public int Id { get; set; } = id;

    }

}
