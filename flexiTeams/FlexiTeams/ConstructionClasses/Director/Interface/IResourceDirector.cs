using FlexiTeams.ConstructionClasses.Builder.Interface;
using FlexiTeams.DataClasses.Resource;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace FlexiTeams.ConstructionClasses.Director.Interface
{
    public interface IResourceDirector
    {
        public Resource Construct(XElement resourceNode, IResourceBuilder rBuilder);
    }
}
