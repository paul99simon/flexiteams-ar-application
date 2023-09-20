
namespace FlexiTeams.DataClasses.Wrapper
{
    public abstract class Id
    {
        public string _id { get; set; }

        public override string ToString()
        {
            return _id;
        }
    }
}
