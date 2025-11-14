using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace game
{
    public class CaseResult
    {
        public Project caseProject;
        public int[] errors;
        public bool correct;
        public CaseResult(Project caseProject, int[] errors)
        {
            this.caseProject = caseProject;
            this.errors = errors;
            this.correct = errors.Length == 0 ? true : false;
        }
    }
    public class Result
    {
        public CaseResult[] cases;
        private int counter;
        public int corrects;
        public Result(int numCases)
        {
            this.cases = new CaseResult[numCases];
            this.counter = 0;
            this.corrects = 0;
        }
        public void UpdateCase(Project caseProject, int[] errors)
        {
            if (counter >= cases.Length)
                return;
            this.cases[counter] = new CaseResult(caseProject, errors);
            corrects += cases[counter].correct ? 1 : 0;
            counter++;
        }
        public string Name()
        {
            return this.cases[counter - 1].caseProject.name;
        }
        public string[] IndexesResults()
        {
            string[] ret = new string[] { "Certo", "Certo", "Certo", "Certo" };
            foreach (int index in this.cases[counter - 1].errors)
            {
                ret[index] = "Errado";
            }
            counter--;
            return ret;
        }

    }
}
