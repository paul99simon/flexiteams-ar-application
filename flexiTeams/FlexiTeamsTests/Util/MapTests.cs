using System;
using System.Collections.Generic;
using NUnit.Framework;
using flexiTeams.Util;

namespace FlexiTeamsTests
{
    [TestFixture]
    public class MapTests
    {
        [Test]
        public void T1KeyNotUnique()
        {
            //Arrange
            Map<int, string> map = new Map<int, string>(){
                {1, "a"},
            };;

            //Assert
            ArgumentException ex = Assert.Throws<ArgumentException>(() =>
            {
                map.Add(1, "b");
            });
            
            Assert.AreEqual("Map already contains key pair {1, a}", ex.Message);
        }
        
        [Test]
        public void T2KeyNotUnique()
        {
           
            //Arrange
            Map<int, string> map = new Map<int, string>()
            {
                {1, "a"},
            };;
            
            //Assert
            ArgumentException ex = Assert.Throws<ArgumentException>(() =>
            {
                map.Add(2, "a");
            });
            
            Assert.AreEqual("Map already contains key pair {1, a}", ex.Message);
        }
        
        [Test]
        public void getValueForward()
        {
            //Arrange
            Map<int, string> map = new Map<int, string>()
            {
                {1, "a"},
            };

            //Assert
            Assert.AreEqual("a",map.Forward[1]);
        }
        
        [Test]
        public void getValueReverse()
        {
            //Arrange
            Map<int, string> map = new Map<int, string>()
            {
                {1, "a"},
            };

            //Assert
            Assert.AreEqual(1,map.Reverse["a"]);
        }

        [Test]
        public void MapIsNull()
        {
            Map<int, int> Map = null;

            NullReferenceException ex = Assert.Throws<NullReferenceException>(() =>
            {
                Map.Add(1, 1);
            });
        }

        [Test]
        public void T1IsNull()
        {
            //Arange
            Map<object, object> map = new Map<object, object>();

            //Assert
            ArgumentNullException ex = Assert.Throws<ArgumentNullException>(() =>
            {
                map.Add(null, "a");
            });
        }
        
        [Test]
        public void T2IsNull()
        {
            //Arange
            Map<object, object> map = new Map<object, object>();

            //Assert
            ArgumentNullException ex = Assert.Throws<ArgumentNullException>(() =>
            {
                map.Add("a", null);
            });
        }
        
        [Test]
        public void T1T2IsNull()
        {
            //Arange
            Map<object, object> map = new Map<object, object>();

            //Assert
            ArgumentNullException ex = Assert.Throws<ArgumentNullException>(() =>
            {
                map.Add(null, null);
            });
        }
        
        [Test]
        public void T1IsEmptyString()
        {
            //Arange
            Map<string, string> map = new Map<string, string>();

            //Assert
            ArgumentNullException ex = Assert.Throws<ArgumentNullException>(() =>
            {
                map.Add("", "a");
            });
        }
        
        [Test]
        public void T2IsEmptyString()
        {
            //Arange
            Map<string, string> map = new Map<string, string>();

            //Assert
            ArgumentNullException ex = Assert.Throws<ArgumentNullException>(() =>
            {
                map.Add("a", "");
            });
        }
        
        [Test]
        public void T1T2IsEmptyString()
        {
            //Arange
            Map<string, string> map = new Map<string, string>();

            //Assert
            ArgumentNullException ex = Assert.Throws<ArgumentNullException>(() =>
            {
                map.Add("", "");
            });
        }
        
        [Test]
        public void T1IsWhiteSpaceString()
        {
            //Arange
            Map<string, string> map = new Map<string, string>();

            //Assert
            ArgumentNullException ex = Assert.Throws<ArgumentNullException>(() =>
            {
                map.Add("  ", "a");
            });
        }
        
        [Test]
        public void T2IsWhiteSpaceString()
        {
            //Arange
            Map<string, string> map = new Map<string, string>();

            //Assert
            ArgumentNullException ex = Assert.Throws<ArgumentNullException>(() =>
            {
                map.Add("a", "  ");
            });
        }
        
        [Test]
        public void T1T2IsWhiteSpaceString()
        {
            //Arange
            Map<string, string> map = new Map<string, string>();

            //Assert
            ArgumentNullException ex = Assert.Throws<ArgumentNullException>(() =>
            {
                map.Add("  ", "  ");
            });
        }

        [Test]
        public void CollectionTypeInitialization()
        {
            //Arrange
            Map<int, int> map = new Map<int, int>()
            {
                {1, 2},
                {2, 3},
                {3, 4},
                {4, 5},
            };

            Dictionary<int, int> dictionary = new Dictionary<int, int>();
            
            //Act
            foreach (KeyValuePair<int, int> var in map)
            {
                dictionary.Add(var.Key, var.Value);
            }

            //Assert
            Assert.AreEqual(4, map.Count);
            Assert.AreEqual(dictionary.Count, map.Count);
            Assert.AreEqual(map.ToString(), dictionary.ToString());
        }
    }
}