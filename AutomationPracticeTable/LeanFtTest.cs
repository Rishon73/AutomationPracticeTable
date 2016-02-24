using System;
using NUnit.Framework;
using HP.LFT.SDK;
using HP.LFT.SDK.Web;
using HP.LFT.Verifications;

/* Goal as desribed on: http://toolsqa.com/selenium-webdriver/handle-dynamic-webtables-in-selenium-webdriver/
 * 
 * 1) Launch new Browser
 * 2) Open URL “http://toolsqa.com/automation-practice-table/”
 * 3) Get the value from cell ‘Dubai’ with using dynamic xpath
 * 4) Print all the column values of row ‘Clock Tower Hotel’
 * 
 * This test was inspired when I came across the above example and figured
 * there had to be a better an easier way in LFT to accomplish this rather
 * than the cumbersome and in flexible methed shown using Xpath in Selenium.
 * Yes, there are other ways to accomplish this in Selenium without using
 * Xpath but from my research it is still cumbersome as it doesn't provide
 * you with a mechanism to find a row based on its contents and then process
 * the row easily.
 * 
 * When I say easily, I am referring to the fact that LFT can accomplish this
 * without needing to knwo the row and column counts or iteratively loop
 * through to find ID, tags or even resort to CSS which still needs to know
 * column number value.
 */
namespace AutomationPracticeTable
{
    [TestFixture]
    public class LeanFtTest : UnitTestClassBase
    {
        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            // Setup once per fixture
        }

        [SetUp]
        public void SetUp()
        {
            // Before each test
        }

        [Test]
        public void AutomationPracticeTable()
        {
            Reporter.SnapshotCaptureLevel = HP.LFT.Report.CaptureLevel.All;
            IBrowser browser = BrowserFactory.Launch(BrowserType.Chrome);
            browser.Navigate("http://toolsqa.com/automation-practice-table/");

            var mySearchValue = "Dubai";
            //3) Get the value from cell ‘Dubai’ with using dynamic xpath
            var myCellValue = browser.Describe<IWebElement>(new WebElementDescription
            {
                TagName = @"TD",
                InnerText = @mySearchValue
            }).InnerHTML;
            Reporter.ReportEvent("Cell value:" + myCellValue, myCellValue);

            //4) Print all the column values of row ‘Clock Tower Hotel’
            mySearchValue = "Clock Tower Hotel";
            ITableRow myRow = browser.Describe<ITable>(new TableDescription
            {
                TagName = @"TABLE",
                Index = 0
            }).FindRowWithCellText(@mySearchValue);
            Reporter.ReportEvent("Contents for row:" + @mySearchValue, "");
            var firstEntry = true;
            foreach (ITableCell myCell in myRow.Cells)
            {
                if (firstEntry)
                    firstEntry = false;
                else
                    Reporter.ReportEvent("Row contents:" + myCell.Text, myCell.Text);
            }

            browser.Close();
        }

        [TearDown]
        public void TearDown()
        {
            // Clean up after each test
        }

        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {
            // Clean up once per fixture
        }
    }
}
