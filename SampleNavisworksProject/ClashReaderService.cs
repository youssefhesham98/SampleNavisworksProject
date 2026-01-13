using Autodesk.Navisworks.Api;
using Autodesk.Navisworks.Api.Clash;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Navisworks.Api.Clash;

namespace SampleNavisworksProject
{
    public class ClashReaderService
    {
        public List<ClashTestData> ReadGroupedClashes(Document document)
        {
            var result = new List<ClashTestData>();

            //var clashTests = document.GetClash().TestsData.Tests;

            var testsData = Application.ActiveDocument
                                        .GetClash()
                                        .TestsData;

            foreach (ClashTest test in testsData.Tests)
            {
                var testData = new ClashTestData
                {
                    TestName = test.DisplayName,
                    Groups = new List<ClashGroupData>()
                };

                foreach (SavedItem item in test.Children)
                {
                    if (item is ClashResultGroup group)
                    {
                        var groupData = new ClashGroupData
                        {
                            GroupName = group.DisplayName,
                            Clashes = new List<ClashData>()
                        };

                        foreach (SavedItem child in group.Children)
                        {
                            if (child is ClashResult clash)
                            {
                                groupData.Clashes.Add(new ClashData
                                {
                                    ClashName = clash.DisplayName,
                                    Item1 = clash.Item1?.DisplayName,
                                    Item2 = clash.Item2?.DisplayName,
                                    Status = clash.Status.ToString(),
                                    Distance = clash.Distance
                                });
                            }
                        }

                        testData.Groups.Add(groupData);
                    }
                }
            }

            return result;
        }
    }
}
