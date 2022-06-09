
namespace database
{
   public interface IRepository
    {
        void AddScore(int score);

        void AddLife(int life);

        void Addmusik(bool falsk);

        Life GetAllLife(int life);
        Character GetAllScore(int score);

        Musik GetAlltru(int id);

        void Open();

        void Close();
        void UPDATELife(int life);
    }
}
