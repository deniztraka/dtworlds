using UnityEngine.UI;

namespace TimeOfTheDay
{
    public class DaysPastTextBehaviour : UIBehaviour
    {
        public string Format;

        // Start is called before the first frame update
        void Start()
        {
            if (string.IsNullOrEmpty(Format))
            {
                Format = "{1}:{2} - {0} days";
            }
        }

        public override void UpdateMe()
        {
            if (GameTime != null)
            {
                var currentGameTime = GameTime.GetGameTime();
                var textComp = GetComponent<Text>();
                textComp.text = string.Format(Format,
                currentGameTime.Days,
                currentGameTime.Hours.ToString().Length == 1 ? ("0" + currentGameTime.Hours.ToString()) : currentGameTime.Hours.ToString(),
                currentGameTime.Minutes.ToString().Length == 1 ? ("0" + currentGameTime.Minutes.ToString()) : currentGameTime.Minutes.ToString(),
                currentGameTime.Seconds);
            }
        }
    }
}
