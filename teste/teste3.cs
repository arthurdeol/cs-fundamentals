using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace teste
{
    public class teste3
    {
        public class Markings
        {
            public int Index { get; set; }
            public int Mark { get; set; }
            public int RequerimentPossible { get; set; }
            public int OverFlow { get; set; }
        }

        public class Flaskes
        {
            public List<Markings> Markings { get; set; }
            public int TotalWast { get; set; }
        }

        /*
         * Complete the 'chooseFlask' function below.
         *
         * The function is expected to return an INTEGER.
         * The function accepts following parameters:
         *  1. INTEGER_ARRAY requirements
         *  2. INTEGER flaskTypes
         *  3. 2D_INTEGER_ARRAY markings
         */

        public static int ChooseFlask(List<int> requirements, int flaskTypes, List<List<int>> markings)
        {
            int flaskReturn = -1;
            int highRequirement = requirements.Max();
            List<Flaskes> finalList = new List<Flaskes>();
            for (int i = 0; i < flaskTypes; i++)
            {
                Flaskes flaskes = new Flaskes();
                List<Markings> marking = new List<Markings>();
                int wast = -1;
                bool isMaxCapacityEnough = false;
                int hightCapacit = 0;
                markings.ForEach(x => {
                    List<int> listMark = x[0] == i ? x : null;
                    List<int> listRequirements = requirements;
                    if (listMark != null)
                    {
                        Markings frask = new Markings();
                        frask.Index = x[0];
                        frask.Mark = x[1];
                        frask.RequerimentPossible = requirements.LastOrDefault(r => r <= frask.Mark);
                        frask.OverFlow = frask.Mark - frask.RequerimentPossible;
                        marking.Add(frask);
                        hightCapacit = Math.Max(hightCapacit, frask.Mark);
                    }
                });
                isMaxCapacityEnough = hightCapacit >= highRequirement ? true : false;
                if (isMaxCapacityEnough)
                {
                    var requirementsList = marking.Select(x => x.RequerimentPossible).ToList();
                    var faultRqueriment = requirements.Where(x => !requirementsList.Contains(x)).ToList();
                    if (faultRqueriment != null)
                    {
                        var total = 0;
                        var newRequirementList = requirements.ToList();
                        marking = marking.OrderBy(x => x.OverFlow).ToList();
                        marking.ForEach(x =>
                        {
                            if (newRequirementList.Contains(x.RequerimentPossible))
                            {
                                total += x.OverFlow;
                                int index = newRequirementList.IndexOf(x.RequerimentPossible);
                                newRequirementList.RemoveAt(index);
                            }

                        });

                        wast = total;
                    }

                    flaskes.Markings = marking;
                    flaskes.TotalWast = wast;
                    finalList.Add(flaskes);
                }
            }
            flaskReturn = finalList.OrderBy(z => z.TotalWast).FirstOrDefault().Markings[0].Index;
            return flaskReturn;
        }

    

    }
}
