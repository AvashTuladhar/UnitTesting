namespace NUnitNSubsitute
{
    public class SimpleClass
    {
        public int Amount { get; set; }  


        public int AddCount(int value)
        {
            return Amount = Amount + value;
        }

        public int LessCount(int value)
        {
            return Amount = Amount - value;
        }


        public int GetAmount()
        {
            return Amount;
        }
    }
}

