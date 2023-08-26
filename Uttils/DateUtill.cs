namespace CCRATestAutomation.Uttils
{
    internal class DateUtill
    {

        public static string CurrentDateTime(string currentDateTimePattern)
        {
            DateTime datetime = DateTime.Now;
            string formattedDate = datetime.ToString(currentDateTimePattern);
            return formattedDate;
        }

    }
}
