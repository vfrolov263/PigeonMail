namespace PigeonMail
{
    public struct LetterStatusChangedSignal
    {
        public Letter letter;
    }    

    public struct TrackableLetterStatusChangedSignal
    {
        public Letter letter;
    } 

    public struct NextLessonSignal
    {
    }

    public struct OutOfTimeSignal
    {
    }

    public struct CityDamageSignal
    {
        public CityStateTracker cityTracker;
    }

    public struct CityReceivedLetterSignal
    {
        public CityStateTracker cityTracker;
    }
}
