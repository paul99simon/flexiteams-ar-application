
namespace FlexiTeams.DataClasses.Wrapper
{
    public class Id
    {
        public string _id { get; set; }

        public Id(string id)
        {
            _id = id;
        }

        public override string ToString()
        {
            return _id;
        }
    }
}
