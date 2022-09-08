using Keys;
namespace Commands
{
    public class SaveGameCommand
    {
        public void OnSaveGameData(SaveGameDataParams saveGameDataParams)
        {
            if (saveGameDataParams.Level!=null)
            {
                ES3.Save("Level", saveGameDataParams.Level);
            }
            if (saveGameDataParams.Money!=null)
            {
                int totalScore = saveGameDataParams.Money;
                ES3.Save("Money", totalScore);

            }
        }
    }
}