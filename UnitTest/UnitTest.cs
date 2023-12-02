using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project_Management;

namespace UnitTest
{
    [TestClass]
    public class HelperTest
    {
        [TestMethod]
        public void FinalizeTest()
        {
            Helper helper = new Helper();

            Assert.IsNotNull(helper, "new Helper() should not be null!!");
            Assert.AreEqual("home.", helper.Finalize("home."), "should return the same string");
            Assert.AreEqual("helper.", helper.Finalize("helper"), "should return string with '.' in the end of string");
        }

        [TestMethod]
        public void EllipsisExceptionTest() 
        {
            Helper helper = new Helper();
            Assert.ThrowsException<ArgumentNullException>(() => helper.Ellipsis(null!, 1));
        }

        [TestMethod]
        public void CombineUrlTest()
        {
            Helper helper = new();
            Dictionary<String[], String> testCases = new()
            {
                { new[] { "/home",  "index"   }, "/home/index"  },
                { new[] { "/shop/", "/cart"   }, "/shop/cart"   },
                { new[] { "auth/",  "logout"  }, "/auth/logout" },
                { new[] { "forum/",  "topic/" }, "/forum/topic" },
            };
            foreach (var testCase in testCases)
            {
                Assert.AreEqual(
                    testCase.Value,
                    helper.CombineUrl(testCase.Key[0], testCase.Key[1]),
                    $"{testCase.Value} -- {testCase.Key[0]} + {testCase.Key[1]}"
                );
            }
        }


        [TestMethod]
        public void CombineUrlMultiTest()
        {
            Helper helper = new();
            Dictionary<String[], String> testCases = new()
            {
                { new[] { "/home",  "/index", "/123"  }, "/home/index/123"  },
                { new[] { "/shop/", "/cart/", "123/"  }, "/shop/cart/123"   },
                { new[] { "auth/",  "logout", "/123/" }, "/auth/logout/123" },
                { new[] { "forum",  "topic/", "123"   }, "/forum/topic/123" },
            };
            foreach (var testCase in testCases)
            {
                Assert.AreEqual(
                    testCase.Value,
                    helper.CombineUrl(testCase.Key),
                    $"{testCase.Value} -- {testCase.Key[0]} + {testCase.Key[1]}"
                );
            }
        }

        [TestMethod]
        public void CombineUrlExceptionTest()
        {
            Helper helper = new();
            Assert.AreEqual("/home", helper.CombineUrl("/home", null!));

            Assert.AreEqual("Non null arg after null one",
                Assert.ThrowsException<ArgumentException>(
                () => helper.CombineUrl(null!, null!, "/sub")
                ).Message);
            
            Assert.AreEqual("Non null arg after null one",
                Assert.ThrowsException<ArgumentException>(
                () => helper.CombineUrl(null!, "/sub", null!, null!, "/sub")
                ).Message);

            Assert.AreEqual("Non null arg after null one",
                Assert.ThrowsException<ArgumentException>(
                () => helper.CombineUrl(null!, "/sub", null!, null!, "/sub", "/sub", "/sub", null!, null!)
                ).Message);

            Assert.AreEqual("All arguments are null!",
                Assert.ThrowsException<ArgumentException>(
                () => helper.CombineUrl(null!, null!)
                ).Message);

            Assert.AreEqual("All arguments are null!",
                Assert.ThrowsException<ArgumentException>(
                () => helper.CombineUrl(null!, null!, null!, null!)
                ).Message);
        }
    }
}