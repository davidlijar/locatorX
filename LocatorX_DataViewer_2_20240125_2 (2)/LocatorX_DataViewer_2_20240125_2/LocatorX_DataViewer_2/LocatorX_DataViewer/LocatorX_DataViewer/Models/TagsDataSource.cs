using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace Controller_Tester.Standard_Simulation
{
    public static class TagsDataSource
    {
        public static Random random = new Random();

        public static Tag GetRandomTag()
        {
            return new Tag
            {
                Name = "Tag" + random.Next(0, 100),
                X = random.Next(0, 500),
                Y = random.Next(0, 500)
            };

        }

        public static IEnumerable<Tag> GetRandomTags()
        {
            return Enumerable.Range(5, random.Next(6, 10)).Select(x => GetRandomTag());
        }

        public static Connector GetRandomConnector(IEnumerable<Tag> tags)
        {
            return new Connector
            {

            };
        }

        public static IEnumerable<Connector> GetRandomConnectors(List<Tag> tags)
        {
            var result = new List<Connector>();
            for (int i = 0; i < tags.Count() - 1; i++)
            {
                result.Add(new Connector()
                {
                    Start = tags[i],
                    End = tags[i + 1],
                    Name = "Connector" + random.Next(1, 100).ToString()
                });
            }
            return result;
        }


    }
}